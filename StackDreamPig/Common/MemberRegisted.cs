using Infrastructure;
using Microsoft.EntityFrameworkCore;
using stackDreamPig.Application.Member.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class MemberRegisted
    {
        public static bool HasRegistMember(MemberModel memberModel)
        {
            var option = new DbContextOptions<DataBaseService>();
            var dataBaseService = new DataBaseService(option);
            var hasMember = dataBaseService.Member
            .Where(p => p.userName == memberModel.userName && p.password == memberModel.password);

            if (hasMember == null) return false; else return true;
        }
    }
}
