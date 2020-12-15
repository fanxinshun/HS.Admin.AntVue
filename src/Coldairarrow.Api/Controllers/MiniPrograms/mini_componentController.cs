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
        /// 嵌套组件下拉框数据--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ComponentTreeDTO>> GetTreeDataList(ComponentTreeInputDTO input)
        {
            return await _mini_componentBus.GetTreeDataListAsync(input);
        }

        /// <summary>
        /// 嵌套组件列表页面--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ComponentTreeDTO>> GetTreeDataDetailList(ComponentTreeInputDTO input)
        {
            return await _mini_componentBus.GetTreeDataDetailListAsync(input);
        }

        #endregion

        #region 提交


        #endregion
    }
}