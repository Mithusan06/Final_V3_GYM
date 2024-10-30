namespace Gym_Fees.Model.RequestDTO
{
    public class MemberRequestDTO 
    { 
        public string FullName { get; set; }
        public string NicNumber { get; set; }
        public string phoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Userole { get; set; }
        public IFormFile? Image { get; set; }
    }
}
