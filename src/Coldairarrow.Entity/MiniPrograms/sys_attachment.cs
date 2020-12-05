using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// sys_attachment
    /// </summary>
    [Table("sys_attachment")]
    public class sys_attachment
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// 文件后缀名
        /// </summary>
        public String FileExt { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public Single? FileSize { get; set; }

        /// <summary>
        /// 文件存储相对路径
        /// </summary>
        public String FilePath { get; set; }

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