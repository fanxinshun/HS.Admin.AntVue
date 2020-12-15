using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_pageBusiness
    {
        Task<mini_page> GetTheDataAsync(string id);
        Task AddDataAsync(mini_page data);
        Task UpdateDataAsync(mini_page data);
        Task DeleteDataAsync(List<string> ids);
        Task<PageResult<Mini_PageDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);
        Task<List<SelectOption>> GetALLOptionList(OptionListInputDTO input);

    }

    public class Mini_PageDTO : mini_page
    {
        public string PageTypeName { get; set; }
    }
}