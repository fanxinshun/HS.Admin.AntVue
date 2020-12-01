using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_mainpage_productController : BaseApiController
    {
        #region DI

        public mini_mainpage_productController(Imini_mainpage_productBusiness mini_mainpage_productBus)
        {
            _mini_mainpage_productBus = mini_mainpage_productBus;
        }

        Imini_mainpage_productBusiness _mini_mainpage_productBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<MainpageProductDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _mini_mainpage_productBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<mini_mainpage_product> GetTheData(IdInputDTO input)
        {
            return await _mini_mainpage_productBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(mini_mainpage_product data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_mainpage_productBus.AddDataAsync(data);
            }
            else
            {
                await _mini_mainpage_productBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_mainpage_productBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}