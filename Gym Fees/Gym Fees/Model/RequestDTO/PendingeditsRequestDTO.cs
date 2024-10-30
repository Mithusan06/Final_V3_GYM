namespace Gym_Fees.Model.RequestDTO
{
    public class PendingeditsRequestDTO
    {
        public Guid MemberId { get; set; }
        public string NicNumber { get; set; }
        public string phoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
