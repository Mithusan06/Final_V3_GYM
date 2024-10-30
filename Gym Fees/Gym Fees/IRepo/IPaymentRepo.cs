using Gym_Fees.Entity;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IRepo
{
    public interface IPaymentRepo
    {
        List<PaymentResponseDTO> GetAllPaymentDetails();
        List<PaymentResponseDTO> GetAllByPaymentId(Guid PaymentId);
        List<PaymentResponseDTO> GetAllByMemberId(Guid MemberId);
        Task<string> UpdatePayment(Payment payment);
        Payment AddPayment(Payment payment);
        List<PaymentResponseDTO> GetAllByPaymentStatus(PaymentStatus paymentStatus);
    }
}
