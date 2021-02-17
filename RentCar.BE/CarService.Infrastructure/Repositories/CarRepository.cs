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
    public class CarRepository : ICarRepository
    {
        private readonly IConfiguration _configuration;

        public CarRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Car entity)
        {
            var sql = "INSERT INTO Cars (Description, ChassisNumber, EngineNumber, PlateNumber, " +
                "Status, TypeOfFuelID, TypeOfCarID, BrandID, ModelID) " +
                "Values (@Description, @ChassisNumber, @EngineNumber, @PlateNumber, @Status, @TypeOfFuelID," +
                "@TypeOfCarID, @BrandID, @ModelID);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, entity);
                    return affectedRows;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Cars WHERE ID = @ID;";
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

        public async Task<Car> Get(int id)
        {
            var sql = "SELECT ca.ID, ca.Description, ca.ChassisNumber, ca.EngineNumber, ca.PlateNumber," +
                    "ca.Status, ca.TypeOfFuelID, ca.TypeOfCarID, ca.BrandID, ca.ModelID, " +
                    "br.Description as Brand, mo.Description as Model, tc.Description as TypeOfCar, " +
                    "tf.Description as TypeOfFuel FROM Cars ca " +
                    "INNER JOIN Brand br on ca.BrandID = br.ID INNER JOIN Model mo on ca.ModelID = mo.ID " +
                    "INNER JOIN TypeOfCars tc on ca.TypeOfCarID = tc.ID " +
                    "INNER JOIN TypeOfFuels tf on ca.TypeOfFuelID = tf.ID WHERE ca.ID = @ID";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Car>(sql, new { ID = id });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            try
            {
                var sql = "SELECT ca.ID, ca.Description, ca.ChassisNumber, ca.EngineNumber, ca.PlateNumber," +
                    "ca.Status, ca.TypeOfFuelID, ca.TypeOfCarID, ca.BrandID, ca.ModelID, " +
                    "br.Description as Brand, mo.Description as Model, tc.Description as TypeOfCar, tf.Description as TypeOfFuel FROM Cars ca " +
                    "INNER JOIN Brand br on ca.BrandID = br.ID INNER JOIN Model mo on ca.ModelID = mo.ID INNER JOIN TypeOfCars tc on ca.TypeOfCarID = tc.ID INNER JOIN TypeOfFuels tf on ca.TypeOfFuelID = tf.ID";


                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Car>(sql);
                    return result;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Car>> GetReport(int? id, string description, string status,
            int? brandID, int? modelID, int? typeOfCarID, int? typeOfFuelID, string plateNumber,
            string engineNumber, string chassisNumber)
        {
            try
            {
                var sql = "SELECT ca.ID, ca.Description, ca.ChassisNumber, ca.EngineNumber, ca.PlateNumber," +
                    "ca.Status, ca.TypeOfFuelID, ca.TypeOfCarID, ca.BrandID, ca.ModelID, " +
                    "br.Description as Brand, mo.Description as Model, tc.Description as TypeOfCar, " +
                    "tf.Description as TypeOfFuel FROM Cars ca " +
                    "INNER JOIN Brand br on ca.BrandID = br.ID INNER JOIN Model mo on ca.ModelID = mo.ID " +
                    "INNER JOIN TypeOfCars tc on ca.TypeOfCarID = tc.ID " +
                    "INNER JOIN TypeOfFuels tf on ca.TypeOfFuelID = tf.ID " +
                    "WHERE ca.ID = ISNULL(@ID, ca.ID) " +
                   "AND ca.Status = ISNULL(@Status, ca.Status) " +
                   "AND ca.Description = ISNULL(@Description, ca.Description) " +
                   "AND ca.BrandID = ISNULL(@BrandID, ca.BrandID) " +
                   "AND ca.ModelID = ISNULL(@ModelID, ca.ModelID) " +
                   "AND ca.TypeOfCarID = ISNULL(@TypeOfCarID, ca.TypeOfCarID) " +
                   "AND ca.TypeOfFuelID = ISNULL(@TypeOfFuelID, ca.TypeOfFuelID) " +
                   "AND ca.PlateNumber = ISNULL(@PlateNumber, ca.PlateNumber) " +
                   "AND ca.EngineNumber = ISNULL(@EngineNumber, ca.EngineNumber) " +
                   "AND ca.ChassisNumber = ISNULL(@ChassisNumber, ca.ChassisNumber);";

                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Car>(sql, new
                    {
                        ID = id,
                        Status = status,
                        BrandID = brandID,
                        ModelID = modelID,
                        TypeOfCarID = typeOfCarID,
                        TypeOfFuelID = typeOfFuelID,
                        PlateNumber = plateNumber,
                        EngineNumber = engineNumber,
                        ChassisNumber = chassisNumber,
                        Description = description
                    });
                    return result;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task<int> Update(Car entity)
        {
            var sql = "UPDATE Cars SET Description = @Description, ChassisNumber = @ChassisNumber, EngineNumber = @EngineNumber," +
                "PlateNumber = @PlateNumber, Status = @Status, TypeOfFuelID = @TypeOfFuelID," +
                "TypeOfCarID = @TypeOfCarID, BrandID = @BrandID, ModelID = @ModelID WHERE ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, entity);
                    return affectedRows;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
