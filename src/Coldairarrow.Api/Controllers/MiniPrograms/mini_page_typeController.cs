using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_page_typeController : BaseApiController
    {
        #region DI

        public mini_page_typeController(Imini_page_typeBusiness mini_pageBus)
        {
            _mini_pageBus = mini_pageBus;
        }

        Imini_page_typeBusiness _mini_pageBus { get; }

        #endregion

        #region 获取
        /// <summary>
        /// 页面类型下拉框
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectOption>> GetOptionList(OptionListInputDTO input)
        {
            return await _mini_pageBus.GetOptionListAsync(input);
        }

        [HttpPost]
        public async Task<PageResult<mini_page_type>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_pageBus.GetDataListAsync(input);
        }
        /// <summary>
        /// 页面类型下拉框(包含历史失效的数据)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectOption>> GetALLOptionListAsync(OptionListInputDTO input)
        {
            return await _mini_pageBus.GetALLOptionListAsync(input);
        }
        /// <summary>
        /// 页面和页面类型下拉框(包含历史失效的数据)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<SelectPageOption>> GetPagePageTypeOptionList()
        {
            return await _mini_pageBus.GetPagePageTypeOptionListAsync();
        }

        [HttpPost]
        public async Task<mini_page_type> GetTheData(IdInputDTO input)
        {
            return await _mini_pageBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_page_type data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_pageBus.AddDataAsync(data);
            }
            else
            {
                await _mini_pageBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_pageBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}