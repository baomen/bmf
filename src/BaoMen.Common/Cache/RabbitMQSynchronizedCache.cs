using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaoMen.Common.Cache
{
    /// <summary>
    /// 基于RabbitMQ同步的缓存
    /// </summary>
    public class RabbitMQSynchronizedCache : ICache, ISynchronizedCache, IDisposable
    {
        private MemoryCache memoryCache;

        private const string exchangeName = "baomen.cache";
        private const string cacheRemoveRoutingName = "baomen.cache.removed";

        IConnection queueConnection;
        private IModel channel;

        private Dictionary<string, string> keyMapDictionary = new Dictionary<string, string>();
        private readonly string guid = Guid.NewGuid().ToString();

        public event EventHandler<CacheSynchronizingEventArgs> OnSynchronizing;
        public event EventHandler<CacheSynchronizedEventArgs> OnSynchronized;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public RabbitMQSynchronizedCache(IConfiguration configuration)
        {
            string connectionStringName = configuration.GetSection("Cache:RabbitMQSynchronizedCacheConfig")["ConnectionStringName"];
            if (string.IsNullOrEmpty(connectionStringName))
            {
                connectionStringName = configuration.GetSection("RabbitMQConfiguration")["DefaultConnectionString"] ?? "default";
            }
            string connectionString = configuration.GetSection($"RabbitMQConfiguration:ConnectionStrings:{connectionStringName}")["ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString", "Construt RabbitMQSynchronizedCache instance error.RabbitMQ conncetion string is null or empty.");
            string partialQueueName = configuration.GetSection($"Cache:RabbitMQSynchronizedCacheConfig")["QueueName"];
            //string queuePartialName = configuration.GetSection("Cache:RabbitMQSynchronizedCacheConfig")["QueuePartialName"];
            //string queueName = $"{exchangeName}.{ queuePartialName}";
            string queueName = $"{exchangeName}.{partialQueueName}.{guid}";
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(connectionString);
            queueConnection = factory.CreateConnection();
            channel = queueConnection.CreateModel();
            channel.ExchangeDeclare(exchange: exchangeName, type: "direct", durable: true);
            QueueDeclareOk queueDeclareOk = channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true, arguments: null);
            channel.QueueBind(queue: queueDeclareOk.QueueName, exchange: exchangeName, routingKey: cacheRemoveRoutingName, arguments: null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            memoryCache = new MemoryCache();

            List<IConfigurationSection> keyMapSections = configuration.GetSection("Cache:RabbitMQSynchronizedCacheConfig:KeyMap").GetChildren().ToList();
            if (keyMapSections != null)
            {
                foreach (IConfigurationSection keyMapSection in keyMapSections)
                {
                    keyMapDictionary.Add(keyMapSection.Key, keyMapSection.Value);
                }
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            RemoteKey remoteKey = Extension.ObjectExtension.Deserialize<RemoteKey>(e.Body.ToArray());
            //RemoteKey remoteKey = Extension.ObjectExtension.Deserialize<RemoteKey>(e.Body);
            if (remoteKey.Guid != guid)
            {
                CacheSynchronizingEventArgs cacheSynchronizingEventArgs = new CacheSynchronizingEventArgs() { Key = remoteKey.Key };
                OnSynchronizing?.Invoke(sender, cacheSynchronizingEventArgs);
                if (!cacheSynchronizingEventArgs.Cancel)
                {
                    memoryCache.Remove(remoteKey.Key);
                    if (keyMapDictionary.ContainsKey(remoteKey.Key))
                    {
                        memoryCache.Remove(keyMapDictionary[remoteKey.Key]);
                    }
                    OnSynchronized?.Invoke(sender, new CacheSynchronizedEventArgs() { Key = remoteKey.Key });
                }
            }
            ((EventingBasicConsumer)sender).Model.BasicAck(deliveryTag: e.DeliveryTag, multiple: true);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~RabbitMQSynchronizedCache()
        {
            Dispose();
        }

        #region ICache
        object ICache.Get(string key)
        {
            return memoryCache.Get(key);
        }

        void ICache.Set(string key, object value)
        {
            memoryCache.Set(key, value);
        }

        void ICache.Remove(string key)
        {
            memoryCache.Remove(key);
            RemoteKey remoteKey = new RemoteKey() { Key = key, Guid = guid };
            channel.BasicPublish(exchange: exchangeName, routingKey: cacheRemoveRoutingName, basicProperties: null, body: Extension.ObjectExtension.Serialize(remoteKey));
        }
        #endregion

        #region ISynchronizedCache


        #endregion

        #region IDisposable
        public void Dispose()
        {
            memoryCache = null;
            if (channel != null)
            {
                channel.Dispose();
            }
            if (queueConnection != null)
            {
                queueConnection.Dispose();
            }
        }
        #endregion
    }
}
