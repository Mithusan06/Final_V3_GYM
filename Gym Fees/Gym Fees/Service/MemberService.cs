using Azure.Core;
using Gym_Fees.DataBase;
using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepository;
        public MemberService(IMemberRepo memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> AddMember(Member mem)
        {
            var response = await _memberRepository.AddMember(mem);
            return response;
        }

        public async Task<string> RemoveUser(Guid MemberId)
        {
            return await _memberRepository.RemoveUser(MemberId);
        }

        public async Task<List<MemberResponseDTO>> GetAllMembers()
        {
            var data = await _memberRepository.GetAllMembers();

            var Obj = new List<MemberResponseDTO>();
            foreach (var user in data)
            {
                var response = new MemberResponseDTO()
                {
                    MemberId = user.MemberId,
                    FullName = user.FullName,
                    NicNumber = user.NicNumber,
                    phoneNumber = user.phoneNumber,
                    UserName = user.UserName,
                    Password = user.Password,
                    Userole = user.Userole,
                    Memberimg = user.Memberimg,
                    DateofRegistration = user.DateofRegistration
                };
                Obj.Add(response);
            }
            return Obj;
        }

        public async Task<string> UpdateUser(Member user, Guid id)
        {
            if (user != null)
            {
                var ReqUser = new Member
                {
                    MemberId = id,
                    NicNumber = user.NicNumber,
                    phoneNumber = user.phoneNumber,
                    UserName = user.UserName
                };
                var data = await _memberRepository.UpdateUser(ReqUser);
                return data;
            }
            else
            {
                throw new Exception("Field is Required");
            }
        }

        public async Task<MemberResponseDTO> GetMemberByUsernameAndPasswordAsync(string username, string password)
        {
            try
            {
                var member = await _memberRepository.GetMemberByUsernameAndPasswordAsync(username, password);
                if (member == null)
                {
                    return null;
                }
                return new MemberResponseDTO
                {
                    MemberId = member.MemberId,
                    FullName = member.FullName,
                    NicNumber = member.NicNumber,
                    phoneNumber = member.phoneNumber,
                    UserName = member.UserName,
                    Password = member.Password,
                    Userole = member.Userole,
                    Memberimg = member.Memberimg,
                    DateofRegistration = member.DateofRegistration
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MemberService: {ex.Message}");
                throw new Exception("An error occurred while processing your request.");
            }
        }

        public async Task<Member> GetMemberByUsernameAsync(string username)
        {
            return await _memberRepository.GetMemberByUsernameAsync(username);
        }
       


    }
}

