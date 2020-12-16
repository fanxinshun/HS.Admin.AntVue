using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_page
    /// </summary>
    [Table("mini_page_type")]
    public class mini_page_type
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 页面类型编码
        /// </summary>
        public String Type_Code { get; set; }

        /// <summary>
        /// 页面类型名称
        /// </summary>
        public String Type_Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 排序
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