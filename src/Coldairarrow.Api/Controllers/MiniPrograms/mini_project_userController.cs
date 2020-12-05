using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_project_userController : BaseApiController
    {
        #region DI

        public mini_project_userController(Imini_project_userBusiness mini_project_userBus)
        {
            _mini_project_userBus = mini_project_userBus;
        }

        Imini_project_userBusiness _mini_project_userBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<mini_project_user>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_project_userBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_project_user> GetTheData(IdInputDTO input)
        {
            return await _mini_project_userBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_project_user data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_project_userBus.AddDataAsync(data);
            }
            else
            {
                await _mini_project_userBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_project_userBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}