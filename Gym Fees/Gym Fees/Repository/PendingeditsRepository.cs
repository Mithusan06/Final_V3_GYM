using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Microsoft.Data.SqlClient;

namespace Gym_Fees.Repository
{
    public class PendingeditsRepository : IPendingeditsRepo
    {
        private readonly string _connectionString;
        public PendingeditsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Pendingedits>> GetAllPendingEditsAsync()
        {
            var pendingEdits = new List<Pendingedits>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Pendingedits";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            pendingEdits.Add(new Pendingedits
                            {
                                PendingeditId = reader.GetGuid(0),
                                MemberId = reader.GetGuid(1),
                                NicNumber = reader.GetString(2),
                                phoneNumber = reader.GetString(3),
                                FullName = reader.GetString(4),
                                UserName = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
            }
            return pendingEdits;
        }

        public async Task<Pendingedits> GetPendingEditByIdAsync(Guid pendingeditId)
        {
            Pendingedits pendingedit = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Pendingedits WHERE PendingeditId = @PendingeditId";
                    command.Parameters.AddWithValue("@PendingeditId", pendingeditId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            pendingedit = new Pendingedits
                            {
                                PendingeditId = reader.GetGuid(0),
                                MemberId = reader.GetGuid(1),
                                NicNumber = reader.GetString(2),
                                phoneNumber = reader.GetString(3),
                                FullName = reader.GetString(4),
                                UserName = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
            }
            return pendingedit;
        }

        public async Task<bool> AddPendingEditAsync(Pendingedits pendingedit)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
            INSERT INTO Pendingedits (PendingeditId, MemberId, NicNumber, PhoneNumber, FullName, UserName) 
            VALUES (@PendingeditId, @MemberId, @NicNumber, @PhoneNumber, @FullName, @UserName)";

                    command.Parameters.AddWithValue("@PendingeditId", pendingedit.PendingeditId);
                    command.Parameters.AddWithValue("@MemberId", pendingedit.MemberId);
                    command.Parameters.AddWithValue("@NicNumber", pendingedit.NicNumber);
                    command.Parameters.AddWithValue("@PhoneNumber", pendingedit.phoneNumber);
                    command.Parameters.AddWithValue("@FullName", pendingedit.FullName);
                    command.Parameters.AddWithValue("@UserName", pendingedit.UserName);

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeletePendingEditAsync(Guid pendingeditId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Pendingedits WHERE PendingeditId = @PendingeditId";
                    command.Parameters.AddWithValue("@PendingeditId", pendingeditId);

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
