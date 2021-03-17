using Entities;

namespace Infrastructure.Member
{
    public interface IMemberRepository
    {
        MemberEntity GetUser(string targetName, string targetPath);

        MemberEntity GetUserWithSession(string target);

        MemberEntity GetUserWithUserName(string target);

        void Create(MemberEntity memberEntity);

        void Save();
    }
}
