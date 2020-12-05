using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_module_componentBusiness
    {
        Task<PageResult<mini_module_component>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_module_component> GetTheDataAsync(string id);
        Task AddDataAsync(mini_module_component data);
        Task UpdateDataAsync(mini_module_component data);
        Task DeleteDataAsync(List<string> ids);
        Task<ModuleComponentDTO> GetModuleComponent(IdInputDTO input);
        Task<PageResult<ModuleComponentDTO>> GetModuleComponentListAsync(PageInput<ConditionDTO> input);
        Task AddModuleComponentAsync(ModuleComponentDTO data);
        Task DeleteModuleComponentAsync(List<string> ids);
    }
    /// <summary>
    /// 页面组件DTO
    /// </summary>
    [Map(typeof(mini_module_component), typeof(mini_component))]
    public class ModuleComponentDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 项目地
        /// </summary>
        public String Project_Id { get; set; }

        /// <summary>
        /// 页面ID
        /// </summary>
        public String Module_Id { get; set; }

        /// <summary>
        /// 页面编码
        /// </summary>
        public String Module_Code { get; set; }

        /// <summary>
        /// 页面名称
        /// </summary>
        public String Module_Name { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Single? Sort { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 用户组件
        /// </summary>
        public String Component_Id { get; set; }

        /// <summary>
        /// 跳转页
        /// </summary>
        public String Target_Pages { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 系统组件
        /// </summary>
        public String Sys_Component_Id { get; set; }

        /// <summary>
        /// 系统组件名称
        /// </summary>
        public String Component_Name { get; set; }

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