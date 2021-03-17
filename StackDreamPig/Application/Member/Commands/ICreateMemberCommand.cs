using Application.Member.Model;

namespace Application.Member.Commands
{
    public interface ICreateMemberCommand
    {
        void Execute(IMemberDTO memberModel);
    }
}
