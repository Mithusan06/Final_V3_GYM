using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.Service;
using Microsoft.Data.SqlClient;
using static Gym_Fees.Repository.PendingProgramRepository;

namespace Gym_Fees.Repository
{

    public class PendingProgramRepository : IPendingProgramRepo
    {

        private readonly string _connectionString;
        public PendingProgramRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Pendingprogram>> GetAllPendingProgramsAsync()
        {
            var pendingPrograms = new List<Pendingprogram>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("SELECT * FROM PendingPrograms", connection);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            pendingPrograms.Add(new Pendingprogram
                            {
                                PendingprogramId = reader.GetGuid(0),
                                TrainingId = reader.GetGuid(1),
                                MemberId = reader.GetGuid(2),
                                Cardio = reader["Cardio"].ToString().Split(',').ToList(),
                                Weighttraining = reader["Weighttraining"].ToString().Split(',').ToList()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            return pendingPrograms;
        }

        public async Task<Pendingprogram> GetPendingProgramByIdAsync(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("SELECT * FROM PendingPrograms WHERE PendingprogramId = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Pendingprogram
                            {
                                PendingprogramId = reader.GetGuid(0),
                                TrainingId = reader.GetGuid(1),
                                MemberId = reader.GetGuid(2),
                                Cardio = reader["Cardio"].ToString().Split(',').ToList(),
                                Weighttraining = reader["Weighttraining"].ToString().Split(',').ToList()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            return null;
        }

        public async Task AddPendingProgramAsync(Pendingprogram pendingProgram)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand(
                        "INSERT INTO PendingPrograms (PendingprogramId, TrainingId, MemberId, Cardio, Weighttraining) " +
                        "VALUES (@PendingprogramId, @TrainingId, @MemberId, @Cardio, @Weighttraining)", connection);

                    command.Parameters.AddWithValue("@PendingprogramId", pendingProgram.PendingprogramId);
                    command.Parameters.AddWithValue("@TrainingId", pendingProgram.TrainingId);
                    command.Parameters.AddWithValue("@MemberId", pendingProgram.MemberId);

                    // Convert the lists to comma-separated strings
                    command.Parameters.AddWithValue("@Cardio", string.Join(",", pendingProgram.Cardio));
                    command.Parameters.AddWithValue("@Weighttraining", string.Join(",", pendingProgram.Weighttraining));

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task DeletePendingProgramAsync(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("DELETE FROM PendingPrograms WHERE PendingprogramId = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

    }

}





