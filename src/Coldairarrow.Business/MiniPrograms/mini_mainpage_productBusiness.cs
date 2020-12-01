using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public class mini_mainpage_productBusiness : BaseBusiness<mini_mainpage_product>, Imini_mainpage_productBusiness, ITransientDependency
    {
        public mini_mainpage_productBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        /// <summary>
        /// 查询活动商品（分页）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<MainpageProductDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = from a in GetIQueryable()
                    join b in Db.GetIQueryable<mini_mainpage_module>() on a.Module_Id equals b.Id
                    select new MainpageProductDTO
                    {
                        ModuleName = b.Module_Name,
                        Id = a.Id,
                        Module_Id = a.Module_Id,
                        Commodities_Id = a.Commodities_Id,
                        Attachment_Id = a.Attachment_Id,
                        Description = a.Description,
                        Sort = a.Sort,
                        CreatorId = a.CreatorId,
                        CreateTime = a.CreateTime
                    };

            var search = input.Search;
            var whereExp = LinqHelper.True<MainpageProductDTO>();

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<MainpageProductDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                whereExp = whereExp.And(newWhere);
            }

            return await q.Where(whereExp).GetPageResultAsync(input);
        }

        /// <summary>
        /// 查询分组后的产品(不分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<MainpageProductTreeDTO>> GetTreeDataListAsync(MainpageProductTreeInputDTO input)
        {
            var q = from a in GetIQueryable()
                    join b in Db.GetIQueryable<mini_mainpage_module>() on a.Module_Id equals b.Id
                    select new MainpageProductDTO
                    {
                        ModuleName = b.Module_Name,
                        Id = a.Id,
                        Module_Id = a.Module_Id,
                        Parent_Module_Id = b.Parent_Module_Id,
                        Commodities_Id = a.Commodities_Id,
                        Attachment_Id = a.Attachment_Id,
                        Description = a.Description,
                    };
            var list = await q.ToListAsync();
            var treeList = list.Select(x => new MainpageProductTreeDTO
            {
                Id = x.Id,
                ParentId = x.Parent_Module_Id,
                Module_Id = x.Module_Id,
                ModuleName = x.ModuleName,
                Commodities_Id = x.Commodities_Id,
                Attachment_Id = x.Attachment_Id,
                Description = x.Description

            }).ToList();

            return TreeHelper.BuildTree(treeList);
        }

        /// <summary>
        /// 查询子模组产品(不分页)
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<MainpageProductTreeDTO>> GetChildrenIdsAsync(string moduleId)
        {
            var q = from a in GetIQueryable()
                    join b in Db.GetIQueryable<mini_mainpage_module>() on a.Module_Id equals b.Id
                    select new MainpageProductTreeDTO
                    {
                        ModuleName = b.Module_Name,
                        Id = a.Id,
                        Module_Id = a.Module_Id,
                        ParentId = b.Parent_Module_Id,
                        Commodities_Id = a.Commodities_Id,
                        Attachment_Id = a.Attachment_Id,
                        Description = a.Description,
                    };
            var list = await q.ToListAsync();

            var children = TreeHelper
                .GetChildren(list, list.Where(x => x.Id == moduleId).FirstOrDefault(), false)
                .ToList();

            return children;
        }

        public async Task<mini_mainpage_product> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_mainpage_product data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_mainpage_product data)
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