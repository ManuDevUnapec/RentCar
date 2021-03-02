using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EmployeeService.Core.Entities;
using EmployeeService.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Employee entity)
        {
            entity.Created = DateTime.Now;
            var sql = "INSERT INTO Employees (Name, IdentificationCard, HourHand, CommisionPercent, " +
                "Created, Status) " +
                "Values (@Name, @IdentificationCard, @HourHand, @CommisionPercent, @Created, @Status);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, entity);
                    return affectedRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Employees WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                    return affectedRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> Get(int id)
        {
            var sql = "SELECT * FROM Employees WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Employee>(sql, new { ID = id });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Employees;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Employee>(sql);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetReport(int? id, string name, string identificationCard, string hourHand,
            string commisionPercent, string status)
        {
            var sql = "SELECT * FROM Employees WHERE ID = ISNULL(@ID, ID)" +
               "AND Name = ISNULL(@Name, Name)" +
               "AND IdentificationCard = ISNULL(@IdentificationCard, IdentificationCard)" +
               "AND HourHand = ISNULL(@HourHand, HourHand)" +
               "AND CommisionPercent = ISNULL(@CommisionPercent, CommisionPercent)" +
               "AND Status = ISNULL(@Status, Status)";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Employee>(sql, new
                    {
                        ID = id,
                        Name = name,
                        IdentificationCard = identificationCard,
                        HourHand = hourHand,
                        CommisionPercent = commisionPercent,
                        Status = status
                    });
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Update(Employee entity)
        {
            var sql = "UPDATE Employees SET Name = @Name, IdentificationCard = @IdentificationCard, HourHand = @HourHand," +
                "CommisionPercent = @CommisionPercent, Status = @Status " +
                "WHERE ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, entity);
                    return affectedRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
