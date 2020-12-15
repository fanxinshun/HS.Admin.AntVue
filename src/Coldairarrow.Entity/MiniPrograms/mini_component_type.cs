using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_component_type
    /// </summary>
    [Table("mini_component_type")]
    public class mini_component_type
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 组件编码
        /// </summary>
        public String Component_Code { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public String Component_Name { get; set; }

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