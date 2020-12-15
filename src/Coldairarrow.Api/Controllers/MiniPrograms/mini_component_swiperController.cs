using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_component_swiperController : BaseApiController
    {
        #region DI

        public mini_component_swiperController(Imini_component_swiperBusiness mini_component_swiperBus)
        {
            _mini_component_swiperBus = mini_component_swiperBus;
        }

        Imini_component_swiperBusiness _mini_component_swiperBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 轮播图文列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<MiniComponentSwiperDTO>> GetMiniComponentSwiperList(PageInput<List<ConditionDTO>> input)
        {
            return await _mini_component_swiperBus.GetMiniComponentSwiperListAsync(input);
        }
        /// <summary>
        /// 单个轮播图文--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MiniComponentSwiperDTO> GetMiniComponentSwiper(IdInputDTO input)
        {
            return await _mini_component_swiperBus.GetMiniComponentSwiperAsync(input) ?? new MiniComponentSwiperDTO();
        }
        [HttpPost]
        public async Task<PageResult<mini_component_swiper>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_component_swiperBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_component_swiper> GetTheData(IdInputDTO input)
        {
            return await _mini_component_swiperBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(MiniComponentSwiperDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_component_swiperBus.AddDataAsync(data);
            }
            else
            {
                await _mini_component_swiperBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_component_swiperBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}