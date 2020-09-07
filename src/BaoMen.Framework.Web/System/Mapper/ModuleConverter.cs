//using AutoMapper;
//using BaoMen.Framework.System.Entity;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BaoMen.Framework.System.Mapper
//{
//    /// <summary>
//    /// 从Module的ID转为Module实例
//    /// </summary>
//    public class IdToModuleTypeConverter : ITypeConverter<string, Entity.Module>
//    {
//        /// <summary>
//        /// 抓换
//        /// </summary>
//        /// <param name="source"></param>
//        /// <param name="destination"></param>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public Entity.Module Convert(string source, Entity.Module destination, ResolutionContext context)
//        {
//            return new Entity.Module() { Id = source };
//        }
//    }

//    /// <summary>
//    /// 从Module实例转为Module的ID
//    /// </summary>
//    public class ModuleToIdTypeConverter : ITypeConverter<Entity.Module, string>
//    {
//        /// <summary>
//        /// 转换
//        /// </summary>
//        /// <param name="source"></param>
//        /// <param name="destination"></param>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public string Convert(Entity.Module source, string destination, ResolutionContext context)
//        {
//            return source.Id;
//        }
//    }

//    /// <summary>
//    /// 从Module的ID转为Module实例
//    /// </summary>
//    public class IdToModuleValueConverter : IValueConverter<string, Entity.Module>
//    {
//        /// <summary>
//        /// 转换
//        /// </summary>
//        /// <param name="sourceMember"></param>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public Entity.Module Convert(string sourceMember, ResolutionContext context)
//        {
//            return new Entity.Module() { Id = sourceMember };
//        }
//    }

//    /// <summary>
//    /// 从Module的ID转为Module实例
//    /// </summary>
//    public class ModuleToIdValueConverter : IValueConverter<Entity.Module, string>
//    {
//        /// <summary>
//        /// 转换
//        /// </summary>
//        /// <param name="sourceMember"></param>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public string Convert(Entity.Module sourceMember, ResolutionContext context)
//        {
//            return sourceMember.Id;
//        }
//    }
//}
