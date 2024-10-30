
namespace Gym_Fees.Entity
{
    public enum PaymentMethod
    {
        Cash,
        Card
    }
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Overdue
    }
    public enum PaymentType
    {
        Annual,
        Monthly,
        ThreeMonth,
        SixMonth
    }

    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid MemberId { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentDate { get; set; }
        public string? NextpaymentDate { get; set; }

    }
}
