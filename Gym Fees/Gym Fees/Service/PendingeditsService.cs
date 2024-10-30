using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Repository;

namespace Gym_Fees.Service
{
    public class PendingeditsService : IPendingeditsService
    {
        private readonly IPendingeditsRepo _pendingeditsRepository;

        public PendingeditsService(IPendingeditsRepo pendingeditsRepository)
        {
            _pendingeditsRepository = pendingeditsRepository;
        }

        public async Task<List<PendingeditResponseDTO>> GetAllPendingEditsAsync()
        {
            var pendingEdits = await _pendingeditsRepository.GetAllPendingEditsAsync();
            return pendingEdits.Select(pe => new PendingeditResponseDTO
            {
                PendingeditId = pe.PendingeditId,
                MemberId = pe.MemberId,
                NicNumber = pe.NicNumber,
                phoneNumber = pe.phoneNumber,
                FullName = pe.FullName,
                UserName = pe.UserName
            }).ToList();
        }

        public async Task<PendingeditResponseDTO> GetPendingEditByIdAsync(Guid pendingeditId)
        {
            var pendingedit = await _pendingeditsRepository.GetPendingEditByIdAsync(pendingeditId);
            if (pendingedit == null) return null;

            return new PendingeditResponseDTO
            {
                PendingeditId = pendingedit.PendingeditId,
                MemberId = pendingedit.MemberId,
                NicNumber = pendingedit.NicNumber,
                phoneNumber = pendingedit.phoneNumber,
                FullName = pendingedit.FullName,
                UserName = pendingedit.UserName
            };
        }

        public async Task<bool> AddPendingEditAsync(PendingeditsRequestDTO requestDto)
        {
            var pendingedit = new Pendingedits
            {
                PendingeditId = Guid.NewGuid(),
                MemberId = requestDto.MemberId,
                NicNumber = requestDto.NicNumber,
                phoneNumber = requestDto.phoneNumber,
                FullName = requestDto.FullName,
                UserName = requestDto.UserName
            };

            return await _pendingeditsRepository.AddPendingEditAsync(pendingedit);
        }

        public async Task<bool> DeletePendingEditAsync(Guid pendingeditId)
        {
            return await _pendingeditsRepository.DeletePendingEditAsync(pendingeditId);
        }
    }
}

