using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_module_component
    /// </summary>
    [Table("mini_page_component")]
    public class mini_page_component
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 页面ID
        /// </summary>
        public String Page_Id { get; set; }


        /// <summary>
        /// 组件ID
        /// </summary>
        public String Component_Id { get; set; }

        ///// <summary>
        ///// 页面组件描述
        ///// </summary>
        //public String Description { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Single? Sort { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

    }
}