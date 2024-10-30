using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Microsoft.Data.SqlClient;
using static Gym_Fees.Repository.TrainingProgramRepository;

namespace Gym_Fees.Repository
{
    public class TrainingProgramRepository : ITrainingProgramRepo
    {
        private readonly string _connectionString;
        public TrainingProgramRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddTrainingProgramAsync(Trainingprogram program)
        {
            try
            {
                var cardioCsv = string.Join(",", program.Cardio);
                var weighttrainingCsv = string.Join(",", program.Weighttraining);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @"
            INSERT INTO TrainingProgram (TrainingId, MemberId, Cardio, Weighttraining)
            VALUES (@TrainingId, @MemberId, @Cardio, @Weighttraining)";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@TrainingId", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@MemberId", program.MemberId);
                    cmd.Parameters.AddWithValue("@Cardio", cardioCsv);
                    cmd.Parameters.AddWithValue("@Weighttraining", weighttrainingCsv);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<List<Trainingprogram>> GetAllTrainingProgramsAsync()
        {
            var programs = new List<Trainingprogram>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM TrainingProgram";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var program = new Trainingprogram
                        {
                            TrainingId = (Guid)reader["TrainingId"],
                            MemberId = (Guid)reader["MemberId"],
                            Cardio = reader["Cardio"].ToString().Split(',').ToList(),
                            Weighttraining = reader["Weighttraining"].ToString().Split(',').ToList()
                        };
                        programs.Add(program);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
            return programs;
        }

        public async Task<Trainingprogram> GetTrainingProgramByIdAsync(Guid trainingId)
        {
            Trainingprogram program = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM TrainingProgram WHERE TrainingId = @TrainingId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@TrainingId", trainingId);
                    connection.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        program = new Trainingprogram
                        {
                            TrainingId = (Guid)reader["TrainingId"],
                            MemberId = (Guid)reader["MemberId"],
                            Cardio = reader["Cardio"].ToString().Split(',').ToList(),
                            Weighttraining = reader["Weighttraining"].ToString().Split(',').ToList()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            return program;
        }

        public async Task UpdateTrainingProgramAsync(Trainingprogram program)
        {
            var cardioCsv = string.Join(",", program.Cardio);
            var weighttrainingCsv = string.Join(",", program.Weighttraining);

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @" UPDATE TrainingProgram
                                     SET MemberId = @MemberId, Cardio = @Cardio, Weighttraining = @Weighttraining
                                     WHERE TrainingId = @TrainingId";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@TrainingId", program.TrainingId);
                    cmd.Parameters.AddWithValue("@MemberId", program.MemberId);
                    cmd.Parameters.AddWithValue("@Cardio", cardioCsv);
                    cmd.Parameters.AddWithValue("@Weighttraining", weighttrainingCsv);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteTrainingProgramAsync(Guid trainingId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @"DELETE FROM TrainingProgram WHERE TrainingId = @TrainingId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@TrainingId", trainingId);
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<Trainingprogram> GetTrainingProgramByMemberId(Guid MemberId)
        {
            Trainingprogram program = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string sql = @"SELECT * FROM TrainingProgram WHERE MemberId = @MemberId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@MemberId", MemberId);
                    connection.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        program = new Trainingprogram
                        {
                            MemberId = (Guid)reader["MemberId"],
                            TrainingId = (Guid)reader["TrainingId"],
                            Cardio = reader["Cardio"].ToString().Split(',').ToList(),
                            Weighttraining = reader["Weighttraining"].ToString().Split(',').ToList()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            return program;
        }

    }

}
