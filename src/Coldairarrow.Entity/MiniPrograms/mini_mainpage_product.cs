using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_mainpage_product
    /// </summary>
    [Table("mini_mainpage_product")]
    public class mini_mainpage_product
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 模组
        /// </summary>
        public String Module_Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public String Commodities_Id { get; set; }

        /// <summary>
        /// 商品活动图片
        /// </summary>
        public String Attachment_Id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Description { get; set; }

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