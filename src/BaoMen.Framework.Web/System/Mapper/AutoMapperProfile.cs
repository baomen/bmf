using BaoMen.Common.Model;
using BaoMen.Framework.System.BusinessLogic;
using BaoMen.Framework.Web.Converter;
using Entity = BaoMen.Framework.System.Entity;

namespace BaoMen.Framework.Web.System.Mapper
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public class AutoMapperProfile : AutoMapper.Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoMapperProfile()
        {
            //CreateMap<Entity.User, Models.User>().ForMember(dest => dest.StatusName, opt => opt.ConvertUsing<StatusValueToNameConverter, string>(src => src.Status.ToString()));
            CreateMap<Entity.User, Models.User>();
            CreateMap<Models.CreateUser, Entity.User>().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RoleIds));
            CreateMap<Models.UpdateUser, Entity.User>().ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RoleIds));
            CreateMap<Models.UpdateUserPersonalSetting, Entity.User>();
            CreateMap<Models.DeleteUser, Entity.User>();
            CreateMap<Entity.User, TextValue<string>>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));

            CreateMap<Entity.Role, Models.Role>().ForMember(dest => dest.ModuleIds, opt => opt.MapFrom(src => src.Modules));
            CreateMap<Models.CreateRole, Entity.Role>().ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.ModuleIds));
            CreateMap<Models.UpdateRole, Entity.Role>().ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.ModuleIds));
            CreateMap<Models.DeleteRole, Entity.Role>();
            CreateMap<Entity.Role, string>().ConvertUsing(src => src.Id);
            CreateMap<string, Entity.Role>().ConvertUsing(src => new Entity.Role { Id = src });
            CreateMap<Entity.Role, TextValue<string>>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));

            CreateMap<Entity.Module, Models.Module>();
            CreateMap<Models.CreateModule, Entity.Module>();
            CreateMap<Models.UpdateModule, Entity.Module>();
            CreateMap<Models.DeleteModule, Entity.Module>();
            CreateMap<Entity.Module, string>().ConvertUsing(src => src.Id);
            CreateMap<string, Entity.Module>().ConvertUsing(src => new Entity.Module { Id = src });

            CreateMap<Entity.Parameter, Models.Parameter>();
            CreateMap<Models.CreateParameter, Entity.Parameter>();
            CreateMap<Models.UpdateParameter, Entity.Parameter>();
            CreateMap<Models.DeleteParameter, Entity.Parameter>();

            CreateMap<Entity.OperateHistory, Models.OperateHistory>()
                .ForMember(dest => dest.UserName, opt => opt.ConvertUsing<KeyToNameConverter<string, IUserManager>, string>(src => src.UserId));

            CreateMap<Entity.Province, Models.Province>();
            CreateMap<Entity.Province, TextValue<string>>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
            CreateMap<Models.CreateProvince, Entity.Province>();
            CreateMap<Models.UpdateProvince, Entity.Province>();
            CreateMap<Models.DeleteProvince, Entity.Province>();

            CreateMap<Entity.City, Models.City>();
            CreateMap<Entity.City, TextValue<string>>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
            CreateMap<Models.CreateCity, Entity.City>();
            CreateMap<Models.UpdateCity, Entity.City>();
            CreateMap<Models.DeleteCity, Entity.City>();

            CreateMap<Entity.District, Models.District>();
            CreateMap<Entity.District, TextValue<string>>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
            CreateMap<Models.CreateDistrict, Entity.District>();
            CreateMap<Models.UpdateDistrict, Entity.District>();
            CreateMap<Models.DeleteDistrict, Entity.District>();

            CreateMap<Entity.UserLoginHistory, Models.UserLoginHistory>()
                .ForMember(dest => dest.TypeName, opt => opt.ConvertUsing<ParameterValueToNameConverter, ParameterSourceMember>(src => new ParameterSourceMember("02010101", src.Type)))
                .ForMember(dest => dest.ResultName, opt => opt.ConvertUsing<ParameterValueToNameConverter, ParameterSourceMember>(src => new ParameterSourceMember("02010102", src.Result)));
            CreateMap<Models.CreateUserLoginHistory, Entity.UserLoginHistory>();
            CreateMap<Models.UpdateUserLoginHistory, Entity.UserLoginHistory>();
            CreateMap<Models.DeleteUserLoginHistory, Entity.UserLoginHistory>();

            CreateMap<Entity.DownloadFile, Models.DownloadFile>()
                .ForMember(dest => dest.TypeName, opt => opt.ConvertUsing<ParameterValueToNameConverter, ParameterSourceMember>(src => new ParameterSourceMember("02010301", src.Type)));

            CreateMap<Entity.UploadFile, Models.UploadFile>()
                .ForMember(dest => dest.TypeName, opt => opt.ConvertUsing<ParameterValueToNameConverter, ParameterSourceMember>(src => new ParameterSourceMember("02010201", src.Type)))
                .ForMember(dest => dest.CreateUserName, opt => opt.ConvertUsing<KeyToNameConverter<string, IUserManager>, string>(src => src.CreateUserId));
        }

    }
}
