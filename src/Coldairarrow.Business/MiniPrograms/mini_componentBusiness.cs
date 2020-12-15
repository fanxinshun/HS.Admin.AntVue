using AutoMapper;
using Coldairarrow.Entity;
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
    public class mini_componentBusiness : BaseBusiness<mini_component>, Imini_componentBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        readonly Operator _operator;

        public mini_componentBusiness(IDbAccessor db, IMapper mapper, Operator op)
            : base(db)
        {
            _mapper = mapper;
            _operator = op;
        }

        #region 外部接口

        /// <summary>
        /// 嵌套组件下拉框数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ComponentTreeDTO>> GetTreeDataListAsync(ComponentTreeInputDTO input)
        {
            var syscom = await Db.GetIQueryable<mini_component_type>()
                .Where(x => x.Component_Code == "coms")
                .Select(y => y.Id)
                .ToListAsync<string>();

            var where = LinqHelper.True<mini_component>();
            if (!input.parentId.IsNullOrEmpty())
                where = where.And(x => x.Parent_Component_Id == input.parentId);

            var list = await GetIQueryable().Where(x => syscom.Contains(x.Sys_Component_Id)).Where(where).ToListAsync();
            var treeList = list
                .Select(x => new ComponentTreeDTO
                {
                    Id = x.Id,
                    ParentId = x.Parent_Component_Id,
                    Text = x.Description,
                    Value = x.Id
                }).ToList();

            return TreeHelper.BuildTree(treeList);
        }

        /// <summary>
        /// 嵌套组件树形结构数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ComponentTreeDTO>> GetTreeDataDetailListAsync(ComponentTreeInputDTO input)
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
                    select new ComponentTreeDTO()
                    {
                        Text = c.Description,
                        Value = c.Id,

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
                        ParentId = c.Parent_Component_Id,
                        Sys_Component_Id = c.Sys_Component_Id,
                        Target_Pages = c.Target_Pages,
                        Tag = c.Tag,
                        Description = c.Description,
                        //组件类型
                        Component_Code = d.Component_Code,
                        Component_Name = d.Component_Name,

                        PageTypeName = e.Type_Name
                    };

            var where = LinqHelper.True<ComponentTreeDTO>();
            if (!input.parentId.IsNullOrEmpty())
                where = where.And(x => x.ParentId == input.parentId);
            if (!input.pageId.IsNullOrEmpty())
                where = where.And(x => x.Page_Id == input.pageId);

            var treeList = await q.Where(where).ToListAsync();

            return TreeHelper.BuildTree(treeList);
        }


        #endregion

        #region 私有成员

        #endregion
    }
}