using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.MiniPrograms
{
    public interface Imini_programMainBusiness
    {
        Task<List<MiniProguamComponentDTO>> GetMainModuleComponentDTO(MiniProgramMainModuleInput input);
        Task UpdateOldPageData();
    }
    /// <summary>
    /// 数据库查询数据
    /// </summary>
    public class Mini_ProguamComponent : ProductsInfo
    {
        public String component_id { get; set; }
        public String component_type { get; set; }
        public String component_name { get; set; }
        public String description { get; set; }
        public String tag { get; set; }
        public String parent_component_id { get; set; }

    }

    /// <summary>
    /// 小程序首页模块查询参数
    /// </summary>
    public class MiniProgramMainModuleInput
    {
        /// <summary>
        /// 项目地
        /// </summary>
        public String Project_Code { get; set; }

        /// <summary>
        /// 页面类型
        /// </summary>
        public String Page_TypeCode { get; set; }
    }
    /// <summary>
    /// 小程序接口返回数据对象
    /// </summary>
    public class MiniProguamComponentDTO
    {
        public String component_id { get; set; }
        public String component_type { get; set; }
        public String component_name { get; set; }
        public String description { get; set; }
        public String tag { get; set; }
        public String parent_component_id { get; set; }
        public List<ProductsInfo> cons { get; set; }
        public List<MiniProguamComponentDTO> coms { get; set; }

    }

    /// <summary>
    /// 商品详情
    /// </summary>
    public class ProductsInfo
    {
        public String Id { get; set; }

        public String Type { get; set; }

        public String tittle { get; set; }

        public String src { get; set; }

        public String skuid { get; set; }
    }

}
