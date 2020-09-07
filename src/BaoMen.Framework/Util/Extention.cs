using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BaoMen.Framework.Util
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extention
    {
        /// <summary>
        /// 配置DI中业务逻辑
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBmmf(this IServiceCollection services)
        {
            services.AddSingleton<System.BusinessLogic.IUserManager, System.BusinessLogic.UserManager>();
            services.AddSingleton<System.BusinessLogic.UserRoleManager>();
            services.AddSingleton<System.BusinessLogic.UserTokenManager>();
            services.AddSingleton<System.BusinessLogic.IUserLoginHistoryManager, System.BusinessLogic.UserLoginHistoryManager>();
            services.AddSingleton<System.BusinessLogic.IRoleManager, System.BusinessLogic.RoleManager>();
            services.AddSingleton<System.BusinessLogic.RoleModuleManager>();
            services.AddSingleton<System.BusinessLogic.IModuleManager, System.BusinessLogic.ModuleManager>();
            services.AddSingleton<System.BusinessLogic.IOperateHistoryManager, System.BusinessLogic.OperateHistoryManager>();
            services.AddSingleton<System.BusinessLogic.IParameterManager, System.BusinessLogic.ParameterManager>();
            services.AddSingleton<System.BusinessLogic.IProvinceManager, System.BusinessLogic.ProvinceManager>();
            services.AddSingleton<System.BusinessLogic.ICityManager, System.BusinessLogic.CityManager>();
            services.AddSingleton<System.BusinessLogic.IDistrictManager, System.BusinessLogic.DistrictManager>();
            services.AddSingleton<System.BusinessLogic.IUploadFileManager, System.BusinessLogic.UploadFileManager>();
            services.AddSingleton<System.BusinessLogic.IDownloadFileManager, System.BusinessLogic.DownloadFileManager>();

            return services;
        }
    }
}
