using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class sys_attachmentController : BaseApiController
    {
        #region DI

        public sys_attachmentController(Isys_attachmentBusiness sys_attachmentBus)
        {
            _sys_attachmentBus = sys_attachmentBus;
        }

        Isys_attachmentBusiness _sys_attachmentBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<sys_attachment>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _sys_attachmentBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<sys_attachment> GetTheData(IdInputDTO input)
        {
            return await _sys_attachmentBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(sys_attachment data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _sys_attachmentBus.AddDataAsync(data);
            }
            else
            {
                await _sys_attachmentBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _sys_attachmentBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}