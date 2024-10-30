using Gym_Fees.Entity;

namespace Gym_Fees.IRepo
{
    public interface IPendingProgramRepo
    {
        Task<List<Pendingprogram>> GetAllPendingProgramsAsync();
        Task<Pendingprogram> GetPendingProgramByIdAsync(Guid id);
        Task AddPendingProgramAsync(Pendingprogram pendingProgram);
        Task DeletePendingProgramAsync(Guid id);
    }
}
