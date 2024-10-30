using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Repository;
using static Gym_Fees.Service.PendingProgramService;

namespace Gym_Fees.Service
{

    public class PendingProgramService : IPendingProgramService
    {
        private readonly IPendingProgramRepo _pendingProgramRepository;

        public PendingProgramService(IPendingProgramRepo pendingProgramRepository)
        {
            _pendingProgramRepository = pendingProgramRepository;
        }

        public async Task<List<PendingProgramResponseDTO>> GetAllPendingProgramsAsync()
        {
            var data = await _pendingProgramRepository.GetAllPendingProgramsAsync();
            return data.Select(p => new PendingProgramResponseDTO
            {
                PendingprogramId = p.PendingprogramId,
                TrainingId = p.TrainingId,
                MemberId = p.MemberId,
                Cardio = p.Cardio,
                Weighttraining = p.Weighttraining
            }).ToList();
        }

        public async Task<PendingProgramResponseDTO> GetPendingProgramByIdAsync(Guid id)
        {
            var data = await _pendingProgramRepository.GetPendingProgramByIdAsync(id);
            if (data == null) return null;
            return new PendingProgramResponseDTO
            {
                PendingprogramId = data.PendingprogramId,
                TrainingId = data.TrainingId,
                MemberId = data.MemberId,
                Cardio = data.Cardio,
                Weighttraining = data.Weighttraining
            };
        }

        public async Task AddPendingProgramAsync(PendingProgramRequestDTO pendingProgramRequestDTO)
        {
            var pendingProgram = new Pendingprogram
            {
                PendingprogramId = Guid.NewGuid(),
                TrainingId = pendingProgramRequestDTO.TrainingId,
                MemberId = pendingProgramRequestDTO.MemberId,
                Cardio = pendingProgramRequestDTO.Cardio,
                Weighttraining = pendingProgramRequestDTO.Weighttraining
            };
            await _pendingProgramRepository.AddPendingProgramAsync(pendingProgram);
        }

        public async Task DeletePendingProgramAsync(Guid id)
        {
            await _pendingProgramRepository.DeletePendingProgramAsync(id);
        }
    }


}

