namespace Gym_Fees.Model.RequestDTO
{
    public class TrainingprogramRequestDTO
    {
        public Guid MemberId { get; set; }
        public List<string> Cardio { get; set; }
        public List<string> Weighttraining { get; set; }
    }
}
