using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Isys_componentBusiness
    {
        Task<PageResult<sys_component>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<sys_component> GetTheDataAsync(string id);
        Task AddDataAsync(sys_component data);
        Task UpdateDataAsync(sys_component data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);
    }
}