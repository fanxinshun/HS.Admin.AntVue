using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_component_item
    /// </summary>
    [Table("mini_component_item")]
    public class mini_component_item
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 用户组件ID
        /// </summary>
        public String Component_Id { get; set; }

        /// <summary>
        /// Type链接对象
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 商品图片地址
        /// </summary>
        public String Image { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public String SKU { get; set; }

        /// <summary>
        /// Tittle
        /// </summary>
        public String Tittle { get; set; }

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