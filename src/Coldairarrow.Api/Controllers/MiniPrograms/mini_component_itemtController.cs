using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_component_itemController : BaseApiController
    {
        #region DI

        public mini_component_itemController(Imini_component_itemBusiness mini_component_productBus)
        {
            _mini_component_productBus = mini_component_productBus;
        }

        Imini_component_itemBusiness _mini_component_productBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 活动商品列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<MiniComponentProductDTO>> GetMiniComponentProductList(PageInput<List<ConditionDTO>> input)
        {
            return await _mini_component_productBus.GetMiniComponentProductDTOListAsync(input);
        }

        /// <summary>
        /// 单个活动商品--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MiniComponentProductDTO> GetMiniComponentProduct(IdInputDTO input)
        {
            return await _mini_component_productBus.GetMiniComponentProductAsync(input) ?? new MiniComponentProductDTO();
        }
        [HttpPost]
        public async Task<PageResult<mini_component_item>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_component_productBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_component_item> GetTheData(IdInputDTO input)
        {
            return await _mini_component_productBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(MiniComponentProductDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_component_productBus.InsertProductDataAsync(data);
            }
            else
            {
                await _mini_component_productBus.UpdateProductDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_component_productBus.DeleteProductDataAsync(ids);
        }

        #endregion
    }
}