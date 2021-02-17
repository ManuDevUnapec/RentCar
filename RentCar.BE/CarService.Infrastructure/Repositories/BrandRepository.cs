using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CarService.Core.Entities;
using CarService.Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;


namespace CarService.Infrastructure.Repositories
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
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Brand WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                    return affectedRows;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Brand> Get(int id)
        {
            var sql = "SELECT * FROM Brand WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Brand>(sql, new { ID = id });
                    return result.FirstOrDefault();
                }
            }catch(Exception e)
            {
                throw e;
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
                throw e;
            }
        }

        public async Task<IEnumerable<Brand>> GetReport(int? id, string description, string status)
        {
            var sql = "SELECT * FROM Brand WHERE ID = ISNULL(@ID, ID)" +
                "AND Description = ISNULL(@Description, Description)" +
                "AND Status = ISNULL(@Status, Status);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Brand>(sql, new { ID = id, Description = description,
                    Status = status});
                    return result;
                }
            }catch(Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
