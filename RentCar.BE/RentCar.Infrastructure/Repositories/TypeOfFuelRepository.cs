using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using RentCar.Core.Entities;
using RentCar.Core.Interfaces;

namespace RentCar.Infrastructure.Repositories
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
                return 0;
            }

        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM TypeOfFuels WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<TypeOfFuel> Get(int id)
        {
            var sql = "SELECT * FROM TypeOfFuels WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TypeOfFuel>(sql, new { ID = id });
                return result.FirstOrDefault();
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
            catch (Exception e)
            {
                //Log Error
                return new List<TypeOfFuel>();
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
                return 0;
            }
        }
    }
}
