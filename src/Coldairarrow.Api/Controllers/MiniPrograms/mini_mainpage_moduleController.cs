using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_mainpage_moduleController : BaseApiController
    {
        #region DI

        public mini_mainpage_moduleController(Imini_mainpage_moduleBusiness mini_mainpage_moduleBus)
        {
            _mini_mainpage_moduleBus = mini_mainpage_moduleBus;
        }

        Imini_mainpage_moduleBusiness _mini_mainpage_moduleBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<mini_mainpage_module>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_mainpage_moduleBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_mainpage_module> GetTheData(IdInputDTO input)
        {
            return await _mini_mainpage_moduleBus.GetTheDataAsync(input.id);
        }

        [HttpPost]
        public async Task<List<MainpageModuleTreeDTO>> GetTreeDataList(MainpageModuleTreeInputDTO input)
        {
            return await _mini_mainpage_moduleBus.GetTreeDataListAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_mainpage_module data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_mainpage_moduleBus.AddDataAsync(data);
            }
            else
            {
                await _mini_mainpage_moduleBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_mainpage_moduleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}