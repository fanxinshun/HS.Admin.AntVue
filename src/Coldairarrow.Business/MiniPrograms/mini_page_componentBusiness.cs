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
    public class mini_page_componentBusiness : BaseBusiness<mini_page_component>, Imini_page_componentBusiness, ITransientDependency
    {
        public mini_page_componentBusiness(IDbAccessor db, Operator op, IMapper mapper)
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

            var q = (from a in Db.GetIQueryable<mini_page>()
                     join b in Db.GetIQueryable<mini_page_component>() on a.Id equals b.Page_Id
                     join c in Db.GetIQueryable<mini_component>() on b.Component_Id equals c.Id
                     join d in Db.GetIQueryable<mini_component_type>() on c.Sys_Component_Id equals d.Id
                     join e in Db.GetIQueryable<mini_page_type>() on a.Page_Type_Id equals e.Id
                     where a.Project_Id == c.Project_Id
                             && a.Project_Id == proj_id
                             && a.Deleted == false
                             && b.Deleted == false
                             && c.Deleted == false
                             && b.Id == input.id
                     select new ModuleComponentDTO()
                     {
                         //页面信息
                         Page_Id = a.Id,
                         Project_Id = a.Project_Id,
                         PageRemark = a.Name,
                         //组件Module
                         Id = b.Id,
                         Sort = b.Sort,
                         Component_Id = b.Component_Id,
                         CreatorId = b.CreatorId,
                         CreateTime = b.CreateTime,
                         //组件信息
                         Parent_Component_Id = c.Parent_Component_Id,
                         Sys_Component_Id = c.Sys_Component_Id,
                         Target_Pages = c.Target_Pages,
                         Tag = c.Tag,
                         Description = c.Description,
                         //组件类型
                         Component_Code = d.Component_Code,
                         Component_Name = d.Component_Name,

                         PageTypeName = e.Type_Name
                     });
            return await q.FirstOrDefaultAsync();
        }
        /// <summary>
        /// 页面组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<ModuleComponentDTO>> GetModuleComponentListAsync(PageInput<List<ConditionDTO>> input)
        {
            var proj_id = _operator?.Property?.Last_Interview_Project;

            var q = from a in Db.GetIQueryable<mini_page>()
                    join b in Db.GetIQueryable<mini_page_component>() on a.Id equals b.Page_Id
                    join c in Db.GetIQueryable<mini_component>() on b.Component_Id equals c.Id
                    join d in Db.GetIQueryable<mini_component_type>() on c.Sys_Component_Id equals d.Id
                    join e in Db.GetIQueryable<mini_page_type>() on a.Page_Type_Id equals e.Id
                    where a.Project_Id == c.Project_Id
                            && a.Project_Id == proj_id
                            && a.Deleted == false
                            && b.Deleted == false
                            && c.Deleted == false
                            && d.Deleted == false
                    orderby a.Sort, b.Sort
                    select new ModuleComponentDTO()
                    {
                        //页面信息
                        Page_Id = a.Id,
                        Project_Id = a.Project_Id,
                        PageRemark = a.Name,
                        //组件Module
                        Id = b.Id,
                        Sort = b.Sort,
                        Component_Id = b.Component_Id,
                        CreatorId = b.CreatorId,
                        CreateTime = b.CreateTime,
                        //组件信息
                        Parent_Component_Id = c.Parent_Component_Id,
                        Sys_Component_Id = c.Sys_Component_Id,
                        Target_Pages = c.Target_Pages,
                        Tag = c.Tag,
                        Description = c.Description,
                        //组件类型
                        Component_Code = d.Component_Code,
                        Component_Name = d.Component_Name,

                        PageTypeName = e.Type_Name
                    };

            var where = LinqHelper.True<ModuleComponentDTO>();
            var search = input.Search;

            //筛选
            search.ForEach(item =>
            {
                if (!item.Condition.IsNullOrEmpty() && !item.Keyword.IsNullOrEmpty())
                {
                    var newWhere = DynamicExpressionParser.ParseLambda<ModuleComponentDTO, bool>(
                        ParsingConfig.Default, false, $@"{item.Condition}.Contains(@0)", item.Keyword);
                    where = where.And(newWhere);
                }
                else
                {
                    if (item.Condition == "Parent_Component_Id")
                    {
                        where = where.And(x => x.Parent_Component_Id == null);
                    }
                }
            });

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
            await Db.InsertAsync(_mapper.Map<mini_page_component>(input));

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
            var list = GetIQueryable().Where(x => ids.Contains(x.Id)).ToList();
            List<string> componentIds = list.Select(x => x.Component_Id).ToList();

            await DeleteAsync(ids);
            await Db.DeleteAsync<mini_component>(componentIds);
            await Db.DeleteAsync<mini_component_swiper>(x => componentIds.Contains(x.Component_Id));
            await Db.DeleteAsync<mini_component_item>(x => componentIds.Contains(x.Component_Id));

            var plist = Db.GetIQueryable<mini_component>().Where(x => componentIds.Contains(x.Parent_Component_Id));
            List<string> pcomponentIds = plist.Select(x => x.Id).ToList();

            await Db.DeleteAsync<mini_component>(pcomponentIds);
            await Db.DeleteAsync<mini_component_swiper>(x => pcomponentIds.Contains(x.Component_Id));
            await Db.DeleteAsync<mini_component_item>(x => pcomponentIds.Contains(x.Component_Id));
        }

        public async Task<PageResult<mini_page_component>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_page_component>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_page_component, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_page_component> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_page_component data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_page_component data)
        {
            await UpdateAsync(data);
        }

        /// <summary>
        /// 修改页面组件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task UpdateDataAsync(ModuleComponentDTO data)
        {
            await Db.UpdateAsync(_mapper.Map<mini_page_component>(data));

            data.Id = data.Component_Id;
            await Db.UpdateAsync(_mapper.Map<mini_component>(data));
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