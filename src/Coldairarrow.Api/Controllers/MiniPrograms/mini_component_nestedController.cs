using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_component_nestedController : BaseApiController
    {
        #region DI

        public mini_component_nestedController(Imini_component_nestedBusiness mini_component_nestedBus)
        {
            _mini_component_nestedBus = mini_component_nestedBus;
        }

        Imini_component_nestedBusiness _mini_component_nestedBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<ModuleComponentDTO>> GetMiniComponentNestedDTOList(PageInput<ConditionDTO> input)
        {
            return await _mini_component_nestedBus.GetMiniComponentNestedDTOListAsync(input);
        }

        /// <summary>
        /// 单个--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MiniComponentNestedDTO> GetMiniComponentNested(IdInputDTO input)
        {
            return null;// await _mini_component_nestedBus.GetMiniComponentNested(input) ?? new MiniComponentNestedDTO();
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(MiniComponentNestedDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_component_nestedBus.InsertProductDataAsync(data);
            }
            else
            {
                await _mini_component_nestedBus.UpdateProductDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_component_nestedBus.DeleteProductDataAsync(ids);
        }

        #endregion
    }
}