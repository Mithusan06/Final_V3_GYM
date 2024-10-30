namespace Gym_Fees.Model.RequestDTO
{
    public class PendingProgramRequestDTO
    {

        public Guid TrainingId { get; set; }
        public Guid MemberId { get; set; }
        public List<string> Cardio { get; set; }
        public List<string> Weighttraining { get; set; }
    }
}
