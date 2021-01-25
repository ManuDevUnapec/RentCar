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
    public class BrandRepository : IBrandRepository
    {
        private readonly IConfiguration _configuration;

        public BrandRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Brand entity)
        {
            var sql = "INSERT INTO Brand (Description, Status) " +
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
            var sql = "DELETE FROM Brand WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<Brand> Get(int id)
        {
            var sql = "SELECT * FROM Brand WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Brand>(sql, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Brand;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Brand>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Log Error
                return new List<Brand>();
            }
        }

        public async Task<int> Update(Brand entity)
        {
            var sql = "UPDATE Brand SET Description = @Description, Status = @Status WHERE ID = @ID;";

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
