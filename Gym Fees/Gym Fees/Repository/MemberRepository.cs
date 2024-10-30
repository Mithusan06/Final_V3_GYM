using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;

namespace Gym_Fees.DataBase
{
    public class MemberRepository : IMemberRepo
    {
        private readonly string _connectionString;

        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> UpdateUser(Member user)
        {
            try
            {
                using (var Connection = new SqlConnection(_connectionString))
                {
                    await Connection.OpenAsync();

                    var command = Connection.CreateCommand();
                    command.CommandText = @" UPDATE Members
                                              SET NicNumber = @NicNumber, 
                                                  phoneNumber = @phoneNumber,
                                                  UserName = @UserName
                                              WHERE MemberId = @id";

                    command.Parameters.AddWithValue("@NicNumber", user.NicNumber);
                    command.Parameters.AddWithValue("@phoneNumber", user.phoneNumber);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    command.Parameters.AddWithValue("@id", user.MemberId);

                    await command.ExecuteNonQueryAsync();
                }
                return "Update Successfully";
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in UpdateUser: {sqlEx.Message}");
                return $"SQL Error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateUser: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }


        public async Task<string> RemoveUser(Guid MemberId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Members
                                    SET IsDeleted = 1
                                    WHERE MemberId = @id";

                    command.Parameters.AddWithValue("@id", MemberId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return "User not found or already deleted.";
                    }
                }

                return "User soft-deleted successfully";
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in SoftDeleteUser: {sqlEx.Message}");
                return $"SQL Error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SoftDeleteUser: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        public async Task<Member> GetMemberByUsernameAndPasswordAsync(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sqlQuery = @"SELECT * FROM Members WHERE UserName = @username AND Password = @password";

                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new Member
                                {
                                    MemberId = reader.GetGuid(0),
                                    FullName = reader.GetString(1),
                                    NicNumber = reader.GetString(2),
                                    phoneNumber = reader.GetString(3),
                                    UserName = reader.GetString(4),
                                    Password = reader.GetString(5),
                                    Userole = reader.GetString(6),
                                    Memberimg = reader.GetString(7),
                                    DateofRegistration = DateOnly.FromDateTime(reader.GetDateTime(8))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MemberRepository: {ex.Message}");
                throw new Exception("An error occurred while querying the database.");
            }
        }


        public async Task<Member> GetMemberByUsernameAsync(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var sqlQuery = @"SELECT * FROM Members WHERE UserName = @username";

                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new Member
                                {
                                    MemberId = reader.GetGuid(0),
                                    FullName = reader.GetString(1),
                                    NicNumber = reader.GetString(2),
                                    phoneNumber = reader.GetString(3),
                                    UserName = reader.GetString(4),
                                    Password = reader.GetString(5),
                                    Userole = reader.GetString(6),
                                    Memberimg = reader.GetString(7),
                                    DateofRegistration = DateOnly.FromDateTime(reader.GetDateTime(8))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in MemberRepository: {ex.Message}");
                    throw new Exception("An error occurred while querying the database.");
                }
            }
        }


        public async Task<List<Member>> GetAllMembers()
        {
            var members = new List<Member>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Members";
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var member = new Member

                        {
                            MemberId = reader.GetGuid(reader.GetOrdinal("MemberId")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            NicNumber = reader.GetString(reader.GetOrdinal("NicNumber")),
                            phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            Userole = reader.GetString(reader.GetOrdinal("Userole")),
                            Memberimg = reader.GetString(reader.GetOrdinal("Image")),
                            DateofRegistration = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("DateOfRegistration")))
                        };
                        members.Add(member);
                    }
                }
            }
            return members;
        }


        public async Task<Member> AddMember(Member member)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO Members(MemberId, FullName, NicNumber, phoneNumber, UserName, Password, Userole, Image, DateofRegistration)
                    VALUES(@MemberId, @FullName, @NicNumber, @phoneNumber, @UserName, @Password, @Userole, @Image, @DateofRegistration)";

                    member.MemberId = Guid.NewGuid();

                    command.Parameters.AddWithValue("@MemberId", member.MemberId);
                    command.Parameters.AddWithValue("@FullName", member.FullName);
                    command.Parameters.AddWithValue("@NicNumber", member.NicNumber);
                    command.Parameters.AddWithValue("@phoneNumber", member.phoneNumber);
                    command.Parameters.AddWithValue("@UserName", member.UserName);
                    command.Parameters.AddWithValue("@Password", member.Password);
                    command.Parameters.AddWithValue("@Userole", member.Userole);
                    command.Parameters.AddWithValue("@Image", member.Memberimg);
                    command.Parameters.AddWithValue("@DateofRegistration", member.DateofRegistration);

                    await command.ExecuteNonQueryAsync();
                    return member;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                }
            }
            return member;
        }
        
        



    }
}
