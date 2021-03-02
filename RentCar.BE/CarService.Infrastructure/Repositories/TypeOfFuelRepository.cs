using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using CarService.Core.Entities;
using CarService.Core.Interfaces;

namespace CarService.Infrastructure.Repositories
{
    public class TypeOfFuelRepository : ITypeOfFuelRepository
    {
        private readonly IConfiguration _configuration;

        public TypeOfFuelRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(TypeOfFuel entity)
        {
            var sql = "INSERT INTO TypeOfFuels (Description, Status) " +
                "Values (@Description, @Status);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
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
            var sql = "DELETE FROM TypeOfFuels WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
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

        public async Task<TypeOfFuel> Get(int id)
        {
            var sql = "SELECT * FROM TypeOfFuels WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<TypeOfFuel>(sql, new { ID = id });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TypeOfFuel>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM TypeOfFuels;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<TypeOfFuel>(sql);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TypeOfFuel>> GetReport(int? id, string description, string status)
        {
            var sql = "SELECT * FROM TypeOfFuels WHERE ID = ISNULL(@ID, ID)" +
                "AND Description = ISNULL(@Description, Description)" +
                "AND Status = ISNULL(@Status, Status);";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<TypeOfFuel>(sql, new
                    {
                        ID = id,
                        Description = description,
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

        public async Task<int> Update(TypeOfFuel entity)
        {
            var sql = "UPDATE TypeOfFuels SET Description = @Description, Status = @Status WHERE ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
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
