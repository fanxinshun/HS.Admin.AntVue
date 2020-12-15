using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_componentBusiness
    {
        Task<List<ComponentTreeDTO>> GetTreeDataListAsync(ComponentTreeInputDTO input);
        Task<List<ComponentTreeDTO>> GetTreeDataDetailListAsync(ComponentTreeInputDTO input);
    }

    /// <summary>
    /// 用户组件DTO
    /// </summary>
    [Map(typeof(mini_component))]
    public class ComponentTreeDTO : TreeModel
    {
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }

        /// <summary>
        /// 页面类型名称
        /// </summary>
        public String PageTypeName { get; set; }

        /// <summary>
        /// 页面描述
        /// </summary>
        public String PageRemark { get; set; }

        /// <summary>
        /// 系统组件编码
        /// </summary>
        public String Component_Code { get; set; }

        /// <summary>
        /// 系统组件名称
        /// </summary>
        public String Component_Name { get; set; }
        public string Page_Id { get; set; }
        public string Component_Id { get; set; }
        public string Project_Id { get; set; }
        public float? Sort { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Sys_Component_Id { get; set; }
        public string Target_Pages { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
    }


    /// <summary>
    /// 组件属性结构查询条件
    /// </summary>
    public class ComponentTreeInputDTO
    {
        public string parentId { get; set; }
        public string pageId { get; set; }
    }

}