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
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Inspections WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
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

        public async Task<Inspection> Get(int id)
        {
            var sql = "SELECT * FROM Inspections WHERE ID = @ID;";
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Inspection>(sql, new { ID = id });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateByClient(int clientID, string clientName)
        {
            var sql = $"UPDATE Inspections SET ClientID = {clientID}, Client = '{clientName}' WHERE ClientID = {clientID};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateByEmployee(int employeeID, string employeeName)
        {
            var sql = $"UPDATE Inspections SET EmployeeID = {employeeID}, Employee = '{employeeName}' WHERE EmployeeID = {employeeID};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateByCar(int carID, string carName)
        {
            var sql = $"UPDATE Inspections SET CarID = {carID}, Car = '{carName}' WHERE CarID = {carID};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Inspection>> GetReport(int? id, bool? grated, bool? cat,
            bool? rubberBack, bool? glassBreak, bool? rubberStateOne, bool? rubberStateTwo, bool? rubberStateThree,
            bool? rubberStateFour, string amountOfFuel, string status, int? employeeid, int? clientID, int? carID)
        {
            var sql = "SELECT * FROM Inspections WHERE ID = ISNULL(@ID, ID)" +
               "AND Grated = ISNULL(@Grated, Grated)" +
               "AND Cat = ISNULL(@Cat, Cat)" +
               "AND RubberBack = ISNULL(@RubberBack, RubberBack)" +
               "AND GlassBreak = ISNULL(@GlassBreak, GlassBreak)" +
               "AND RubberStateOne = ISNULL(@RubberStateOne, RubberStateOne)" +
               "AND RubberStateTwo = ISNULL(@RubberStateTwo, RubberStateTwo)" +
               "AND RubberStateThree = ISNULL(@RubberStateThree, RubberStateThree)" +
               "AND RubberStateFour = ISNULL(@RubberStateFour, RubberStateFour)" +
               "AND AmountOfFuel = ISNULL(@AmountOfFuel, AmountOfFuel)" +
               "AND Status = ISNULL(@Status, Status)" +
               "AND EmployeeID = ISNULL(@EmployeeID, EmployeeID)" +
               "AND ClientID = ISNULL(@ClientID, ClientID)" +
               "AND CarID = ISNULL(@CarID, CarID)";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("InspectionConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Inspection>(sql, new
                    {
                        ID = id,
                        Grated = grated,
                        Cat = cat,
                        RubberBack = rubberBack,
                        GlassBreak = glassBreak,
                        RubberStateOne = rubberStateOne,
                        RubberStateTwo = rubberStateTwo,
                        RubberStateThree = rubberStateThree,
                        RubberStateFour = rubberStateFour,
                        AmountOfFuel = amountOfFuel,
                        Status = status,
                        EmployeeID = employeeid,
                        ClientID = clientID,
                        CarID = carID,
                    });
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
