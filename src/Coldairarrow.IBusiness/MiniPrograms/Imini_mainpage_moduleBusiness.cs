using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_mainpage_moduleBusiness
    {
        Task<List<MainpageModuleTreeDTO>> GetTreeDataListAsync(MainpageModuleTreeInputDTO input);
        Task<List<string>> GetChildrenIdsAsync(string moduleId);

        Task<PageResult<mini_mainpage_module>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_mainpage_module> GetTheDataAsync(string id);
        Task AddDataAsync(mini_mainpage_module data);
        Task UpdateDataAsync(mini_mainpage_module data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class MainpageModuleTreeInputDTO
    {
        public string parentId { get; set; }
    }
    public class MainpageModuleTreeDTO : TreeModel
    {
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
        public string Project_Id { get; set; }
        public string Module_Code { get; set; }
        public string Module_Name { get; set; }
        public Single? Sort { get; set; }
        public string Remark { get; set; }
    }
}