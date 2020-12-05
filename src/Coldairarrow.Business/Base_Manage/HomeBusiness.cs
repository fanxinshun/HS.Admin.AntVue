using AutoMapper;
using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class HomeBusiness : BaseBusiness<Base_User>, IHomeBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        readonly IMapper _mapper;
        readonly Imini_project_userBusiness _mini_project_userBusiness;
        public HomeBusiness(IDbAccessor db, IOperator @operator, IMapper mapper, Imini_project_userBusiness mini_project_userBusiness)
            : base(db)
        {
            _operator = @operator;
            _mapper = mapper;
            _mini_project_userBusiness = mini_project_userBusiness;
        }

        public async Task<string> SubmitLoginAsync(LoginInputDTO input)
        {
            input.password = input.password.ToMD5String();
            var theUser = await GetIQueryable()
                .Where(x => x.UserName == input.userName && x.Password == input.password)
                .FirstOrDefaultAsync();

            if (theUser.IsNullOrEmpty())
                throw new BusException("账号或密码不正确！");

            List<UserProjectDTO> projusers = await _mini_project_userBusiness.GetUserProjectListAsync(theUser.UserName);
            if (projusers?.Count > 0)
            {
                var defaultProject = projusers.FirstOrDefault(x => x.Id == theUser.Last_Interview_Project)?.Id;
                if (theUser.Last_Interview_Project != defaultProject)
                {
                    theUser.Last_Interview_Project = defaultProject;
                    await UpdateAsync(theUser);
                }
            }

            //生成token,有效期一天
            JWTPayload jWTPayload = new JWTPayload
            {
                UserId = theUser.Id,
                Expire = DateTime.Now.AddDays(1)
            };
            string token = JWTHelper.GetToken(jWTPayload.ToJson(), JWTHelper.JWTSecret);

            return token;
        }

        public async Task ChangePwdAsync(ChangePwdInputDTO input)
        {
            var theUser = _operator.Property;
            if (theUser.Password != input.oldPwd?.ToMD5String())
                throw new BusException("原密码错误!");

            theUser.Password = input.newPwd.ToMD5String();
            await UpdateAsync(_mapper.Map<Base_User>(theUser));
        }
    }
}
