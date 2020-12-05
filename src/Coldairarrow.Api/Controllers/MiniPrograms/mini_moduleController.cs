using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_moduleController : BaseApiController
    {
        #region DI

        public mini_moduleController(Imini_moduleBusiness mini_moduleBus)
        {
            _mini_moduleBus = mini_moduleBus;
        }

        Imini_moduleBusiness _mini_moduleBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 页面管理--查询(分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<mini_module>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_moduleBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 页面管理下拉框
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectOption>> GetOptionList(OptionListInputDTO input)
        {
            return await _mini_moduleBus.GetOptionListAsync(input);
        }

        [HttpPost]
        public async Task<mini_module> GetTheData(IdInputDTO input)
        {
            return await _mini_moduleBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_module data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_moduleBus.AddDataAsync(data);
            }
            else
            {
                await _mini_moduleBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_moduleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}