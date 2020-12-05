using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_attachment_moduleController : BaseApiController
    {
        #region DI

        public mini_attachment_moduleController(Imini_attachment_moduleBusiness mini_attachment_moduleBus)
        {
            _mini_attachment_moduleBus = mini_attachment_moduleBus;
        }

        Imini_attachment_moduleBusiness _mini_attachment_moduleBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<mini_attachment_module>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_attachment_moduleBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_attachment_module> GetTheData(IdInputDTO input)
        {
            return await _mini_attachment_moduleBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_attachment_module data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_attachment_moduleBus.AddDataAsync(data);
            }
            else
            {
                await _mini_attachment_moduleBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_attachment_moduleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}