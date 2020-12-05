using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public interface Imini_project_userBusiness
    {
        Task<PageResult<mini_project_user>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<mini_project_user> GetTheDataAsync(string id);
        Task AddDataAsync(mini_project_user data);
        Task UpdateDataAsync(mini_project_user data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<UserProjectDTO>> GetUserProjectListAsync(string userId);
    }

    /// <summary>
    /// 用户项目
    /// </summary>
    public class UserProjectDTO
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public String User_Id { get; set; }

        /// <summary>
        /// 项目地编号
        /// </summary>
        public String Project_Code { get; set; }

        /// <summary>
        /// 项目地名称
        /// </summary>
        public String Project_Name { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public String Id { get; set; }

    }
}