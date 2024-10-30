namespace Gym_Fees.Model.ResponseDTO
{
    public class MemberResponseDTO 
    {
        public Guid MemberId { get; set; }
        public string FullName { get; set; }
        public string NicNumber { get; set; }
        public string phoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Userole { get; set; }
        public string Memberimg { get; set; }
        //public IFormFile Image { get; set; }
        public DateOnly DateofRegistration { get; set; }
        public bool ISActive { get; set; }
    }
}
