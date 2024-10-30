using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IService
{
    public interface IPendingProgramService
    {
        Task<List<PendingProgramResponseDTO>> GetAllPendingProgramsAsync();
        Task<PendingProgramResponseDTO> GetPendingProgramByIdAsync(Guid id);
        Task AddPendingProgramAsync(PendingProgramRequestDTO pendingProgramRequestDTO);
        Task DeletePendingProgramAsync(Guid id);
    }
}
