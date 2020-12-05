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
    public class mini_moduleBusiness : BaseBusiness<mini_module>, Imini_moduleBusiness, ITransientDependency
    {
        public mini_moduleBusiness(IDbAccessor db, Operator op)
            : base(db)
        {
            _operator = op;
        }

        #region 外部接口

        Operator _operator;

        protected override string _valueField { get; } = "Id";
        protected override string _textField { get => "Module_Name"; }

        /// <summary>
        /// 页面列表--查询(分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<mini_module>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;
            var q = GetIQueryable();

            var where = LinqHelper.True<mini_module>();
            where = where.And(x => x.Project_Id == proj_id);

            var search = input.Search;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_module, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_module> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_module data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_module data)
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