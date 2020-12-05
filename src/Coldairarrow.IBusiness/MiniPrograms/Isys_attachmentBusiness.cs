using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Isys_attachmentBusiness
    {
        Task<PageResult<sys_attachment>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<sys_attachment> GetTheDataAsync(string id);
        Task AddDataAsync(sys_attachment data);
        Task UpdateDataAsync(sys_attachment data);
        Task DeleteDataAsync(List<string> ids);
    }
}