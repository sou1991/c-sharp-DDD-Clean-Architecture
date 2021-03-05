using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Member
{
    public interface IMemberRepository
    {
        MemberEntity FindSingle(string target);

        MemberEntity Find(string targetName, string targetPass);

        MemberEntity GetSecurePassword(string target);

        void Create(MemberEntity memberEntity);

        void Save();
    }
}
