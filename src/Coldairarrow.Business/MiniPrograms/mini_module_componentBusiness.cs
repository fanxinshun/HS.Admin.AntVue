using AutoMapper;
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
    public class mini_module_componentBusiness : BaseBusiness<mini_module_component>, Imini_module_componentBusiness, ITransientDependency
    {
        public mini_module_componentBusiness(IDbAccessor db, Operator op, IMapper mapper)
            : base(db)
        {
            _operator = op;
            _mapper = mapper;
        }

        readonly IMapper _mapper;
        Operator _operator;

        #region 外部接口

        /// <summary>
        /// 页面组件--单个查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ModuleComponentDTO> GetModuleComponent(IdInputDTO input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;

            var q = (from a in Db.GetIQueryable<mini_module>()
                     join b in Db.GetIQueryable<mini_module_component>() on a.Id equals b.Module_Id
                     join c in Db.GetIQueryable<mini_component>() on b.Component_Id equals c.Id
                     where a.Project_Id == c.Project_Id
                             && a.Project_Id == proj_id
                             && a.Deleted == false
                             && b.Deleted == false
                             && c.Deleted == false
                             && b.Id == input.id
                     orderby a.Sort
                     select new ModuleComponentDTO()
                     {
                         Module_Id = a.Id,
                         Project_Id = a.Project_Id,
                         Module_Code = a.Module_Code,
                         Module_Name = a.Module_Name,
                         Remark = a.Remark,

                         Id = b.Id,
                         Sort = b.Sort,
                         Component_Id = b.Component_Id,

                         Sys_Component_Id = c.Sys_Component_Id,
                         Target_Pages = c.Target_Pages,
                         Description = c.Description
                     });
            return await q.FirstOrDefaultAsync();
        }
        /// <summary>
        /// 页面组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<ModuleComponentDTO>> GetModuleComponentListAsync(PageInput<ConditionDTO> input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;

            var q = from a in Db.GetIQueryable<mini_module>()
                    join b in Db.GetIQueryable<mini_module_component>() on a.Id equals b.Module_Id
                    join c in Db.GetIQueryable<mini_component>() on b.Component_Id equals c.Id
                    join d in Db.GetIQueryable<sys_component>() on c.Sys_Component_Id equals d.Id
                    where a.Project_Id == c.Project_Id
                            && a.Project_Id == proj_id
                            && a.Deleted == false
                            && b.Deleted == false
                            && c.Deleted == false
                            && d.Deleted == false
                    orderby a.Sort, b.Sort
                    select new ModuleComponentDTO()
                    {
                        Module_Id = a.Id,
                        Project_Id = a.Project_Id,
                        Module_Code = a.Module_Code,
                        Module_Name = a.Module_Name,
                        Remark = a.Remark,

                        Id = b.Id,
                        Sort = b.Sort,
                        Component_Id = b.Component_Id,

                        Sys_Component_Id = c.Sys_Component_Id,
                        Target_Pages = c.Target_Pages,
                        Description = c.Description,

                        Component_Name = d.Component_Name
                    };

            var where = LinqHelper.True<ModuleComponentDTO>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<ModuleComponentDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        /// <summary>
        /// 页面组件--新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Transactional]
        public async Task AddModuleComponentAsync(ModuleComponentDTO input)
        {
            input.Id = IdHelper.GetId();
            input.Component_Id = IdHelper.GetId();
            await Db.InsertAsync(_mapper.Map<mini_module_component>(input));

            input.Id = input.Component_Id;
            await Db.InsertAsync(_mapper.Map<mini_component>(input));
        }
        /// <summary>
        /// 页面组件--删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Transactional]
        public async Task DeleteModuleComponentAsync(List<string> ids)
        {
            var list = GetIQueryable().Where(x => ids.Contains(x.Id));
            List<string> componentIds = list.Select(x => x.Component_Id).ToList();

            await Db.DeleteAsync<mini_module_component>(ids);
            await Db.DeleteAsync<mini_component>(componentIds);
        }

        public async Task<PageResult<mini_module_component>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_module_component>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_module_component, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_module_component> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_module_component data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_module_component data)
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