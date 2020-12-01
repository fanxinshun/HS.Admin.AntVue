using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public class mini_mainpage_moduleBusiness : BaseBusiness<mini_mainpage_module>, Imini_mainpage_moduleBusiness, ITransientDependency
    {
        public mini_mainpage_moduleBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        /// <summary>
        /// 查询分组后的模组(不分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<MainpageModuleTreeDTO>> GetTreeDataListAsync(MainpageModuleTreeInputDTO input)
        {
            var where = LinqHelper.True<mini_mainpage_module>();
            if (!input.parentId.IsNullOrEmpty())
                where = where.And(x => x.Parent_Module_Id == input.parentId);

            var list = await GetIQueryable().Where(where).ToListAsync();
            var treeList = list
                .Select(x => new MainpageModuleTreeDTO
                {
                    Id = x.Id,
                    ParentId = x.Parent_Module_Id,
                    Text = x.Module_Name,
                    Value = x.Id,
                    Project_Id = x.Project_Id,
                    Module_Code = x.Module_Code,
                    Module_Name = x.Module_Name,
                    Sort = x.Sort,
                    Remark = x.Remark
                }).ToList();

            return TreeHelper.BuildTree(treeList);
        }

        /// <summary>
        /// 查询子模组(不分页)
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetChildrenIdsAsync(string moduleId)
        {
            var allNode = await GetIQueryable().Select(x => new TreeModel
            {
                Id = x.Id,
                ParentId = x.Parent_Module_Id,
                Text = x.Module_Name,
                Value = x.Id
            }).ToListAsync();

            var children = TreeHelper
                .GetChildren(allNode, allNode.Where(x => x.Id == moduleId).FirstOrDefault(), true)
                .Select(x => x.Id)
                .ToList();

            return children;
        }

        public async Task<PageResult<mini_mainpage_module>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_mainpage_module>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_mainpage_module, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_mainpage_module> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_mainpage_module data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_mainpage_module data)
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