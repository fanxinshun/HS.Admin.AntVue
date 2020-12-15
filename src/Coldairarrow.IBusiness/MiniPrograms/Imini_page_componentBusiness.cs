using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_page_componentBusiness
    {
        Task<PageResult<mini_page_component>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_page_component> GetTheDataAsync(string id);
        Task AddDataAsync(mini_page_component data);
        Task UpdateDataAsync(mini_page_component data);
        Task DeleteDataAsync(List<string> ids);
        Task<ModuleComponentDTO> GetModuleComponent(IdInputDTO input);
        Task<PageResult<ModuleComponentDTO>> GetModuleComponentListAsync(PageInput<List<ConditionDTO>> input);
        Task AddModuleComponentAsync(ModuleComponentDTO data);
        Task DeleteModuleComponentAsync(List<string> ids);
        Task UpdateDataAsync(ModuleComponentDTO data);
    }

    /// <summary>
    /// 页面组件DTO
    /// </summary>
    [Map(typeof(mini_page_component), typeof(mini_component))]
    public class ModuleComponentDTO : mini_page_component
    {
        /// <summary>
        /// 项目地
        /// </summary>
        public String Project_Id { get; set; }

        /// <summary>
        /// 父组件
        /// </summary>
        public String Parent_Component_Id { get; set; }

        /// <summary>
        /// 页面类型名称
        /// </summary>
        public String PageTypeName { get; set; }

        /// <summary>
        /// 页面描述
        /// </summary>
        public String PageRemark { get; set; }

        /// <summary>
        /// 跳转页
        /// </summary>
        public String Target_Pages { get; set; }

        /// <summary>
        /// 组件标识码，用于同一个组件展示不同效果
        /// </summary>
        public String Tag { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 系统组件
        /// </summary>
        public String Sys_Component_Id { get; set; }

        /// <summary>
        /// 系统组件编码
        /// </summary>
        public String Component_Code { get; set; }

        /// <summary>
        /// 系统组件名称
        /// </summary>
        public String Component_Name { get; set; }

    }
}