using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Fees.Entity
{
    public class Notification
    {
        public Guid N_Id { get; set; }
        public Guid Memberid { get; set; }
        public string N_Type { get; set; }
        public string N_Status { get; set; }
    }
}
