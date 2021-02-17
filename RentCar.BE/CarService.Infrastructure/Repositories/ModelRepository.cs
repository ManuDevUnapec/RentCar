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
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Model WHERE ID = @ID;";
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

        public async Task<Model> Get(int id)
        {
            var sql = "SELECT M.ID, m.[Description], m.[Status], b.[Description] as Brand, b.ID as BrandID " +
                    "from Model m INNER JOIN Brand b on m.BrandID = b.ID WHERE m.ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Model>(sql, new { ID = id });
                    return result.FirstOrDefault();
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            try
            {
                var sql = "SELECT M.ID, m.[Description], m.[Status], b.[Description] as Brand, b.ID as BrandID " +
                    "from Model m INNER JOIN Brand b on m.BrandID = b.ID;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Model>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Model>> GetReport(int? id, string description, string status, int? brandID)
        {
            var sql = "SELECT m.ID as ID, m.Description as Description, m.Status as Status," +
                "m.BrandID as BrandID, b.Description as Brand " +
                "FROM Model m INNER JOIN Brand b on m.BrandID = b.ID WHERE m.ID = ISNULL(@ID, m.ID)" +
                "AND m.Description = ISNULL(@Description, m.Description)" +
                "AND m.Status = ISNULL(@Status, m.Status)" +
                "AND m.BrandID = ISNULL(@BrandID, m.BrandID);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Model>(sql, new
                    {
                        ID = id,
                        Description = description,
                        Status = status,
                        BrandID = brandID
                    });
                    return result;
                }
            }catch(Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
