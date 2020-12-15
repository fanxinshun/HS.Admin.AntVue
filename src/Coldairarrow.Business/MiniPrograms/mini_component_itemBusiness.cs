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
    public class mini_component_itemBusiness : BaseBusiness<mini_component_item>, Imini_component_itemBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        readonly Operator _operator;
        public mini_component_itemBusiness(IDbAccessor db, IMapper mapper, Operator op)
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
        public async Task<MiniComponentProductDTO> GetMiniComponentProductAsync(IdInputDTO input)
        {
            return await (from a in Db.GetIQueryable<mini_component>()
                          join b in Db.GetIQueryable<mini_component_item>() on a.Id equals b.Component_Id
                          where a.Deleted == false && b.Id == input.id
                          select new MiniComponentProductDTO()
                          {
                              Id = b.Id,
                              SKU = b.SKU,
                              Description = a.Description,
                              Image = ConfigHelper.GetValue("FastDFS:FileRootUrl") + b.Image,
                              Type = b.Type,
                              Tittle = b.Tittle,
                              Component_Id = b.Component_Id,
                              Sort = b.Sort,
                              CreatorId = b.CreatorId,
                              CreateTime = b.CreateTime
                          }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 用户组件列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<MiniComponentProductDTO>> GetMiniComponentProductDTOListAsync(PageInput<List<ConditionDTO>> input)
        {
            var q = from a in Db.GetIQueryable<mini_component>()
                    join b in Db.GetIQueryable<mini_component_item>() on a.Id equals b.Component_Id
                    join c in Db.GetIQueryable<mini_component_type>() on a.Sys_Component_Id equals c.Id
                    join d in Db.GetIQueryable<mini_page_component>() on a.Id equals d.Component_Id
                    where a.Deleted == false && b.Deleted == false && c.Deleted == false
                    select new MiniComponentProductDTO()
                    {
                        Id = b.Id,
                        Tittle = b.Tittle,
                        SKU = b.SKU,
                        Component_Id = b.Component_Id,
                        Sort = b.Sort,

                        Component_Code = c.Component_Code,
                        Component_Name = c.Component_Name,

                        Page_Id = d.Page_Id
                    };
            var where = LinqHelper.True<MiniComponentProductDTO>();
            var search = input.Search;

            //筛选
            search.ForEach(item =>
            {
                if (!item.Condition.IsNullOrEmpty() && !item.Keyword.IsNullOrEmpty())
                {
                    var newWhere = DynamicExpressionParser.ParseLambda<MiniComponentProductDTO, bool>(
                        ParsingConfig.Default, false, $@"{item.Condition}.Contains(@0)", item.Keyword);
                    where = where.And(newWhere);
                }

            });

            return await q.Where(where).GetPageResultAsync(input);
        }


        /// <summary>
        /// 新增活动商品组件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task InsertProductDataAsync(MiniComponentProductDTO data)
        {
            await InsertAsync(_mapper.Map<mini_component_item>(data));
        }
        /// <summary>
        /// 活动商品组件--修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task UpdateProductDataAsync(MiniComponentProductDTO data)
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