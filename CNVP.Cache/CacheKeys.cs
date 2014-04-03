using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Cache
{
    public class CacheKeys
    {
        /// <summary>
        /// 所有菜单
        /// </summary>
        public const string AllMenu = "/HongXu/AllMenu";
        /// <summary>
        /// 商家列表
        /// </summary>
        public const string StoreList = "/HongXu/StoreList";
        /// <summary>
        /// 会员列表
        /// </summary>
        public const string AllUsers = "/HongXu/AllUsers";
        /// <summary>
        /// 角色列表
        /// </summary>
        public const string AllRoles = "/HongXu/AllRoles";
        /// <summary>
        /// 品牌列表
        /// </summary>
        public const string AllBrand = "/HongXu/AllBrand";

        public const string GoodB2C_RefreshToken = "/HongXu/RefreshToken";
        public const string GoodB2C_ClicksUrl = "/HongXu/TaoBao/ClicksUrl{0}";
    }
}