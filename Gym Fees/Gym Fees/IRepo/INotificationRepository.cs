using Gym_Fees.Entity;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IRepo
{
    public interface INotificationRepository
    {
        string AddNotification(Notification notification);
        List<Notification> GetAllNotifications();
        string DeleteNotification(Guid notificationId);
        string UpdateNotification(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsByMemberId(Guid memberId);
    }
}
