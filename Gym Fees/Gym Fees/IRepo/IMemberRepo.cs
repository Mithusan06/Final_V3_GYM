namespace Gym_Fees.IRepo
{
    public interface IMemberRepo
    {
        Task<Member> AddMember(Member member);
        Task<string> RemoveUser(Guid MemberId);
        Task<List<Member>> GetAllMembers();
        Task<string> UpdateUser(Member user);
        Task<Member> GetMemberByUsernameAndPasswordAsync(string username, string password);
        Task<Member> GetMemberByUsernameAsync(string username);
        
    }
}
