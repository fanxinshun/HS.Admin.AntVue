using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// 页面类型
    /// </summary>
    [Table("app_page_type")]
    public class app_page_type
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public String num { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public Int32? order_id { get; set; }

        /// <summary>
        /// 删除标志(0:未删除,1:已删除)
        /// </summary>
        public Boolean is_deleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? deleted_time { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updated_time { get; set; }

    }
}