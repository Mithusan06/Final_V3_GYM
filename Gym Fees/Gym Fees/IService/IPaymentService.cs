using Gym_Fees.Entity;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IService
{
    public interface IPaymentService
    {
        List<PaymentResponseDTO> GetAllPaymentDetails();
        List<PaymentResponseDTO> GetAllByPaymentId(Guid Paymentid);
        List<PaymentResponseDTO> GetAllByMemberId(Guid MemberId);
        PaymentResponseDTO AddPayment(PaymentRequestDTO payment);
        public List<PaymentResponseDTO> GetAllByPaymentStatus(PaymentStatus paymentStatus);
        Task<string> UpdatePayment(PaymentResponseDTO pay, Guid id);

    }
}
