using BaoMen.Common.Constant;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaoMen.Common.Data
{
    /// <summary>
    /// 访问Redis的数据访问基类
    /// </summary>
    public abstract class RedisDataAccess<TKey, TValue>
        where TValue : class
    {
        /// <summary>
        /// Redis连接串
        /// </summary>
        protected readonly string connectionString;

        /// <summary>
        /// Redis Key分隔符
        /// </summary>
        protected const string keySeparator = ":";

        /// <summary>
        /// redis链接
        /// </summary>
        protected static IConnectionMultiplexer connectionMultiplexer = null;

        private static readonly object locker = new object();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="connectionString">Redis连接串</param>
        public RedisDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IConnectionMultiplexer GetConnectionMultiplexer()
        {
            if ((connectionMultiplexer == null) || !connectionMultiplexer.IsConnected)
            {
                lock (locker)
                {
                    if ((connectionMultiplexer == null) || !connectionMultiplexer.IsConnected)
                        connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
                }
            }

            return connectionMultiplexer;
        }

        /// <summary>
        /// 根据标识符获取Redis Key
        /// </summary>
        /// <param name="id">标识符</param>
        /// <returns></returns>
        protected abstract RedisKey GetRedisKey(TKey id);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value">Redis的值</param>
        /// <param name="serializeType">序列化类型</param>
        /// <returns></returns>
        protected TValue Deserialize(RedisValue value, SerializeType serializeType = SerializeType.Json)
        {
            return serializeType switch
            {
                SerializeType.Json => Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(value),
                //return System.Text.Json.JsonSerializer.Deserialize<TValue>(value);
                SerializeType.Binary => Extension.ObjectExtension.TryDeserialize<TValue>(value),
                SerializeType.Xml => Extension.ObjectExtension.DeserializeXml<TValue>(value),
                _ => throw new System.ArgumentException("not support serializetype"),
            };
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="serializeType">序列化类型</param>
        /// <returns></returns>
        protected RedisValue Serialize(TValue value, SerializeType serializeType = SerializeType.Json)
        {
            return serializeType switch
            {
                SerializeType.Json => Newtonsoft.Json.JsonConvert.SerializeObject(value),
                //return System.Text.Json.JsonSerializer.Serialize(value);
                SerializeType.Binary => Extension.ObjectExtension.Serialize(value),
                SerializeType.Xml => Extension.ObjectExtension.SerializeXml(value),
                _ => throw new System.ArgumentException("not support serializetype"),
            };
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serializeType">序列化类型</param>
        /// <param name="databaseIndex">数据库索引</param>
        /// <returns></returns>
        public virtual TValue Get(TKey id, SerializeType serializeType = SerializeType.Json, int databaseIndex = -1)
        {
            RedisKey key = GetRedisKey(id);
            IDatabase db = GetConnectionMultiplexer().GetDatabase(databaseIndex);
            RedisValue value = db.StringGet(key);
            if (value.IsNull) return null;
            return Deserialize(value, serializeType);
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="value">值</param>
        /// <param name="expiry">到期时间</param>
        /// <param name="serializeType">序列化类型</param>
        /// <param name="databaseIndex">数据库索引</param>
        /// <returns></returns>
        public virtual bool Set(TKey id, TValue value, TimeSpan? expiry = null, SerializeType serializeType = SerializeType.Json, int databaseIndex = -1)
        {
            RedisKey key = GetRedisKey(id);
            IDatabase db = GetConnectionMultiplexer().GetDatabase(databaseIndex);
            RedisValue redisValue = Serialize(value, serializeType);
            return db.StringSet(key, redisValue, expiry: expiry);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="ids">标识</param>
        /// <param name="serializeType">序列号类型</param>
        /// <param name="databaseIndex">数据库索引</param>
        /// <returns></returns>
        public virtual IList<TValue> GetList(IEnumerable<TKey> ids, SerializeType serializeType = SerializeType.Json, int databaseIndex = -1)
        {
            List<RedisKey> redisKeys = ids.Select(p => GetRedisKey(p)).ToList();
            return GetList(redisKeys, serializeType, databaseIndex);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="keys">RedisKeys</param>
        /// <param name="serializeType">序列号类型</param>
        /// <param name="databaseIndex">数据库索引</param>
        /// <returns></returns>
        public virtual IList<TValue> GetList(IEnumerable<RedisKey> keys, SerializeType serializeType = SerializeType.Json, int databaseIndex = -1)
        {
            List<TValue> values = new List<TValue>();
            List<Task<RedisValue>> redisValues = new List<Task<RedisValue>>();
            IDatabase db = GetConnectionMultiplexer().GetDatabase(databaseIndex);
            IBatch batch = db.CreateBatch();
            foreach (RedisKey key in keys)
            {
                redisValues.Add(batch.StringGetAsync(key));
            }
            batch.Execute();
            foreach (Task<RedisValue> redisValue in redisValues)
            {
                if (!redisValue.Result.IsNull)
                {
                    values.Add(Deserialize(redisValue.Result, serializeType));
                }
            }
            return values;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="databaseIndex">数据库索引</param>
        /// <returns></returns>
        public virtual bool Delete(TKey id, int databaseIndex = -1)
        {
            RedisKey key = GetRedisKey(id);
            IDatabase db = GetConnectionMultiplexer().GetDatabase(databaseIndex);
            return db.KeyDelete(key);
        }

        /// <summary>
        /// 获取Keys
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pattern"></param>
        /// <param name="pageSize"></param>
        /// <param name="cursor"></param>
        /// <param name="pageOffset"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public virtual IEnumerable<RedisKey> GetKeys(int database = 0, RedisValue pattern = default, int pageSize = 10, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
        {
            IConnectionMultiplexer connectionMultiplexer = GetConnectionMultiplexer();
            IServer server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()[0]);
            return server.Keys(database: database, pattern: pattern, pageSize: pageSize, cursor: cursor, pageOffset: pageOffset, flags: flags);
        }
    }
}
