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
    public class mini_pageBusiness : BaseBusiness<mini_page>, Imini_pageBusiness, ITransientDependency
    {
        public mini_pageBusiness(IDbAccessor db, Operator op)
            : base(db)
        {
            _operator = op;
        }

        #region 外部接口

        Operator _operator;
        protected override string _valueField { get; } = "Id";
        protected override string _textField { get => "Name"; }


        /// <summary>
        /// 页面列表--查询(分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<Mini_PageDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;

            var q = from a in GetIQueryable()
                    join b in Db.GetIQueryable<mini_page_type>() on a.Page_Type_Id equals b.Id
                    where a.Project_Id == proj_id && a.Deleted == false && b.Deleted == false
                    select new Mini_PageDTO
                    {
                        Id = a.Id,
                        Page_Type_Id = a.Page_Type_Id,
                        Code = a.Code,
                        Name = a.Name,
                        Sort = a.Sort,
                        PageTypeName = b.Type_Name
                    };

            var where = LinqHelper.True<Mini_PageDTO>();

            var search = input.Search;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Mini_PageDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_page> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }
        /// <summary>
        /// 查询页面列表--下拉框数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public new async Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;
            var source = GetIQueryable().Where(x => (x.Project_Id == proj_id && x.Deleted == false));

            return await GetOptionListAsync(input, _textField, _valueField, source);
        }

        /// <summary>
        /// 查询页面列表--下拉框数据(包含历史失效的数据)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<SelectOption>> GetALLOptionList(OptionListInputDTO input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;
            var source = GetIQueryable().Where(x => (x.Project_Id == proj_id && x.Deleted == false) || (x.Project_Id == null && x.Deleted == true));

            return await GetOptionListAsync(input, "Name", "Code", source);
        }
        public async Task AddDataAsync(mini_page data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_page data)
        {
            await UpdateAsync(data);
        }
        /// <summary>
        /// 删除页面
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Transactional]
        public async Task DeleteDataAsync(List<string> ids)
        {
            //删除页面
            await DeleteAsync(ids);
            //查询页面组件
            var list = Db.GetIQueryable<mini_page_component>().Where(x => ids.Contains(x.Page_Id));
            List<string> componentIds = list.Select(x => x.Component_Id).ToList();

            //根据页面组件删除关联信息
            await Db.DeleteAsync<mini_page_component>(x => ids.Contains(x.Page_Id));
            await Db.DeleteAsync<mini_component>(componentIds);
            await Db.DeleteAsync<mini_component_swiper>(x => componentIds.Contains(x.Component_Id));
            await Db.DeleteAsync<mini_component_item>(x => componentIds.Contains(x.Component_Id));
        }

        #endregion

        #region 私有成员

        #endregion
    }
}