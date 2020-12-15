using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_attachment_module
    /// </summary>
    [Table("mini_attachment_module")]
    public class mini_attachment_module
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 组件Id
        /// </summary>
        public String Component_Id { get; set; }

        /// <summary>
        /// 组件项(组件内有多类图片，用改字段区分)
        /// </summary>
        public String Component_Item { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public String Image_Path { get; set; }

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