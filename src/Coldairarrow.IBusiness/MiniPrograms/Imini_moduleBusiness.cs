using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_moduleBusiness
    {
        Task<mini_module> GetTheDataAsync(string id);
        Task AddDataAsync(mini_module data);
        Task UpdateDataAsync(mini_module data);
        Task DeleteDataAsync(List<string> ids);
        Task<PageResult<mini_module>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);
    }
}