using Gym_Fees.Entity;

namespace Gym_Fees.IRepo
{
    public interface IPendingeditsRepo
    {
        Task<List<Pendingedits>> GetAllPendingEditsAsync();
        Task<Pendingedits> GetPendingEditByIdAsync(Guid pendingeditId);
        Task<bool> AddPendingEditAsync(Pendingedits pendingedit);
        Task<bool> DeletePendingEditAsync(Guid pendingeditId);
    }
}
