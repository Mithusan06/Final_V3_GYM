﻿namespace Gym_Fees.Model.ResponseDTO
{
    public class TrainingprogramResponseDTO
    {
        public Guid TrainingId { get; set; }
        public Guid MemberId { get; set; }
        public List<string> Cardio { get; set; }
        public List<string> Weighttraining { get; set; }
    }
}
