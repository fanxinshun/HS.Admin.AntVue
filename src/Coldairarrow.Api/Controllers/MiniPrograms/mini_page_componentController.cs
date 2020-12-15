using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.MiniPrograms
{
    [Route("/MiniPrograms/[controller]/[action]")]
    public class mini_page_componentController : BaseApiController
    {
        #region DI

        public mini_page_componentController(Imini_page_componentBusiness mini_module_componentBus)
        {
            _mini_module_componentBus = mini_module_componentBus;
        }

        Imini_page_componentBusiness _mini_module_componentBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 页面组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<ModuleComponentDTO>> GetModuleComponentList(PageInput<List<ConditionDTO>> input)
        {
            var data = await _mini_module_componentBus.GetModuleComponentListAsync(input);
            return data;
        }

        /// <summary>
        /// 页面组件--单个查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ModuleComponentDTO> GetModuleComponent(IdInputDTO input)
        {
            return await _mini_module_componentBus.GetModuleComponent(input) ?? new ModuleComponentDTO();
        }

        #endregion

        #region 提交

        /// <summary>
        /// 页面组件--新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(ModuleComponentDTO data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _mini_module_componentBus.AddModuleComponentAsync(data);
            }
            else
            {
                await _mini_module_componentBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _mini_module_componentBus.DeleteModuleComponentAsync(ids);
        }

        #endregion
    }
}