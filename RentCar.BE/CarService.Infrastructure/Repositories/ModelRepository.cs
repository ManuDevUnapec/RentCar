using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CarService.Core.Entities;
using CarService.Core.Interfaces;
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace CarService.Infrastructure.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly IConfiguration _configuration;

        public ModelRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Model entity)
        {
            var sql = "INSERT INTO Model (Description, Status, BrandID) " +
                "Values (@Description, @Status, @BrandID);";

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
            var sql = "DELETE FROM Model WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<Model> Get(int id)
        {
            var sql = "SELECT * FROM Model WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Model>(sql, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Model;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Model>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Log Error
                return new List<Model>();
            }
        }

        public async Task<int> Update(Model entity)
        {
            var sql = "UPDATE Model SET Description = @Description, Status = @Status, BrandID = @BrandID WHERE ID = @ID;";

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
