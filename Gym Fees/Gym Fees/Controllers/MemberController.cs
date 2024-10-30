using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Fees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _member;
        private readonly IFileService _fileService;
        public MemberController(IMemberService service, IFileService fileService)

        {
            _member = service;
            _fileService = fileService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddMember([FromForm] MemberRequestDTO mem)
        {
            try
            {
                if (mem.Image.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size exceeds the limit of 1 MB.");
                }

                string[] fileext = { ".jpg", ".jpeg", ".png" };
                string createdImagePath = await _fileService.SaveFile(mem.Image, fileext);

                var member = new Member
                {
                    MemberId = Guid.NewGuid(),
                    FullName = mem.FullName,
                    NicNumber = mem.NicNumber,
                    phoneNumber = mem.phoneNumber,
                    UserName = mem.UserName,
                    Password = mem.Password,
                    Userole = mem.Userole,
                    Memberimg = createdImagePath
                };
                var data = await _member.AddMember(member);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{MemberId}")]
        public async Task<string> RemoveUser(Guid MemberId)
        {
            return await _member.RemoveUser(MemberId);
        }

        [HttpGet("{username}/{password}")]
        public async Task<IActionResult> GetMemberByUsernameAndPassword(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return BadRequest("Username and password are required.");
                }
                var member = await _member.GetMemberByUsernameAndPasswordAsync(username, password);
                if (member == null)
                {
                    return Unauthorized("Invalid username or password.");
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody]Member user, Guid id)
        {
            try
            {
                var data = await _member.UpdateUser(user, id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetMemberByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username cannot be empty.");
            }
            try
            {
                var member = await _member.GetMemberByUsernameAsync(username);
                if (member != null)
                {
                    return Ok(member);
                }
                else
                {
                    return NotFound($"Member with username '{username}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MemberController: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            try
            {
                var data = await _member.GetAllMembers();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
