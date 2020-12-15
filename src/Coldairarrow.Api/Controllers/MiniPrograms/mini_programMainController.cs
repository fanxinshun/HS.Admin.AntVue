using Coldairarrow.IBusiness.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_programMainController : BaseController //BaseApiController
    {
        public mini_programMainController(Imini_programMainBusiness mini_programMain)
        {
            _mini_programMain = mini_programMain;
        }

        Imini_programMainBusiness _mini_programMain { get; }

        /// <summary>
        /// 查询首页模块数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MiniProguamComponentDTO>> GetMainModule(MiniProgramMainModuleInput input)
        {
            return await _mini_programMain.GetMainModuleComponentDTO(input);
        }

        /// <summary>
        /// 执行数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateOldPageData()
        {
            await _mini_programMain.UpdateOldPageData();
        }


    }
}
