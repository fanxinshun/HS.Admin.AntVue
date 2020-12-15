using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_component_swiperBusiness
    {
        Task<PageResult<mini_component_swiper>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_component_swiper> GetTheDataAsync(string id);
        Task AddDataAsync(MiniComponentSwiperDTO data);
        Task UpdateDataAsync(MiniComponentSwiperDTO data);
        Task DeleteDataAsync(List<string> ids);
        Task<PageResult<MiniComponentSwiperDTO>> GetMiniComponentSwiperListAsync(PageInput<List<ConditionDTO>> input);
        Task<MiniComponentSwiperDTO> GetMiniComponentSwiperAsync(IdInputDTO input);

    }

    /// <summary>
    /// 轮播图文DTO
    /// </summary>
    [Map(typeof(mini_component))]
    public class MiniComponentSwiperDTO : mini_component_swiper
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