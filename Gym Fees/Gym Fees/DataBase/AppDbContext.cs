using Gym_Fees.Entity;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Gym_Fees.DataBase
{
    public class AppDbContext
    {
        private readonly string _Connection;

        public AppDbContext(string connectionString)
        {
            _Connection = connectionString;
        }

        public void Initialize()
        {
            using (var sqlConnection = new SqlConnection(_Connection))
            {
                sqlConnection.Open();
                var createTableCommand = sqlConnection.CreateCommand();

                createTableCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'Members')
                BEGIN 
                    CREATE TABLE Members (
                        MemberId UNIQUEIDENTIFIER PRIMARY KEY,
                        FullName NVARCHAR(100) NOT NULL,
                        NicNumber NVARCHAR(25) NOT NULL,
                        PhoneNumber NVARCHAR(25) NOT NULL,
                        UserName NVARCHAR(25) NOT NULL,
                        Password NVARCHAR(25) NOT NULL,
                        Userole NVARCHAR(25) NOT NULL,
                        Image NVARCHAR(250) NOT NULL,
                        DateofRegistration DATETIME NOT NULL,
                        IsDeleted BIT NOT NULL DEFAULT 0
                    );

                    INSERT INTO Members (MemberId, FullName, NicNumber, PhoneNumber, UserName, Password, Userole, Image, DateofRegistration, IsDeleted)
                    VALUES (NEWID(), 'John Doe', '123456789V', '123-456-7890', 'johndoe', 'MTIz', 'member', 'e0fd0398-3a00-43c5-ac67-b9109d9ccff6.jpg', GETDATE(), 0);
                     INSERT INTO Members (MemberId, FullName, NicNumber, PhoneNumber, UserName, Password, Userole, Image, DateofRegistration, IsDeleted)
                    VALUES (NEWID(), 'John Doe', '123456789V', '123-456-7890', 'mithu', 'MTIz', 'admin', 'profile.jpg', GETDATE(), 0);
                END";
                createTableCommand.ExecuteNonQuery();

                
                createTableCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'Pendingedits')
                BEGIN 
                    CREATE TABLE Pendingedits (
                        PendingeditId UNIQUEIDENTIFIER PRIMARY KEY,
                        MemberId UNIQUEIDENTIFIER NOT NULL,
                        FullName NVARCHAR(100) NOT NULL,
                        NicNumber NVARCHAR(25) NOT NULL,
                        PhoneNumber NVARCHAR(25) NOT NULL,
                        UserName NVARCHAR(25) NOT NULL
                    );

                 
                END";
                createTableCommand.ExecuteNonQuery();

               
                createTableCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'Notification')
                BEGIN 
                    CREATE TABLE Notification (
                        N_Id UNIQUEIDENTIFIER PRIMARY KEY,
                        MemberId UNIQUEIDENTIFIER NOT NULL,
                        N_Type NVARCHAR(50) NOT NULL, 
                        N_Status NVARCHAR(50) NOT NULL
                    );

                   
                END";
                createTableCommand.ExecuteNonQuery();

               
                createTableCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'Payment')
                BEGIN 
                    CREATE TABLE Payment (
                        PaymentId UNIQUEIDENTIFIER PRIMARY KEY,
                        MemberId UNIQUEIDENTIFIER NOT NULL,
                        Amount DECIMAL(15,2) NOT NULL,
                        Paymentmethod NVARCHAR(50) NOT NULL,
                        PaymentType NVARCHAR(50) NOT NULL,
                        PaymentStatus NVARCHAR(50),
                        PaymentDate NVARCHAR(50) NOT NULL,
                        NextpaymentDate NVARCHAR(50) NOT NULL
                    );

                    INSERT INTO Payment (PaymentId, MemberId, Amount, Paymentmethod, PaymentType, PaymentStatus, PaymentDate, NextpaymentDate)
                    VALUES (NEWID(), (SELECT MemberId FROM Members WHERE UserName = 'johndoe'), 1000.00, 'Cash', 'Monthly', 'Overdue', '2024-06-01', '2024-08-01');
                    INSERT INTO Payment (PaymentId, MemberId, Amount, Paymentmethod, PaymentType, PaymentStatus, PaymentDate, NextpaymentDate)
                    VALUES (NEWID(), (SELECT MemberId FROM Members WHERE UserName = 'johndoe'), 1000.00, 'Cash', 'Monthly', 'Overdue', '2024-09-01', '2024-10-01');
                END";
                createTableCommand.ExecuteNonQuery();

                
                createTableCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'Pendingprograms')
                BEGIN 
                    CREATE TABLE Pendingprograms (
                        PendingprogramId UNIQUEIDENTIFIER PRIMARY KEY,
                        TrainingId UNIQUEIDENTIFIER NOT NULL,
                        MemberId UNIQUEIDENTIFIER NOT NULL,
                        Cardio NVARCHAR(250) NOT NULL,
                        Weighttraining NVARCHAR(250) NOT NULL
                    );

                   
                END";
                createTableCommand.ExecuteNonQuery();

                
                createTableCommand.CommandText = @"
                IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME = 'Trainingprogram')
                BEGIN 
                    CREATE TABLE Trainingprogram (
                        TrainingId UNIQUEIDENTIFIER PRIMARY KEY,
                        MemberId UNIQUEIDENTIFIER NOT NULL,
                        Cardio NVARCHAR(250) NOT NULL,
                        Weighttraining NVARCHAR(250) NOT NULL
                    );

                    INSERT INTO Trainingprogram (TrainingId, MemberId, Cardio, Weighttraining)
                    VALUES (NEWID(), (SELECT MemberId FROM Members WHERE UserName = 'johndoe'), 'Aerobic', 'Lifting');
                END";
                createTableCommand.ExecuteNonQuery();
            }
        }
    }

}

//CONSTRAINT FK_Pendingedits_MemberId FOREIGN KEY (MemberId) REFERENCES Members(MemberId),


