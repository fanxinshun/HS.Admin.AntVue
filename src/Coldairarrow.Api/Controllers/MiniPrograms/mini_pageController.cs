using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_pageController : BaseApiController
    {
        #region DI

        public mini_pageController(Imini_pageBusiness mini_moduleBus)
        {
            _mini_moduleBus = mini_moduleBus;
        }

        Imini_pageBusiness _mini_moduleBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 页面管理--查询(分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Mini_PageDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_moduleBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 页面管理下拉框
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input)
        {
            return await _mini_moduleBus.GetOptionListAsync(input);
        }
        /// <summary>
        /// 页面管理下拉框(包含历史失效的数据)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectOption>> GetALLOptionList(OptionListInputDTO input)
        {
            return await _mini_moduleBus.GetALLOptionList(input);
        }


        [HttpPost]
        public async Task<mini_page> GetTheData(IdInputDTO input)
        {
            return await _mini_moduleBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_page data)
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

        /// <summary>
        /// 删除页面
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_moduleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}