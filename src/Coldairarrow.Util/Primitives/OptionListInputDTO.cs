using System.Collections.Generic;

namespace Coldairarrow.Util
{
    public class OptionListInputDTO
    {
        public PageInput<List<ConditionDTO>> pageInput { get; set; }
        public List<string> selectedValues { get; set; }
        public string q { get; set; }
    }
}