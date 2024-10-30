using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Gym_Fees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationServices _notificationServices;

        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        [HttpPost]
        public IActionResult AddNotification(NotificationRequestDTO notification)
        {
            try
            {
                var returndata = _notificationServices.AddNotification(notification);
                return Ok(returndata);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetNotifications(Guid memberId)
        {
            if (memberId == Guid.Empty)
            {
                return BadRequest("Invalid member ID.");
            }
            try
            {
                var notifications = await _notificationServices.GetNotificationsByMemberIdAsync(memberId);
                if (notifications == null || !notifications.Any())
                {
                    return NotFound("No notifications found for the specified member ID.");
                }
                return Ok(notifications);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in NotificationController: {sqlEx.Message}");
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in NotificationController: {ex.Message}");
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        public IActionResult GetAllNotifications()
        {
            try
            {
                var data = _notificationServices.GetAllNotifications();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(Guid id)
        {
            try
            {
                var data = _notificationServices.DeleteNotification(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateNotification(NotificationRequestDTO notificationRequest, Guid id)
        {
            try
            {
                var Data = _notificationServices.UpdateNotification(notificationRequest, id);
                return Ok(Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}


