using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class sys_componentController : BaseApiController
    {
        #region DI

        public sys_componentController(Isys_componentBusiness sys_componentBus)
        {
            _sys_componentBus = sys_componentBus;
        }

        Isys_componentBusiness _sys_componentBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 系统组件下拉框
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectOption>> GetOptionList(OptionListInputDTO input)
        {
            return await _sys_componentBus.GetOptionListAsync(input);
        }

        [HttpPost]
        public async Task<PageResult<sys_component>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _sys_componentBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<sys_component> GetTheData(IdInputDTO input)
        {
            return await _sys_componentBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(sys_component data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _sys_componentBus.AddDataAsync(data);
            }
            else
            {
                await _sys_componentBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _sys_componentBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}