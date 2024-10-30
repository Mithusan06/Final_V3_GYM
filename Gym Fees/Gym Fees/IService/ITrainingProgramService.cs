using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IService
{
    public interface ITrainingProgramService
    {
        Task AddTrainingProgramAsync(TrainingprogramRequestDTO dto);
        Task<List<TrainingprogramResponseDTO>> GetAllTrainingProgramsAsync();
        Task<TrainingprogramResponseDTO> GetTrainingProgramByIdAsync(Guid trainingId);
        Task UpdateTrainingProgramAsync(Guid trainingId, TrainingprogramRequestDTO dto);
        Task DeleteTrainingProgramAsync(Guid trainingId);
        Task<TrainingprogramResponseDTO> GetTrainingProgramByMemberId(Guid MemberId);
    }
}

