using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_mainpage_productBusiness
    {
        Task<List<MainpageProductTreeDTO>> GetTreeDataListAsync(MainpageProductTreeInputDTO input);
        Task<List<MainpageProductTreeDTO>> GetChildrenIdsAsync(string moduleId);
        Task<PageResult<MainpageProductDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_mainpage_product> GetTheDataAsync(string id);
        Task AddDataAsync(mini_mainpage_product data);
        Task UpdateDataAsync(mini_mainpage_product data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class MainpageProductDTO : mini_mainpage_product
    {
        /// <summary>
        /// 模组名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 父模组Id
        /// </summary>
        public String Parent_Module_Id { get; set; }

        ///// <summary>
        ///// 商品名称
        ///// </summary>
        //public string CommoditiesName { get; set; }

        //public string PictureName { get; set; }
    }

    public class MainpageProductTreeInputDTO
    {
        public string parentId { get; set; }
    }
    public class MainpageProductTreeDTO : TreeModel
    {
        public object children { get => Children; }

        /// <summary>
        /// 自然主键
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 模组
        /// </summary>
        public String Module_Id { get; set; }
        /// <summary>
        /// 模组名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public String Commodities_Id { get; set; }

        /// <summary>
        /// 商品活动图片
        /// </summary>
        public String Attachment_Id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Description { get; set; }


    }
}