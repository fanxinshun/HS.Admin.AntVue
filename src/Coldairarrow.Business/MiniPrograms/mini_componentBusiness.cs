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
        /// 单个用户组件--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MiniComponentDTO> GetMiniComponentAsync(IdInputDTO input)
        {
            var entityList = await (from a in Db.GetIQueryable<mini_component>()
                                    join b in Db.GetIQueryable<sys_component>() on a.Sys_Component_Id equals b.Id
                                    where a.Deleted == false && b.Deleted == false && a.Id == input.id
                                    select new MiniComponentDTO()
                                    {
                                        Id = a.Id,
                                        Project_Id = a.Project_Id,
                                        Sys_Component_Id = a.Sys_Component_Id,
                                        Target_Pages = a.Target_Pages,
                                        Description = a.Description,
                                        Component_Name = b.Component_Name
                                    }).ToListAsync();
            var entity = entityList.FirstOrDefault();

            //查询组件图片
            if (!entity.IsNullOrEmpty())
            {
                var images = await Db.GetIQueryable<mini_attachment_module>()
                    .Where(x => x.Component_Id == input.id)
                    .ToListAsync();
                var fileRootUrl = ConfigHelper.GetValue("FastDFS:FileRootUrl");
                entity.Images = images.Select(x => fileRootUrl + x.Image_Path).ToList();
            }
            return entity;
        }
        /// <summary>
        /// 用户组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<MiniComponentDTO>> GetMiniComponentDTOListAsync(PageInput<ConditionDTO> input)
        {
            var q = from a in Db.GetIQueryable<mini_component>()
                    join b in Db.GetIQueryable<sys_component>() on a.Sys_Component_Id equals b.Id
                    where a.Deleted == false && b.Deleted == false
                    select new MiniComponentDTO()
                    {
                        Id = a.Id,
                        Project_Id = a.Project_Id,
                        Sys_Component_Id = a.Sys_Component_Id,
                        Target_Pages = a.Target_Pages,
                        Description = a.Description,
                        Component_Name = b.Component_Name
                    };
            var where = LinqHelper.True<MiniComponentDTO>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<MiniComponentDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<PageResult<mini_component>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_component>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_component, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_component> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_component data)
        {
            await InsertAsync(data);
        }
        /// <summary>
        /// 添加用户组件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddMiniComponentAsync(MiniComponentDTO data)
        {
            await Db.InsertAsync(_mapper.Map<mini_component>(data));
            if (!data.Images.IsNullOrEmpty() && data.Images.Count > 0)
            {
                List<mini_attachment_module> images = data.Images.Select(x => new mini_attachment_module()
                {
                    Id = IdHelper.GetId(),
                    Component_Id = data.Id,
                    Image_Path = x,
                    CreatorId = _operator?.UserId,
                    CreateTime = DateTime.Now
                }).ToList();
                await Db.InsertAsync<mini_attachment_module>(images);
            }
        }
        /// <summary>
        /// 修改用户组件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateMiniComponentAsync(MiniComponentDTO data)
        {
            await Db.UpdateAsync(_mapper.Map<mini_component>(data));

            await Db.DeleteAsync<mini_attachment_module>(x => x.Component_Id == data.Id);
            List<mini_attachment_module> images = data.Images.Select(x => new mini_attachment_module()
            {
                Id = IdHelper.GetId(),
                Component_Id = data.Id,
                Image_Path = x.Replace(ConfigHelper.GetValue("FastDFS:FileRootUrl"), ""),
                CreatorId = _operator?.UserId,
                CreateTime = DateTime.Now
            }).ToList();
            await Db.InsertAsync<mini_attachment_module>(images);
        }
        public async Task UpdateDataAsync(mini_component data)
        {
            await UpdateAsync(data);
        }
        /// <summary>
        /// 删除用户组件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteMiniComponentAsync(List<string> ids)
        {
            await DeleteAsync(ids);
            await Db.DeleteAsync<mini_attachment_module>(x => ids.Contains(x.Component_Id));
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