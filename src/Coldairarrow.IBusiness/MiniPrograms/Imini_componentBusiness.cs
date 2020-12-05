using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_componentBusiness
    {
        Task<PageResult<mini_component>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_component> GetTheDataAsync(string id);
        Task AddDataAsync(mini_component data);
        Task UpdateDataAsync(mini_component data);
        Task DeleteDataAsync(List<string> ids);
        Task<PageResult<MiniComponentDTO>> GetMiniComponentDTOListAsync(PageInput<ConditionDTO> input);
        Task<MiniComponentDTO> GetMiniComponentAsync(IdInputDTO input);
        Task AddMiniComponentAsync(MiniComponentDTO data);
        Task UpdateMiniComponentAsync(MiniComponentDTO data);
        Task DeleteMiniComponentAsync(List<string> ids);
    }

    /// <summary>
    /// 用户组件DTO
    /// </summary>
    //[Map(typeof(mini_component))]
    public class MiniComponentDTO : mini_component
    {
        /// <summary>
        /// 系统组件名称
        /// </summary>
        public String Component_Name { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public List<string> Images { get; set; }
    }
}