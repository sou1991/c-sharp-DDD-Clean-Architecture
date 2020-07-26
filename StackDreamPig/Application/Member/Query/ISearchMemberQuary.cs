using Application.Member.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Member.Query
{
    public interface ISearchMemberQuary
    {
        MemberModel Execute(MemberModel memberModel);
    }
}
