using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_projectController : BaseApiController
    {
        #region DI

        public mini_projectController(Imini_projectBusiness mini_projectBus)
        {
            _mini_projectBus = mini_projectBus;
        }

        Imini_projectBusiness _mini_projectBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<mini_project>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_projectBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_project> GetTheData(IdInputDTO input)
        {
            return await _mini_projectBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_project data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_projectBus.AddDataAsync(data);
            }
            else
            {
                await _mini_projectBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_projectBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}