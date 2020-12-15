using AutoMapper;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public class mini_component_nestedBusiness : BaseBusiness<mini_component_item>, Imini_component_nestedBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        readonly Operator _operator;
        Imini_page_componentBusiness _mini_page_componentBusiness;
        public mini_component_nestedBusiness(IDbAccessor db, IMapper mapper, Operator op, Imini_page_componentBusiness mini_page_componentBusiness)
            : base(db)
        {
            _mapper = mapper;
            _operator = op;
            _mini_page_componentBusiness = mini_page_componentBusiness;
        }

        #region 外部接口


        /// <summary>
        /// 用户组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<ModuleComponentDTO>> GetMiniComponentNestedDTOListAsync(PageInput<ConditionDTO> input)
        {
            return null;// await _mini_page_componentBusiness.GetModuleComponentListAsync(input);
            //var q = from a in Db.GetIQueryable<mini_component>()
            //        join b in Db.GetIQueryable<mini_component_item>() on a.Id equals b.Component_Id
            //        join c in Db.GetIQueryable<mini_component_type>() on a.Sys_Component_Id equals c.Id
            //        where a.Deleted == false && b.Deleted == false && c.Deleted == false
            //        select new MiniComponentNestedDTO()
            //        {
            //            Description = a.Description,
            //            Parent_Component_Id = a.Parent_Component_Id,
            //            Id = b.Id,
            //            Tittle = b.Tittle,
            //            SKU = b.SKU,
            //            Component_Id = b.Component_Id,
            //            Sort = b.Sort,

            //            Component_Code = c.Component_Code,
            //            Component_Name = c.Component_Name
            //        };
            //var where = LinqHelper.True<MiniComponentNestedDTO>();
            //var search = input.Search;

            ////筛选
            //search.ForEach(item =>
            //{
            //    if (!item.Condition.IsNullOrEmpty() && !item.Keyword.IsNullOrEmpty())
            //    {
            //        var newWhere = DynamicExpressionParser.ParseLambda<MiniComponentNestedDTO, bool>(
            //            ParsingConfig.Default, false, $@"{item.Condition}.Contains(@0)", item.Keyword);
            //        where = where.And(newWhere);
            //    }

            //});

            //return await q.Where(where).GetPageResultAsync(input);
        }


        /// <summary>
        /// 新增嵌套组件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task InsertProductDataAsync(MiniComponentNestedDTO data)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;
            var miniComponent = new mini_component()
            {
                Id = IdHelper.GetId(),
                Project_Id = proj_id,
                Parent_Component_Id = data.Parent_Component_Id,
                Sys_Component_Id = data.Sys_Component_Id,
                Description = data.Tittle,
                Sort = data.Sort,
                CreateTime = DateTime.Now,
                CreatorId = _operator?.UserId
            };
            await Db.InsertAsync<mini_component>(miniComponent);

            data.Component_Id = miniComponent.Id;
            await InsertAsync(_mapper.Map<mini_component_item>(data));
        }
        /// <summary>
        /// 嵌套组件--修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task UpdateProductDataAsync(MiniComponentNestedDTO data)
        {
            data.Image = data.Image.Replace(ConfigHelper.GetValue("FastDFS:FileRootUrl"), "");
            await UpdateAsync(_mapper.Map<mini_component_item>(data));
        }

        /// <summary>
        /// 删除商品组件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Transactional]
        public async Task DeleteProductDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }


        public async Task<PageResult<mini_component_item>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_component_item>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_component_item, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_component_item> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_component_item data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_component_item data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}