using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using RentService.Core.Entities;
using RentService.Core.Interfaces;

namespace RentService.Infrastructure.Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly IConfiguration _configuration;

        public RentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Rent entity)
        {
            entity.RentalDate = DateTime.Now;
            var sql = "INSERT INTO Rents (RentalDate, ReturnDate, AmountForDays, NumberOfDays, " +
                "Status, Comment, EmployeeID, Employee, ClientID, Client, CarID, Car) " +
                "Values (@RentalDate, @ReturnDate, @AmountForDays, @NumberOfDays, @Status, @Comment," +
                "@EmployeeID, @Employee, @ClientID, @Client, @CarID, @Car);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, entity);
                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Rents WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<Rent> Get(int id)
        {
            var sql = "SELECT * FROM Rents WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Rent>(sql, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Rent>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Rents;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Rent>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Log Error
                return new List<Rent>();
            }
        }

        public async Task<int> Update(Rent entity)
        {
            if(entity.Status == RentStatus.Returned.ToString())
            {
                entity.ReturnDate = DateTime.Now;
            }

            var sql = "UPDATE Rents SET ReturnDate = @ReturnDate, AmountForDays = @AmountForDays, NumberOfDays = @NumberOfDays," +
                "Status = @Status, Comment = @Comment, EmployeeID = @EmployeeID, Employee = @Employee, " +
                "ClientID = @ClientID, Client = @Client, CarID = @CarID, Car = @Car WHERE ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, entity);
                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<int> UpdateByClient(int clientID, string clientName)
        {
            var sql = $"UPDATE Rents SET ClientID = {clientID}, Client = '{clientName}' WHERE ClientID = {clientID};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<int> UpdateByEmployee(int employeeID, string employeeName)
        {
            var sql = $"UPDATE Rents SET EmployeeID = {employeeID}, Employee = '{employeeName}' WHERE EmployeeID = {employeeID};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<int> UpdateByCar(int carID, string carName)
        {
            var sql = $"UPDATE Rents SET CarID = {carID}, Car = '{carName}' WHERE CarID = {carID};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Rent>> GetReport(int? id, DateTime? rentalDate, DateTime? returnDate,
            int? amountForDays, int? numberOfDays, string status, int? employeeID,
            int? clientID, int? carID)
        {
            var sql = "SELECT * FROM Rents WHERE ID = ISNULL(@ID, ID)" +
               "AND RentalDate = ISNULL(@RentalDate, RentalDate)" +
               "AND ReturnDate = ISNULL(@ReturnDate, ReturnDate)" +
               "AND AmountForDays = ISNULL(@AmountForDays, AmountForDays)" +
               "AND NumberOfDays = ISNULL(@NumberOfDays, NumberOfDays)" +
               "AND Status = ISNULL(@Status, Status)" +
               "AND EmployeeID = ISNULL(@EmployeeID, EmployeeID)" +
               "AND ClientID = ISNULL(@ClientID, ClientID)" +
               "AND CarID = ISNULL(@CarID, CarID)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("RentConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Rent>(sql, new
                {
                    ID = id,
                    RentalDate = rentalDate,
                    ReturnDate = returnDate,
                    AmountForDays = amountForDays,
                    NumberOfDays = numberOfDays,
                    Status = status,
                    EmployeeID = employeeID,
                    ClientID = clientID,
                    CarID = carID
                });
                return result;
            }
        }
    }
}
