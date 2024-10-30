using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Fees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingProgramController : ControllerBase
    {
        private readonly ITrainingProgramService _service;

        public TrainingProgramController(ITrainingProgramService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainingProgram([FromBody] TrainingprogramRequestDTO dto)
        {
            await _service.AddTrainingProgramAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainingPrograms()
        {
            var programs = await _service.GetAllTrainingProgramsAsync();
            return Ok(programs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTrainingProgramById(Guid id)
        {
            var program = await _service.GetTrainingProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }
            return Ok(program);
        }

        [HttpGet("MemberId/{id}")]
        public async Task<IActionResult> GetTrainingProgramByMemberId(Guid id)
        {
            var program = await _service.GetTrainingProgramByMemberId(id);
            if (program == null)
            {
                return NotFound();
            }
            return Ok(program);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainingProgram(Guid id, TrainingprogramRequestDTO dto)
        {
            await _service.UpdateTrainingProgramAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingProgram(Guid id)
        {
            await _service.DeleteTrainingProgramAsync(id);
            return Ok();
        }
    }
}
