using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_component_swiper
    /// </summary>
    [Table("mini_component_swiper")]
    public class mini_component_swiper
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
        /// 链接对象
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 链接对象传值
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public String Image { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Tittle { get; set; }

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