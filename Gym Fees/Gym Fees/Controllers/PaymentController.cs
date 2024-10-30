using Gym_Fees.Entity;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Fees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet]
        public IActionResult GetAllPaymentDetails()
        {
            try
            {
                var data = _paymentService.GetAllPaymentDetails();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetAllByPaymentId(Guid id)
        {
            try
            {
                var data = _paymentService.GetAllByPaymentId(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("MemberId/{MemberId}")]
        public IActionResult GetAllByMemberId(Guid MemberId)
        {
            try
            {
                var data = _paymentService.GetAllByMemberId(MemberId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddPayment(PaymentRequestDTO payment)
        {
            if (!Enum.IsDefined(typeof(PaymentMethod), payment.PaymentMethod))
            {
                return BadRequest("Invalid enum value.");
            }
            var paymentId = _paymentService.AddPayment(payment);
            return Ok(paymentId);
        }


        [HttpGet("status/{status}")]
        public IActionResult GetPaymentsByStatus(string status)
        {
            if (!Enum.TryParse<PaymentStatus>(status, true, out var paymentStatus))
            {
                return BadRequest("Invalid payment status.");
            }
            try
            {
                var payments = _paymentService.GetAllByPaymentStatus(paymentStatus);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPaymentsByStatus: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(PaymentResponseDTO pay, Guid id)
        {
            try
            {
                var data = await _paymentService.UpdatePayment(pay, id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
