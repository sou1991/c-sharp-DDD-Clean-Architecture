using Application.Member.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Member.Commands
{
    public interface ICreateMemberCommand
    {
        void Execute(MemberModel memberModel);
    }
}
