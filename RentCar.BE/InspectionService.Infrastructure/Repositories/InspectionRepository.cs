using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InspectionService.Core.Entities;
using InspectionService.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InspectionService.Infrastructure.Repositories
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly IConfiguration _configuration;

        public InspectionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Inspection entity)
        {
            entity.InspectionDate = DateTime.Now;
            var sql = "INSERT INTO Inspections (InspectionDate, Grated, Cat, RubberBack, " +
                "GlassBreak, RubberStateOne, RubberStateTwo, RubberStateThree, RubberStateFour, AmountOfFuel," +
                "Status,EmployeeID, Employee, ClientID, Client, CarID, Car) " +
                "Values (@InspectionDate, @Grated, @Cat, @RubberBack, @GlassBreak, @RubberStateOne," +
                "@RubberStateTwo, @RubberStateThree, @RubberStateFour, @AmountOfFuel, @Status, @EmployeeID, " +
                "@Employee, @ClientID, @Client, @CarID, @Car);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
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
            var sql = "DELETE FROM Inspections WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<Inspection> Get(int id)
        {
            var sql = "SELECT * FROM Inspections WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Inspection>(sql, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Inspection>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Inspections;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Inspection>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Log Error
                return new List<Inspection>();
            }
        }

        public async Task<int> Update(Inspection entity)
        {
            var sql = "UPDATE Inspections SET Grated = @Grated, Cat = @Cat, InspectionDate = @InspectionDate," +
                "RubberBack = @RubberBack, GlassBreak = @GlassBreak, RubberStateOne = @RubberStateOne, RubberStateTwo = @RubberStateTwo, " +
                "RubberStateThree = @RubberStateThree, RubberStateFour = @RubberStateFour, AmountOfFuel = @AmountOfFuel, Status = @Status, " +
                "EmployeeID = @EmployeeID, Employee = @Employee, ClientID = @ClientID, Client = @Client, " +
                "CarID = @CarID, Car = @Car WHERE ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
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
    }
}
