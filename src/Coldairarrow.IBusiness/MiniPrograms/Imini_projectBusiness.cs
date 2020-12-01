using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_projectBusiness
    {
        Task<PageResult<mini_project>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_project> GetTheDataAsync(string id);
        Task AddDataAsync(mini_project data);
        Task UpdateDataAsync(mini_project data);
        Task DeleteDataAsync(List<string> ids);
    }
}