using Gym_Fees.Entity;

namespace Gym_Fees.IRepo
{
    public interface ITrainingProgramRepo
    {
        Task AddTrainingProgramAsync(Trainingprogram program);
        Task<List<Trainingprogram>> GetAllTrainingProgramsAsync();
        Task<Trainingprogram> GetTrainingProgramByIdAsync(Guid trainingId);
        Task UpdateTrainingProgramAsync(Trainingprogram program);
        Task DeleteTrainingProgramAsync(Guid trainingId);
        Task<Trainingprogram> GetTrainingProgramByMemberId(Guid MemberId);
    }
}
