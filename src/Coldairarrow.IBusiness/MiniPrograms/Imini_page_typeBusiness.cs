using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_page_typeBusiness
    {
        Task<PageResult<mini_page_type>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_page_type> GetTheDataAsync(string id);
        Task AddDataAsync(mini_page_type data);
        Task UpdateDataAsync(mini_page_type data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);
        Task<List<SelectOption>> GetALLOptionListAsync(OptionListInputDTO input);
        Task<List<SelectPageOption>> GetPagePageTypeOptionListAsync();
    }

    public class SelectPageOption
    {
        public string Type_Code { get; set; }
        public string Type_Name { get; set; }
        public List<SelectOption> Pages { get; set; }
    }
}