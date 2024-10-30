using Gym_Fees.Entity;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Fees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pendingeditsontroller : ControllerBase
    {
        private readonly IPendingeditsService _pendingeditsService;

        public Pendingeditsontroller(IPendingeditsService pendingeditsService)
        {
            _pendingeditsService = pendingeditsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingEdits()
        {
            var pendingEdits = await _pendingeditsService.GetAllPendingEditsAsync();
            return Ok(pendingEdits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPendingEditById(Guid id)
        {
            var pendingEdit = await _pendingeditsService.GetPendingEditByIdAsync(id);
            if (pendingEdit == null)
                return NotFound();

            return Ok(pendingEdit);
        }

        [HttpPost]
        public async Task<IActionResult> AddPendingEdit([FromBody] PendingeditsRequestDTO requestDto)
        {
            var isAdded = await _pendingeditsService.AddPendingEditAsync(requestDto);
            if (isAdded)
                return Ok("Pending edit added successfully");
            return BadRequest("Failed to add pending edit");
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveUser(Guid id)
        {
            return await _pendingeditsService.DeletePendingEditAsync(id);
        }


    }
}
