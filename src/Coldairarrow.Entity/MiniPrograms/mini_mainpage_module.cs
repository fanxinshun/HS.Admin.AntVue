using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_mainpage_module
    /// </summary>
    [Table("mini_mainpage_module")]
    public class mini_mainpage_module
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 项目地
        /// </summary>
        public String Project_Id { get; set; }

        /// <summary>
        /// 模组编码
        /// </summary>
        public String Module_Code { get; set; }

        /// <summary>
        /// 模组名称
        /// </summary>
        public String Module_Name { get; set; }

        /// <summary>
        /// 父级模组
        /// </summary>
        public String Parent_Module_Id { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Single? Sort { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public String Remark { get; set; }

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