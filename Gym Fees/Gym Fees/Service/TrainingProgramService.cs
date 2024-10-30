using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Repository;

namespace Gym_Fees.Service
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly ITrainingProgramRepo _repository;

        public TrainingProgramService(ITrainingProgramRepo repository)
        {
            _repository = repository;
        }

        public async Task AddTrainingProgramAsync(TrainingprogramRequestDTO dto)
        {
            var program = new Trainingprogram
            {
                TrainingId = Guid.NewGuid(),
                MemberId = dto.MemberId,
                Cardio = dto.Cardio,
                Weighttraining = dto.Weighttraining
            };
            await _repository.AddTrainingProgramAsync(program);
        }

        public async Task<List<TrainingprogramResponseDTO>> GetAllTrainingProgramsAsync()
        {
            var programs = await _repository.GetAllTrainingProgramsAsync();
            return programs.Select(p => new TrainingprogramResponseDTO
            {
                TrainingId = p.TrainingId,
                MemberId = p.MemberId,
                Cardio = p.Cardio,
                Weighttraining = p.Weighttraining
            }).ToList();
        }

        public async Task<TrainingprogramResponseDTO> GetTrainingProgramByIdAsync(Guid trainingId)
        {
            var program = await _repository.GetTrainingProgramByIdAsync(trainingId);

            if (program == null) return null;

            return new TrainingprogramResponseDTO
            {
                TrainingId = program.TrainingId,
                MemberId = program.MemberId,
                Cardio = program.Cardio,
                Weighttraining = program.Weighttraining
            };
        }
        public async Task<TrainingprogramResponseDTO> GetTrainingProgramByMemberId(Guid MemberId)
        {
            var program = await _repository.GetTrainingProgramByMemberId(MemberId);

            if (program == null) return null;

            return new TrainingprogramResponseDTO
            {
                MemberId = program.MemberId,
                TrainingId = program.TrainingId,
                Cardio = program.Cardio,
                Weighttraining = program.Weighttraining
            };
        }

        public async Task UpdateTrainingProgramAsync(Guid trainingId, TrainingprogramRequestDTO dto)
        {
            var program = new Trainingprogram
            {
                TrainingId = trainingId,
                MemberId = dto.MemberId,
                Cardio = dto.Cardio,
                Weighttraining = dto.Weighttraining
            };
            await _repository.UpdateTrainingProgramAsync(program);
        }

        public async Task DeleteTrainingProgramAsync(Guid trainingId)
        {
            await _repository.DeleteTrainingProgramAsync(trainingId);
        }


    }

}
