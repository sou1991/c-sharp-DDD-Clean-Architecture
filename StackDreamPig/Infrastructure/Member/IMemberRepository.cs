using Entities;
using Entities.Member;

namespace Infrastructure.Member
{
    public interface IMemberRepository
    {
        MemberEntity GetUser(string targetName, string targetPath);

        MemberEntity GetUserWithSession(string target);

        MemberEntity GetUserWithUserName(string target);

        void Create(MemberEntity memberData);

        void Update(MemberEntity entity, string m_no);

        void Save();
    }
}
