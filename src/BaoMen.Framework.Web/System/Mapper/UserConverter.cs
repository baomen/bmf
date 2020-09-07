//using AutoMapper;
//using BaoMen.Framework.System.BusinessLogic;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BaoMen.Framework.System.Mapper
//{
//    /// <summary>
//    /// 系统用户转换器
//    /// </summary>
//    public abstract class UserConverter<TSourceMember, TDestinationMember> : IValueConverter<TSourceMember, TDestinationMember>
//    {
//        /// <summary>
//        /// 用户管理业务逻辑
//        /// </summary>
//        protected readonly IUserManager userManager;

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="userManager">系统用户业务逻辑</param>
//        public UserConverter(IUserManager userManager)
//        {
//            this.userManager = userManager;
//        }

//        /// <summary>
//        /// 转换
//        /// </summary>
//        /// <param name="sourceMember"></param>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public abstract TDestinationMember Convert(TSourceMember sourceMember, ResolutionContext context);
//    }

//    /// <summary>
//    /// 用户ID --> 用户名 转换器
//    /// </summary>
//    public class UserIdToUserNameConverter : UserConverter<string, string>
//    {
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="userManager">系统用户业务逻辑</param>
//        public UserIdToUserNameConverter(IUserManager userManager) : base(userManager)
//        {
//        }

//        /// <summary>
//        /// 转换
//        /// </summary>
//        /// <param name="sourceMember"></param>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public override string Convert(string sourceMember, ResolutionContext context)
//        {
//            return userManager.Get(sourceMember)?.UserName;
//        }
//    }
//}
