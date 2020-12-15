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
    public class mini_page_typeBusiness : BaseBusiness<mini_page_type>, Imini_page_typeBusiness, ITransientDependency
    {
        public mini_page_typeBusiness(IDbAccessor db, Operator op)
            : base(db)
        {
            _operator = op;
        }
        Operator _operator;
        protected override string _valueField { get; } = "Id";
        protected override string _textField { get => "Type_Name"; }

        #region 外部接口

        public async Task<PageResult<mini_page_type>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_page_type>().And(x => !x.Deleted);
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_page_type, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        /// <summary>
        /// 查询下拉框数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public new async Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;
            var source = GetIQueryable().Where(x => x.Deleted == false);

            input.pageInput = new PageInput<List<ConditionDTO>>() { PageRows = 50 };
            return await GetOptionListAsync(input, _textField, _valueField, source);
        }

        /// <summary>
        /// 查询下拉框数据(包含历史失效的数据)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<SelectOption>> GetALLOptionListAsync(OptionListInputDTO input)
        {
            var source = GetIQueryable();

            input.pageInput = new PageInput<List<ConditionDTO>>() { PageRows = 50 };
            return await GetOptionListAsync(input, "Type_Name", "Type_Code", source);
        }
        /// <summary>
        /// 查询页面和页面类型下拉框数据(包含历史失效的数据)
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectPageOption>> GetPagePageTypeOptionListAsync()
        {
            var source = await (from a in Db.GetIQueryable<mini_page_type>()
                                join b in Db.GetIQueryable<mini_page>() on a.Id equals b.Page_Type_Id
                                select new
                                {
                                    a.Id,
                                    a.Type_Code,
                                    a.Type_Name,
                                    b.Code,
                                    b.Name
                                }).ToListAsync();
            var res = source.GroupBy(g => (g.Id, g.Type_Code, g.Type_Name)).Select(group => new SelectPageOption()
            {
                Type_Code = group.Key.Type_Code,
                Type_Name = group.Key.Type_Name,
                Pages = group.Select(x => new SelectOption() { value = x.Code, text = x.Name }).ToList()

            }).ToList();
            return res;
        }

        public async Task<mini_page_type> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_page_type data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_page_type data)
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