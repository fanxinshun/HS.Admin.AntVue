using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_component_nestedBusiness
    {
        Task<PageResult<mini_component_item>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_component_item> GetTheDataAsync(string id);
        Task AddDataAsync(mini_component_item data);
        Task UpdateDataAsync(mini_component_item data);
        Task DeleteDataAsync(List<string> ids);
        Task<PageResult<ModuleComponentDTO>> GetMiniComponentNestedDTOListAsync(PageInput<ConditionDTO> input);
        //        Task<MiniComponentNestedDTO> GetMiniComponentNested(IdInputDTO input);
        Task InsertProductDataAsync(MiniComponentNestedDTO data);
        Task UpdateProductDataAsync(MiniComponentNestedDTO data);
        Task DeleteProductDataAsync(List<string> ids);

    }

    /// <summary>
    /// 嵌套组件DTO
    /// </summary>
    [Map(typeof(mini_component))]
    public class MiniComponentNestedDTO : mini_component_item
    {
        /// <summary>
        /// 页面Id
        /// </summary>
        public String Page_Id { get; set; }

        /// <summary>
        /// 父组件
        /// </summary>
        public String Parent_Component_Id { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public String Sys_Component_Id { get; set; }

        /// <summary>
        /// 组件描述
        /// </summary>
        public String Description { get; set; }

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