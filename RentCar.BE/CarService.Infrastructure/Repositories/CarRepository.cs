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
            catch(Exception)
            {
                return 0;
            }
            
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Cars WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<Car> Get(int id)
        {
            var sql = "SELECT ca.ID, ca.Description, ca.ChassisNumber, ca.EngineNumber, ca.PlateNumber," +
                    "ca.Status, ca.TypeOfFuelID, ca.TypeOfCarID, ca.BrandID, ca.ModelID, " +
                    "br.Description as Brand, mo.Description as Model, tc.Description as TypeOfCar, tf.Description as TypeOfFuel FROM Cars ca " +
                    "INNER JOIN Brand br on ca.BrandID = br.ID INNER JOIN Model mo on ca.ModelID = mo.ID INNER JOIN TypeOfCars tc on ca.TypeOfCarID = tc.ID INNER JOIN TypeOfFuels tf on ca.TypeOfFuelID = tf.ID WHERE ca.ID = @ID";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("CarConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Car>(sql, new { ID = id });
                return result.FirstOrDefault();
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
                //Log Error
                return new List<Car>();
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
            }catch(Exception)
            {
                return 0;
            }
        }
    }
}
