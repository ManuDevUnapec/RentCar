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
    public class TypeOfCarRepository : ITypeOfCarRepository
    {
        private readonly IConfiguration _configuration;

        public TypeOfCarRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(TypeOfCar entity)
        {
            var sql = "INSERT INTO TypeOfCars (Description, Status) " +
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
            var sql = "DELETE FROM TypeOfCars WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<TypeOfCar> Get(int id)
        {
            var sql = "SELECT * FROM TypeOfCars WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TypeOfCar>(sql, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TypeOfCar>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM TypeOfCars;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<TypeOfCar>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Log Error
                return new List<TypeOfCar>();
            }
        }

        public async Task<int> Update(TypeOfCar entity)
        {
            var sql = "UPDATE TypeOfCars SET Description = @Description, Status = @Status WHERE ID = @ID;";

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
