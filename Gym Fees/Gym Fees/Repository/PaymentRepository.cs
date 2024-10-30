using Gym_Fees.Entity;
using Gym_Fees.IRepo;
using Gym_Fees.Model.ResponseDTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Gym_Fees.Repository
{
    public class PaymentRepository : IPaymentRepo
    {

        private string _connectionstring;
        public PaymentRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public List<PaymentResponseDTO> GetAllPaymentDetails()
        {
            var payment = new List<PaymentResponseDTO>();
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string str = "SELECT * FROM Payment";
                    command.CommandText = str;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var paymentResponse = new PaymentResponseDTO
                                {
                                    PaymentId = reader.GetGuid(reader.GetOrdinal("PaymentId")),
                                    MemberId = reader.GetGuid(reader.GetOrdinal("MemberId")),
                                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), reader["PaymentMethod"].ToString()),
                                    PaymentDate = reader.GetString(("PaymentDate")),
                                    NextpaymentDate = reader.GetString(("NextpaymentDate")),
                                    PaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), reader["PaymentType"].ToString()),
                                    PaymentStatus = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), reader["PaymentStatus"].ToString())
                                };

                                payment.Add(paymentResponse);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing row: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching payment details: {ex.Message}");
                throw;
            }

            return payment;
        }

        public List<PaymentResponseDTO> GetAllByPaymentId(Guid paymentId)
        {
            var paymentList = new List<PaymentResponseDTO>();

            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Payment WHERE PaymentId = @PaymentId", connection))
                    {
                        command.Parameters.AddWithValue("@PaymentId", paymentId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var paymentResponse = new PaymentResponseDTO
                                {
                                    PaymentId = reader.GetGuid(reader.GetOrdinal("PaymentId")),
                                    MemberId = reader.GetGuid(reader.GetOrdinal("MemberId")),
                                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), reader["PaymentMethod"].ToString()),
                                    PaymentDate = reader.GetString(("PaymentDate")),
                                    NextpaymentDate = reader.GetString(("NextpaymentDate")),
                                    PaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), reader["PaymentType"].ToString()),
                                    PaymentStatus = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), reader["PaymentStatus"].ToString())
                                };

                                paymentList.Add(paymentResponse);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching payment by ID: {ex.Message}");
                throw;
            }

            return paymentList;
        }

        public List<PaymentResponseDTO> GetAllByMemberId(Guid memberId)
        {
            var payment = new List<PaymentResponseDTO>();

            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string str = @"SELECT * FROM Payment WHERE MemberId = @MemberId";
                    command.CommandText = str;
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var paymentResponse = new PaymentResponseDTO
                            {
                                PaymentId = reader.GetGuid(reader.GetOrdinal("PaymentId")),
                                MemberId = reader.GetGuid(reader.GetOrdinal("MemberId")),
                                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                PaymentMethod = reader["PaymentMethod"] != DBNull.Value ?
                                    (PaymentMethod)Enum.Parse(typeof(PaymentMethod), reader["PaymentMethod"].ToString()) :
                                    default(PaymentMethod),
                                PaymentDate = reader.GetString(("PaymentDate")),
                                NextpaymentDate = reader.GetString(("NextpaymentDate")),
                                PaymentType = reader["PaymentType"] != DBNull.Value ?
                                    (PaymentType)Enum.Parse(typeof(PaymentType), reader["PaymentType"].ToString()) :
                                    default(PaymentType),
                                PaymentStatus = reader["PaymentStatus"] != DBNull.Value ?
                                    (PaymentStatus)Enum.Parse(typeof(PaymentStatus), reader["PaymentStatus"].ToString()) :
                                    default(PaymentStatus)
                            };

                            payment.Add(paymentResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching payments by MemberId: {ex.Message}");
                throw;
            }
            return payment;
        }

        public Payment AddPayment(Payment payment)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string str = "INSERT INTO Payment (PaymentId, MemberId, Amount, PaymentMethod, PaymentStatus, PaymentType, PaymentDate, NextpaymentDate) VALUES (@PaymentId, @MemberId, @Amount, @PaymentMethod, @PaymentStatus, @PaymentType, @PaymentDate, @NextpaymentDate)";

                    command.CommandText = str;

                    command.Parameters.AddWithValue("@PaymentId", Guid.NewGuid());
                    command.Parameters.AddWithValue("@MemberId", payment.MemberId);
                    if (payment.Amount < 0)
                    {
                        throw new Exception("Amount must be in positive");

                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Amount", payment.Amount);
                    }

                    command.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod.ToString());
                    command.Parameters.AddWithValue("@PaymentType", payment.PaymentType.ToString());
                    command.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus.ToString());
                    command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    command.Parameters.AddWithValue("@NextpaymentDate", payment.NextpaymentDate);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding payment: {ex.Message}");
                throw;
            }

            return payment;
        }


        public List<PaymentResponseDTO> GetAllByPaymentStatus(PaymentStatus paymentStatus)
        {
            var paymentList = new List<PaymentResponseDTO>();

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Payment WHERE PaymentStatus = @PaymentStatus", connection))
                {
                    command.Parameters.AddWithValue("@PaymentStatus", paymentStatus.ToString());

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var paymentResponse = new PaymentResponseDTO
                            {
                                PaymentId = reader.GetGuid(reader.GetOrdinal("PaymentId")),
                                MemberId = reader.GetGuid(reader.GetOrdinal("MemberId")),
                                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), reader["PaymentMethod"].ToString()),
                                PaymentDate = reader.GetString(("PaymentDate")),
                                NextpaymentDate = reader.GetString(("NextpaymentDate")),
                                PaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), reader["PaymentType"].ToString()),
                                PaymentStatus = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), reader["PaymentStatus"].ToString())
                            };

                            paymentList.Add(paymentResponse);
                        }
                    }
                }
            }
            return paymentList;
        }

        public async Task<string> UpdatePayment(Payment payment)
        {
            try
            {
                using (var Connection = new SqlConnection(_connectionstring))
                {
                    await Connection.OpenAsync();

                    var command = Connection.CreateCommand();
                    command.CommandText = @" UPDATE Payment SET PaymentStatus = @PaymentStatus
                                                            WHERE PaymentId = @PaymentId";

                    command.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus.ToString());
                    command.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
                    await command.ExecuteNonQueryAsync();
                }
                return "Update Successfully";
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in UpdatePayment: {sqlEx.Message}");
                return $"SQL Error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePayment: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

    }
}
