using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Member
{
    public interface IMemberRepository
    {
        IQueryable<MemberEntity> FindSingle(string target);

        IQueryable<MemberEntity> Find(string targetName, string targetPass);

        IQueryable<MemberEntity> GetSecurePassword(string target);

        void Create(MemberEntity memberEntity);

        void Save();
    }
}
