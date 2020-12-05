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
    public class mini_project_userBusiness : BaseBusiness<mini_project_user>, Imini_project_userBusiness, ITransientDependency
    {
        public mini_project_userBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<mini_project_user>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<mini_project_user>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<mini_project_user, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<mini_project_user> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(mini_project_user data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_project_user data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task<List<UserProjectDTO>> GetUserProjectListAsync(string userId)
        {
            var q = from a in GetIQueryable()
                    join b in Db.GetIQueryable<mini_project>() on a.Project_Id equals b.Id
                    where a.Deleted == false
                       && a.User_Id == userId
                       && a.Deleted == false
                       && b.Deleted == false
                    select new UserProjectDTO
                    {
                        Project_Code = b.Project_Code,
                        Project_Name = b.Project_Name,
                        Id = a.Project_Id,
                        User_Id = a.User_Id
                    };

            return await q.ToListAsync();
        }

        #endregion

        #region 私有成员

        #endregion
    }
}