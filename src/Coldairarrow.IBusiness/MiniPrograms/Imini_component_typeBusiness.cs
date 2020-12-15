using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_component_typeBusiness
    {
        Task<PageResult<mini_component_type>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_component_type> GetTheDataAsync(string id);
        Task AddDataAsync(mini_component_type data);
        Task UpdateDataAsync(mini_component_type data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);
    }
}