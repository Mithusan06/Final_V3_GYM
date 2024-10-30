using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.Model.ResponseDTO;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Gym_Fees.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private string _connectionstring;
        public NotificationRepository(string connectionString)
        {
            _connectionstring = connectionString;
        }

        public string AddNotification(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT  INTO Notification(N_Id,Memberid,N_Type,N_Status)  
                                       VALUES (@N_Id,@Memberid,@N_Type,@N_Status)";

                command.Parameters.AddWithValue("@N_Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@Memberid", notification.Memberid);
                command.Parameters.AddWithValue("@N_Type", notification.N_Type);
                command.Parameters.AddWithValue("@N_Status", notification.N_Status);

                command.ExecuteNonQuery();
            }
            return "Notification Saved Successfully";
        }


        public async Task<IEnumerable<Notification>> GetNotificationsByMemberId(Guid memberId)
        {
            var notifications = new List<Notification>();

            using (var connection = new SqlConnection(_connectionstring))
            {
                try
                {
                    await connection.OpenAsync();
                    var sqlQuery = @"SELECT * FROM Notification WHERE Memberid = @memberId";

                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@memberId", memberId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                notifications.Add(new Notification
                                {
                                    N_Id = reader.GetGuid(0),
                                    Memberid = reader.GetGuid(1),
                                    N_Type = reader.GetString(2),
                                    N_Status = reader.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error in NotificationRepository: {sqlEx.Message}"); 
                    throw new Exception($"Database error: {sqlEx.Message}"); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in NotificationRepository: {ex.Message}"); 
                    throw new Exception($"Repository error: {ex.Message}"); 
                }
            }

            return notifications;
        }

        
        public List<Notification> GetAllNotifications()
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM  Notification";
                var notification = new List<Notification>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new Notification
                        {
                            N_Id = reader.GetGuid(0),
                            Memberid = reader.GetGuid(1),
                            N_Type = reader.GetString(2),
                            N_Status = reader.GetString(3)
                        };
                        notification.Add(obj);

                    };
                    return notification;
                }
            }
        }



        public string DeleteNotification(Guid notificationId)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @" DELETE Notification WHERE N_Id = @N_Id";

                command.Parameters.AddWithValue("@N_Id", notificationId);
                command.ExecuteNonQuery();
            }
            return "Notification Deleted Successfully";
        }



        public string UpdateNotification(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @" UPDATE Notification
                                    SET Memberid = @Memberid,N_Type = @N_Type,N_Status=@N_Status WHERE N_Id = @N_Id ";

                command.Parameters.AddWithValue("Memberid", notification.Memberid);
                command.Parameters.AddWithValue("N_Type", notification.N_Type);
                command.Parameters.AddWithValue("N_Status", notification.N_Status);
                command.Parameters.AddWithValue("N_Id", notification.N_Id);

                command.ExecuteNonQuery();
            }
            return "Notification Updated Successfully";
        }


    }
}
