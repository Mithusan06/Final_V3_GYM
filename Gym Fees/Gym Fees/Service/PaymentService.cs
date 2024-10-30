using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Repository;

namespace Gym_Fees.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _paymentRepositary;
        public PaymentService(IPaymentRepo paymentRepositary)
        {
            _paymentRepositary = paymentRepositary;
        }

        public List<PaymentResponseDTO> GetAllPaymentDetails()
        {
            var data = _paymentRepositary.GetAllPaymentDetails();
            var list = new List<PaymentResponseDTO>();
            foreach (var item in data)
            {
                var paymentRequestDTO = new PaymentResponseDTO()
                {
                    MemberId = item.MemberId,
                    PaymentId = item.PaymentId,
                    Amount = item.Amount,
                    PaymentDate = item.PaymentDate,
                    NextpaymentDate = item.NextpaymentDate,
                    PaymentMethod = item.PaymentMethod,
                    PaymentStatus = item.PaymentStatus,
                    PaymentType = item.PaymentType
                };
                list.Add(paymentRequestDTO);
            }
            return list;
        }


        public List<PaymentResponseDTO> GetAllByPaymentId(Guid Paymentid)
        {
            var data = _paymentRepositary.GetAllByPaymentId(Paymentid);
            var list = new List<PaymentResponseDTO>();
            foreach (var item in data)
            {
                var paymentRequestDTO = new PaymentResponseDTO()
                {
                    MemberId = item.MemberId,
                    Amount = item.Amount,
                    PaymentId = item.PaymentId,
                    PaymentDate = item.PaymentDate,
                    NextpaymentDate = item.NextpaymentDate,
                    PaymentMethod = item.PaymentMethod,
                    PaymentStatus = item.PaymentStatus,
                    PaymentType = item.PaymentType
                };
                list.Add(paymentRequestDTO);
            }
            return list;
        }


        public List<PaymentResponseDTO> GetAllByMemberId(Guid MemberId)
        {
            var data = _paymentRepositary.GetAllByMemberId(MemberId);
            var list = new List<PaymentResponseDTO>();
            foreach (var item in data)
            {
                var paymentRequestDTO = new PaymentResponseDTO()
                {
                    PaymentId = item.PaymentId,
                    MemberId = item.MemberId,
                    PaymentMethod = item.PaymentMethod,
                    Amount = item.Amount,
                    PaymentDate = item.PaymentDate,
                    NextpaymentDate = item.NextpaymentDate,
                    PaymentStatus = item.PaymentStatus,
                    PaymentType = item.PaymentType
                };
                list.Add(paymentRequestDTO);
            }
            return list;
        }


        public PaymentResponseDTO AddPayment(PaymentRequestDTO payment)
        {
            var item = new Payment()
            {
                MemberId = payment.MemberId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentType = payment.PaymentType,
                PaymentDate = payment.PaymentDate,
                NextpaymentDate = payment.NextpaymentDate
            };

            var data = _paymentRepositary.AddPayment(item);
            DateTime currentDate = DateTime.Now;
            var paymentResponseDTO = new PaymentResponseDTO()
            {
                Amount = data.Amount,
                MemberId = data.MemberId,
                PaymentStatus = data.PaymentStatus,
                PaymentType = data.PaymentType,
                PaymentMethod = data.PaymentMethod,
                PaymentDate = data.PaymentDate,
                NextpaymentDate = data.NextpaymentDate
            };
            return paymentResponseDTO;
        }


        public List<PaymentResponseDTO> GetAllByPaymentStatus(PaymentStatus paymentStatus)
        {
            try
            {
                var data = _paymentRepositary.GetAllByPaymentStatus(paymentStatus);
                var list = new List<PaymentResponseDTO>();
                foreach (var item in data)
                {
                    var paymentRequestDTO = new PaymentResponseDTO()
                    {
                        PaymentId = item.PaymentId,
                        MemberId = item.MemberId,
                        Amount = item.Amount,
                        PaymentDate = item.PaymentDate,
                        NextpaymentDate = item.NextpaymentDate,
                        PaymentMethod = item.PaymentMethod,
                        PaymentStatus = item.PaymentStatus,
                        PaymentType = item.PaymentType
                    };
                    list.Add(paymentRequestDTO);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllByPaymentStatus: {ex.Message}");
                throw;
            }
        }


        public async Task<string> UpdatePayment(PaymentResponseDTO pay, Guid id)
        {
            if (pay != null)
            {
                var Req = new Payment
                {
                    PaymentId = id,
                    PaymentStatus = pay.PaymentStatus
                };
                var data = await _paymentRepositary.UpdatePayment(Req);
                return data;
            }
            else
            {
                throw new Exception("Field is Required");
            }
        }


    }

}



