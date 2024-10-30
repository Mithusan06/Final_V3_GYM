using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Fees.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PendingProgramController : ControllerBase
    {
        private readonly IPendingProgramService _pendingProgramService;

        public PendingProgramController(IPendingProgramService pendingProgramService)
        {
            _pendingProgramService = pendingProgramService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingPrograms()
        {
            var data = await _pendingProgramService.GetAllPendingProgramsAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPendingProgramById(Guid id)
        {
            var data = await _pendingProgramService.GetPendingProgramByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddPendingProgram([FromBody] PendingProgramRequestDTO pendingProgramRequestDTO)
        {
            await _pendingProgramService.AddPendingProgramAsync(pendingProgramRequestDTO);
            return CreatedAtAction(nameof(GetPendingProgramById), new { id = pendingProgramRequestDTO.MemberId }, pendingProgramRequestDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePendingProgram(Guid id)
        {
            await _pendingProgramService.DeletePendingProgramAsync(id);
            return NoContent();
        }
    }
}
