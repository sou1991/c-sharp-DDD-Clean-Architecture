using Application.Member.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Member.Query
{
    public interface ISearchMemberQuary
    {
        int? Execute(LoginModel loginModel);
    }
}
