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
    public class mini_component_swiperBusiness : BaseBusiness<mini_component_swiper>, Imini_component_swiperBusiness, ITransientDependency
    {
        readonly IMapper _mapper;
        readonly Operator _operator;
        public mini_component_swiperBusiness(IDbAccessor db, IMapper mapper, Operator op)
            : base(db)
        {
            _mapper = mapper;
            _operator = op;
        }

        #region 外部接口

        /// <summary>
        /// 单个轮播图文组件--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MiniComponentSwiperDTO> GetMiniComponentSwiperAsync(IdInputDTO input)
        {
            return await (from a in Db.GetIQueryable<mini_component>()
                          join b in Db.GetIQueryable<mini_component_swiper>() on a.Id equals b.Component_Id
                          where a.Deleted == false && b.Id == input.id
                          select new MiniComponentSwiperDTO()
                          {
                              Id = b.Id,
                              Value = b.Value,
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
        /// 轮播图文列表--查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<MiniComponentSwiperDTO>> GetMiniComponentSwiperListAsync(PageInput<List<ConditionDTO>> input)
        {
            var q = from a in Db.GetIQueryable<mini_component>()
                    join b in Db.GetIQueryable<mini_component_swiper>() on a.Id equals b.Component_Id
                    join c in Db.GetIQueryable<mini_component_type>() on a.Sys_Component_Id equals c.Id
                    join d in Db.GetIQueryable<mini_page_component>() on a.Id equals d.Component_Id
                    where a.Deleted == false && b.Deleted == false && c.Deleted == false
                    select new MiniComponentSwiperDTO()
                    {
                        Description = a.Description,
                        Id = b.Id,
                        Value = b.Value,
                        Tittle = b.Tittle,
                        Component_Id = b.Component_Id,
                        Sort = b.Sort,

                        Component_Code = c.Component_Code,
                        Component_Name = c.Component_Name,

                        Page_Id = d.Page_Id
                    };
            var where = LinqHelper.True<MiniComponentSwiperDTO>();
            var search = input.Search;

            //筛选
            search.ForEach(item =>
            {
                if (!item.Condition.IsNullOrEmpty() && !item.Keyword.IsNullOrEmpty())
                {
                    var newWhere = DynamicExpressionParser.ParseLambda<MiniComponentSwiperDTO, bool>(
                        ParsingConfig.Default, false, $@"{item.Condition}.Contains(@0)", item.Keyword);
                    where = where.And(newWhere);
                }

            });

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<PageResult<mini_component_swiper>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_component_swiper>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_component_swiper, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_component_swiper> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        /// 新建轮播图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(MiniComponentSwiperDTO data)
        {
            await InsertAsync(_mapper.Map<mini_component_swiper>(data));
        }

        /// <summary>
        /// 轮播图文--修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(MiniComponentSwiperDTO data)
        {
            data.Image = data.Image.Replace(ConfigHelper.GetValue("FastDFS:FileRootUrl"), "");
            await UpdateAsync(data);
        }
        /// <summary>
        /// 删除轮播图文
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}