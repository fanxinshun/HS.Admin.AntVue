using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_component_itemBusiness
    {
        Task<PageResult<mini_component_item>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_component_item> GetTheDataAsync(string id);
        Task AddDataAsync(mini_component_item data);
        Task UpdateDataAsync(mini_component_item data);
        Task DeleteDataAsync(List<string> ids);
        Task<PageResult<MiniComponentProductDTO>> GetMiniComponentProductDTOListAsync(PageInput<List<ConditionDTO>> input);
        Task<MiniComponentProductDTO> GetMiniComponentProductAsync(IdInputDTO input);
        Task InsertProductDataAsync(MiniComponentProductDTO data);
        Task UpdateProductDataAsync(MiniComponentProductDTO data);
        Task DeleteProductDataAsync(List<string> ids);

    }

    /// <summary>
    /// 商品组件DTO
    /// </summary>
    [Map(typeof(mini_component))]
    public class MiniComponentProductDTO : mini_component_item
    {
        /// <summary>
        /// 页面Id
        /// </summary>
        public String Page_Id { get; set; }

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