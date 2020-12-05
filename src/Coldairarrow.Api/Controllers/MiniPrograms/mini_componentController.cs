using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_componentController : BaseApiController
    {
        #region DI

        public mini_componentController(Imini_componentBusiness mini_componentBus)
        {
            _mini_componentBus = mini_componentBus;
        }

        Imini_componentBusiness _mini_componentBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 用户组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<MiniComponentDTO>> GetMiniComponentList(PageInput<ConditionDTO> input)
        {
            return await _mini_componentBus.GetMiniComponentDTOListAsync(input);
        }

        /// <summary>
        /// 单个用户组件--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MiniComponentDTO> GetMiniComponent(IdInputDTO input)
        {
            return await _mini_componentBus.GetMiniComponentAsync(input);
        }

        [HttpPost]
        public async Task<PageResult<mini_component>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_componentBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_component> GetTheData(IdInputDTO input)
        {
            return await _mini_componentBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(MiniComponentDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_componentBus.AddMiniComponentAsync(data);
            }
            else
            {
                await _mini_componentBus.UpdateMiniComponentAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_componentBus.DeleteMiniComponentAsync(ids);
        }

        #endregion
    }
}