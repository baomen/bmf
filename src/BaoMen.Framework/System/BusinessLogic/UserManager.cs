/*
Author: WangXinBin
CreateTime: 2019/9/23 14:34:58
*/

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using BaoMen.Framework.System.Entity;
using BaoMen.Common.Data;
using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using BaoMen.Common.Extension;
using BaoMen.Common.Constant;
using BaoMen.Common.Model;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Runtime.InteropServices;

namespace BaoMen.Framework.System.BusinessLogic
{
    #region class UserManager (generated)
    /// <summary>
    /// 系统用户业务逻辑
    /// </summary>
    public partial class UserManager : CacheableBusinessLogicBase<string, User, UserFilter, DataAccess.User>, IUserManager
    {
        private readonly IServiceProvider serviceProvider;
        private readonly UserRoleManager userRoleManager;
        private readonly UserTokenManager userTokenManager;
        private readonly IOperateHistoryManager operateHistoryManager;

        private static readonly char[] constant = {
                    '0','1','2','3','4','5','6','7','8','9',
                    'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                    'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
                };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置实例</param>
        /// <param name="serviceProvider">服务提供程序</param>
        public UserManager(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
        {
            this.serviceProvider = serviceProvider;
            operateHistoryManager = serviceProvider.GetRequiredService<IOperateHistoryManager>();
            userRoleManager = serviceProvider.GetRequiredService<UserRoleManager>();
            userTokenManager = serviceProvider.GetRequiredService<UserTokenManager>();
        }

        /// <summary>
        /// 插入系统用户
        /// </summary>
        /// <param name="item">系统用户实体</param>
        /// <returns></returns>
        protected override int DoInsert(User item)
        {
            //检查用户名
            User user = GetList(new UserFilter { UserName = item.UserName }).FirstOrDefault();
            if (user != null)
            {
                throw new ArgumentException("用户名已经存在");
            }

            item.CreateTime = DateTime.Now;
            item.Password = item.Password.To32MD5();
            //item.Email = item.Email ?? string.Empty;
            item.Email ??= string.Empty;
            item.WechatMpOpenId ??= string.Empty;
            item.WechatOpenId ??= string.Empty;
            item.WechatUnionId ??= string.Empty;
            item.DingTalkId ??= string.Empty;
            item.AlipayId ??= string.Empty;
            return ProcessWithTransaction((transaction) =>
            {
                int rows = dal.Insert(item, transaction);
                rows += InsertExtention(item, transaction);
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Insert, transaction: transaction);
                return rows;
            });
        }

        /// <summary>
        /// 更新系统用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override int DoUpdate(User item)
        {
            User user = GetList(new UserFilter { UserName = item.UserName, Id = new FilterProperty<string> { Value = item.Id, CompareOperator = DbCompareOperator.NotEqual } }).FirstOrDefault();
            if (user != null)
            {
                throw new ArgumentException("用户名已经存在");
            }
            item.Email ??= string.Empty;
            item.WechatMpOpenId ??= string.Empty;
            item.WechatOpenId ??= string.Empty;
            item.WechatUnionId ??= string.Empty;
            item.DingTalkId ??= string.Empty;
            item.AlipayId ??= string.Empty;
            return ProcessWithTransaction((transaction) =>
            {
                int rows = DeleteExtention(item, transaction);
                rows += InsertExtention(item, transaction);
                rows += dal.Update(item, transaction);
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Update, transaction: transaction);
                return rows;
            });

        }

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override int DoDelete(User item)
        {
            return ProcessWithTransaction((transaction) =>
            {
                int rows = DeleteExtention(item, transaction);
                rows += dal.Delete(item, transaction);
                operateHistoryManager.Insert(item.Id, item, DataOperationType.Delete, transaction: transaction);
                return rows;
            });
        }

        /// <summary>
        /// 添加扩展属性
        /// </summary>
        /// <param name="item">系统用户实体</param>
        protected override void AppendExtention(User item)
        {
            base.AppendExtention(item);
            IRoleManager roleManager = serviceProvider.GetRequiredService<IRoleManager>();
            ICollection<UserRole> userRoles = userRoleManager.GetList(new UserRoleFilter { UserId = item.Id });
            item.Roles = userRoles.Select(p => roleManager.Get(p.RoleId)).ToList();
        }

        private int InsertExtention(User item, IDbTransaction transaction)
        {
            int rows = 0;
            if (item.Roles?.Count > 0)
            {
                var userRoles = from p in item.Roles select new UserRole() { UserId = item.Id, RoleId = p.Id };
                foreach (var userRole in userRoles)
                {
                    rows += userRoleManager.Dal.Insert(userRole, transaction);
                }
            }
            rows += userTokenManager.Dal.Insert(new UserToken { UserId = item.Id, Expires = item.CreateTime, Token = Guid.NewGuid().ToString("N") }, transaction);
            return rows;
        }

        private int DeleteExtention(User item, IDbTransaction transaction)
        {
            int rows = userRoleManager.Dal.Delete(item.Id, transaction);
            rows += userTokenManager.Dal.Delete(new UserToken { UserId = item.Id }, transaction);
            return rows;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        public override void RemoveCache()
        {
            userRoleManager.RemoveCache();
            base.RemoveCache();
        }

        /// <summary>
        /// 获取用户令牌
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public Entity.UserToken GetUserToken(string token)
        {
            return userTokenManager.GetList(new UserTokenFilter { Token = token }).FirstOrDefault();
        }

        /// <summary>
        /// 更新用户令牌
        /// </summary>
        /// <param name="item">用户令牌实例</param>
        /// <returns></returns>
        public int UpdateUserToken(UserToken item)
        {
            return userTokenManager.Update(item);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="item">用户实体</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public int ModifyPassword(User item, string newPassword)
        {
            return ProcessUpdate(() =>
            {
                User user = Get(item.Id);
                if (item.Password != user.Password)
                {
                    throw new ArgumentException("原密码不正确");
                }
                else
                {
                    item.Password = newPassword.To32MD5();
                    int affectRows = ProcessWithTransaction((transaction) =>
                    {
                        int rows = dal.ModifyPassword(item, transaction.Connection, transaction);
                        rows += userTokenManager.Dal.ExpireToken(item.Id, transaction.Connection, transaction);
                        rows += operateHistoryManager.Insert(item.Id, item, DataOperationType.Update, description: "用户修改密码", transaction: transaction);
                        return rows;
                    });
                    if (affectRows > 0)
                    {
                        RemoveCache();
                    }
                    return affectRows;
                }
            }, (log) =>
            {
                log.Properties["item"] = item;
                log.Properties["newPassword"] = newPassword;
            });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string ResetPassword(User item)
        {
            string newPassword = CreateDefaultPassword();
            int affectRows = ProcessUpdate(() =>
            {
                string oldPassword = item.Password;
                item.Password = newPassword.To32MD5();
                int updateRows = ProcessWithTransaction((transaction) =>
                    {
                        int rows = dal.ModifyPassword(item, transaction.Connection, transaction);
                        rows += userTokenManager.Dal.ExpireToken(item.Id, transaction.Connection, transaction);
                        rows += operateHistoryManager.Insert(item.Id, item, DataOperationType.Update, description: "重置密码", transaction: transaction);
                        return rows;
                    });
                if (updateRows == 0)
                {
                    item.Password = oldPassword;
                }
                else
                {
                    RemoveCache();
                }
                return updateRows;
            },
            (log) =>
            {
                log.Properties["item"] = item;
            });
            if (affectRows > 0)
            {
                return newPassword;
            }
            else
            {
                throw new BusinessLogicException("重置用户密码失败");
            }
        }

        /// <summary>
        /// 过期Token
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int ExpireToken(string userId)
        {
            return ProcessUpdate(() =>
            {
                return userTokenManager.Dal.ExpireToken(userId);
            }, (log) =>
            {
                log.Properties["userId"] = userId;
            });
        }

        /// <summary>
        /// 生成默认随机密码
        /// </summary>
        /// <returns></returns>
        private string CreateDefaultPassword()
        {
            StringBuilder newRandom = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                newRandom.Append(constant[random.Next(62)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 根据ID取得名称
        /// </summary>
        /// <param name="key">ID</param>
        /// <returns></returns>
        public string GetName(string key)
        {
            return Get(key)?.Name;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="item">用户实例</param>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        public int UpdateAvatar(User item, IFormFile file)
        {
            return ProcessUpdate(() =>
            {
                IUploadFileManager uploadFileManager = serviceProvider.GetRequiredService<IUploadFileManager>();
                UploadFile uploadFile = uploadFileManager.CreateUploadFile(item.Id, item.Id, 1, file.FileName);
                string physicalPath = uploadFileManager.GetPhysicalPath(uploadFile.RelativePath);
                uploadFileManager.SaveFile(physicalPath, file);

                string existAvatar = item.Avatar;
                item.Avatar = uploadFile.RelativePath;

                int affectRows = ProcessWithTransaction((transaction) =>
                {
                    int rows = dal.ModifyAvatar(item, transaction.Connection, transaction);
                    rows += operateHistoryManager.Insert(item.Id, item, DataOperationType.Update, description: "用户修改头像", transaction: transaction);
                    rows += uploadFileManager.Insert(uploadFile, transaction);
                    return rows;
                });
                if (affectRows > 0)
                {
                    RemoveCache();
                }
                return affectRows;
            }, (log) =>
            {
                log.Properties[nameof(item)] = item;
            });
        }

        /// <summary>
        /// 用户修改个人设置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int ModifyPersonalSetting(User item)
        {
            item.Email ??= string.Empty;
            return ProcessUpdate(() =>
            {
                int row = dal.ModifyPersonalSetting(item);
                if (row > 0)
                {
                    RemoveCache();
                    operateHistoryManager.Insert(item.Id, item, DataOperationType.Update, description: "修改用户设置");
                }
                return row;
            }, (log) =>
            {
                log.Properties["item"] = item;
            });

        }
    }
    #endregion
}