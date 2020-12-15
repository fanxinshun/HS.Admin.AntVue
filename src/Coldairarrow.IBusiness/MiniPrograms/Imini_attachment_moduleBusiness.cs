using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_attachment_moduleBusiness
    {
        Task<PageResult<mini_attachment_module>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_attachment_module> GetTheDataAsync(string id);
        Task AddDataAsync(mini_attachment_module data);
        Task UpdateDataAsync(mini_attachment_module data);
        Task DeleteDataAsync(List<string> ids);
    }
}