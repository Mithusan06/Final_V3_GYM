using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IService
{
    public interface IPendingeditsService
    {
        Task<List<PendingeditResponseDTO>> GetAllPendingEditsAsync();
        Task<PendingeditResponseDTO> GetPendingEditByIdAsync(Guid pendingeditId);
        Task<bool> AddPendingEditAsync(PendingeditsRequestDTO requestDto);
        Task<bool> DeletePendingEditAsync(Guid pendingeditId);
    }
}
