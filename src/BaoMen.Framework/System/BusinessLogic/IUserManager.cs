/*
Author: WangXinBin
CreateTime: 2019/9/23 14:34:58
*/

using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using BaoMen.Common.Model;
using Microsoft.AspNetCore.Http;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region interface IUserManager (generated)
    /// <summary>
    /// 系统用户业务逻辑接口
    /// </summary>
    public interface IUserManager : ICacheableBusinessLogic<string, User, UserFilter>, Util.IGetNameManager<string>
    {
        /// <summary>
        /// 获取用户令牌
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        UserToken GetUserToken(string token);

        /// <summary>
        /// 更新用户令牌
        /// </summary>
        /// <param name="item">用户令牌实例</param>
        /// <returns></returns>
        int UpdateUserToken(UserToken item);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="item">用户实体</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        int ModifyPassword(User item, string newPassword);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string ResetPassword(User item);

        /// <summary>
        /// 过期Token
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        int ExpireToken(string userId);

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="item">用户实例</param>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        int UpdateAvatar(User item, IFormFile file);

        /// <summary>
        /// 用户修改个人设置
        /// </summary>
        /// <param name="item">用户实体</param>
        int ModifyPersonalSetting(Entity.User item);
    }
    #endregion
}