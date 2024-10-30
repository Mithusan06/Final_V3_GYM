using Gym_Fees.Entity;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using System.Threading.Tasks;

namespace Gym_Fees.Service
{
    public interface IMemberService
    {
        Task<MemberResponseDTO> GetMemberByUsernameAndPasswordAsync(string username, string password);
        Task<string> UpdateUser(Member user, Guid id);
        Task<List<MemberResponseDTO>> GetAllMembers();
        Task<string> RemoveUser(Guid MemberId);
        Task<Member> AddMember(Member mem);
        Task<Member> GetMemberByUsernameAsync(string username);
       
        //Task<Member> AuthenticateAsync(string username, string password);
    }
}
