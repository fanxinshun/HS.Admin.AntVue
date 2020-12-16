using AutoMapper;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.IBusiness.MiniPrograms;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public class mini_programMainBusiness : BaseBusiness<mini_page>, Imini_programMainBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        readonly Operator _operator;
        public mini_programMainBusiness(IDbAccessor db, IMapper mapper, Operator op)
            : base(db)
        {
            _mapper = mapper;
            _operator = op;
        }

        /// <summary>
        /// 更新旧首页数据
        /// </summary>
        /// <returns></returns>
        [Transactional]
        public async Task UpdateOldPageData()
        {
            await Db.DeleteAllAsync<mini_page_type>();
            await Db.DeleteAllAsync<mini_page>();

            var dTime = DateTime.Now;

            var mini_page_type = new List<mini_page_type>();
            mini_page_type.Add(new mini_page_type()
            {
                Id = "1",
                Type_Code = "1",
                Type_Name = "单品详情页",
                Remark = "SKU ID",
                Sort = 1,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });
            mini_page_type.Add(new mini_page_type()
            {
                Id = "2",
                Type_Code = "2",
                Type_Name = "搜索页",
                Remark = "参数值",
                Sort = 2,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });
            mini_page_type.Add(new mini_page_type()
            {
                Id = "3",
                Type_Code = "3",
                Type_Name = "活动频道静态页",
                Remark = "URL",
                Sort = 3,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });
            mini_page_type.Add(new mini_page_type()
            {
                Id = "4",
                Type_Code = "4",
                Type_Name = "图片展示",
                Remark = "URL",
                Sort = 4,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });
            mini_page_type.Add(new mini_page_type()
            {
                Id = "5",
                Type_Code = "5",
                Type_Name = "我的优惠券",
                Remark = "我的优惠券",
                Sort = 5,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });
            mini_page_type.Add(new mini_page_type()
            {
                Id = "6",
                Type_Code = "6",
                Type_Name = "领券活动",
                Remark = "活动 ID",
                Sort = 6,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });
            mini_page_type.Add(new mini_page_type()
            {
                Id = "7",
                Type_Code = "7",
                Type_Name = "自定义链接",
                Remark = "自定义链接",
                Sort = 7,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            });

            //页面类型
            int sort = 8;//顺序从第8个开始
            var pageType = await Db.GetIQueryable<app_page_type>().OrderBy(x => x.num).ToListAsync();
            for (int i = 0; i < pageType.Count; i++)
            {
                var item = new mini_page_type()
                {
                    Id = (pageType[i].num + 7).ToString(),
                    Type_Code = (pageType[i].num + 7).ToString(),
                    Type_Name = pageType[i].name,
                    Sort = sort++,
                    CreatorId = "Sys",
                    CreateTime = dTime,
                    Deleted = true
                };
                if (item.Id == "8" || item.Id == "9" || item.Id == "10" || item.Id == "12")
                {
                    item.Remark = "模版页";
                }
                if (item.Id == "15" || item.Id == "44")
                {
                    item.Remark = "活动编号";
                }
                mini_page_type.Add(item);
            }

            int j = 0;
            //页面
            var page = await Db.GetIQueryable<app_page>().ToListAsync();
            var mini_page = page.Select(x => new mini_page()
            {
                Id = x.id.ToString(),
                Page_Type_Id = mini_page_type.FirstOrDefault(a => a.Id == (x.page_type + 7).ToString())?.Id,
                Code = x.id.ToString(),
                Name = x.page_name,
                Sort = j++,
                CreatorId = "Sys",
                CreateTime = dTime,
                Deleted = true
            }).ToList();
            await Db.InsertAsync<mini_page_type>(mini_page_type);
            await Db.InsertAsync<mini_page>(mini_page);
        }

        /// <summary>
        /// 小程序接口数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<MiniProguamComponentDTO>> GetMainModuleComponentDTO(MiniProgramMainModuleInput input)
        {

            var sql = @"SELECT
	t2.Component_Id,
	t3.Description,
	t3.Tag,
	t3.Parent_Component_Id,
	t4.Component_Code AS component_type,
	t4.Component_Name,
	t5.Type,
	t5.Tittle,
	t5.Image AS src,
	t5.Value AS skuid
FROM
	mini_page_type t0
	INNER JOIN mini_page t1 ON t1.Page_Type_Id = t0.Id
	INNER JOIN mini_project t11 ON t1.Project_Id = t11.Id
	INNER JOIN mini_page_component t2 ON t2.Page_Id = t1.Id
	INNER JOIN mini_component t3 ON t3.Id = t2.Component_Id
	INNER JOIN mini_component_type t4 ON t4.Id = t3.Sys_Component_Id 
	LEFT JOIN mini_component_swiper t5 ON t5.Component_Id = t3.Id 
WHERE
	t0.Deleted = 0 
	AND t1.Deleted = 0 
	AND t11.Deleted = 0 
	AND t2.Deleted = 0 
	AND t3.Deleted = 0 
	AND t4.Deleted = 0 
    AND t11.Project_Code = @Project_Code
    AND t0.Type_Code = @Type_Code
ORDER BY
	t3.Sort
";
            //首页组件
            var moduleComponentList = await Db.GetListBySqlAsync<Mini_ProguamComponent>(sql,
                ("@Project_Code", input.Project_Code),
                ("@Type_Code", input.Page_TypeCode));
            var data = moduleComponentList
                .GroupBy(g => new { g.component_id, g.component_type, g.component_name, g.description, g.tag, g.parent_component_id })
                .Select(group => new MiniProguamComponentDTO()
                {
                    component_id = group.Key.component_id,
                    component_type = group.Key.component_type,
                    component_name = group.Key.component_name,
                    description = group.Key.description,
                    parent_component_id = group.Key.parent_component_id,
                    tag = group.Key.tag,
                    cons = group.Select(x => new ProductsInfo()
                    {
                        Type = x.Type,
                        tittle = x.tittle,
                        src = x.src,
                        skuid = x.skuid
                    }).ToList()
                }).ToList();

            //嵌套数据
            data.ForEach(item =>
            {
                item.coms = data.FindAll(d => item.component_id == d.parent_component_id);
            });
            var newdata = data.FindAll(x => x.parent_component_id == null);

            return newdata.ToList();
        }

    }
}
