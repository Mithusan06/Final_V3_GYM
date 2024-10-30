using Gym_Fees.Entity;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;

namespace Gym_Fees.IService
{
    public interface INotificationServices
    {
        string AddNotification(NotificationRequestDTO notification);
        List<NotificationResponseDTO> GetAllNotifications();
        string DeleteNotification(Guid notificationId);
        string UpdateNotification(NotificationRequestDTO notificationRequest, Guid notificationId);
        Task<IEnumerable<NotificationResponseDTO>> GetNotificationsByMemberIdAsync(Guid memberId);
    }
}
