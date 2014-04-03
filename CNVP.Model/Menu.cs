using System;
using System.Collections.Generic;
using System.Text;

namespace CNVP.Model
{
    public class Menu
    {
        /// <summary>
        /// 菜单序号
        /// </summary>
        public int MenuID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单权限值
        /// </summary>
        public string MenuValue { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string MenuUrl { get; set; }
        /// <summary>
        /// 菜单父类
        /// </summary>
        public string MenuParent { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 是否可用(0-不可用，1-可用)
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 是否有子类(0-没有，1-有)
        /// </summary>
        public int IsLeaf { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int OrderID { get; set; }
    }
}