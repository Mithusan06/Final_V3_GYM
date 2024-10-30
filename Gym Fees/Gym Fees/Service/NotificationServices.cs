using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.IService;
using Gym_Fees.Model.RequestDTO;
using Gym_Fees.Model.ResponseDTO;
using Gym_Fees.Repository;

namespace Gym_Fees.Service
{
    public class NotificationServices : INotificationServices
    {

        private INotificationRepository _notificationRepository;
        public NotificationServices(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public string AddNotification(NotificationRequestDTO notification)
        {
            var obj = new Notification
            {
                Memberid = notification.Memberid,
                N_Type = notification.N_Type,
                N_Status = notification.N_Status
            };

            var returndata = _notificationRepository.AddNotification(obj);
            return returndata;
        }

        public async Task<IEnumerable<NotificationResponseDTO>> GetNotificationsByMemberIdAsync(Guid memberId)
        {
            var notifications = await _notificationRepository.GetNotificationsByMemberId(memberId);


            var notificationResponseList = new List<NotificationResponseDTO>();

            foreach (var notification in notifications)
            {
                notificationResponseList.Add(new NotificationResponseDTO
                {
                    N_Id = notification.N_Id,
                    Memberid = notification.Memberid,
                    N_Type = notification.N_Type,
                    N_Status = notification.N_Status
                });
            }
            return notificationResponseList;
        }


        public List<NotificationResponseDTO> GetAllNotifications()
        {
            var data = _notificationRepository.GetAllNotifications();
            var listersponse = new List<NotificationResponseDTO>();
            foreach (var item in data)
            {
                var response = new NotificationResponseDTO()
                {
                    N_Id = item.N_Id,
                    Memberid = item.Memberid,
                    N_Type = item.N_Type,
                    N_Status = item.N_Status
                };
                listersponse.Add(response);
            }
            return listersponse;
        }

        public string DeleteNotification(Guid notificationId)
        {
            var data = _notificationRepository.DeleteNotification(notificationId);
            return data;
        }


        public string UpdateNotification(NotificationRequestDTO notificationRequest, Guid notificationId)
        {
            var requestdata = new Notification
            {
                N_Id = notificationId,
                Memberid = notificationRequest.Memberid,
                N_Type = notificationRequest.N_Type,
                N_Status = notificationRequest.N_Status
            };
            var data = _notificationRepository.UpdateNotification(requestdata);
            return data;
        }


    }
}
