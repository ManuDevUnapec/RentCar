using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ClientService.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Client entity)
        {
            var sql = "INSERT INTO Clients (Name, IdentificationCard, CardNumber, CreditLimit, " +
                "PersonType, Status) " +
                "Values (@Name, @IdentificationCard, @CardNumber, @CreditLimit, @PersonType, @Status);";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ClientConnection")))
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
            var sql = "DELETE FROM Clients WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ClientConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { ID = id });
                return affectedRows;
            }
        }

        public async Task<Client> Get(int id)
        {
            var sql = "SELECT * FROM Clients WHERE ID = @ID;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ClientConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Client>(sql, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Clients;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ClientConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Client>(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Log Error
                return new List<Client>();
            }
        }

        public async Task<IEnumerable<Client>> GetReport(int? id, string name, string identificationCard,
            string cardNumber, int? creditLimit, string personType, string status)
        {
            var sql = "SELECT * FROM Clients WHERE ID = ISNULL(@ID, ID)" +
               "AND Name = ISNULL(@Name, Name)" +
               "AND IdentificationCard = ISNULL(@IdentificationCard, IdentificationCard)" +
               "AND CardNumber = ISNULL(@CardNumber, CardNumber)" +
               "AND CreditLimit = ISNULL(@CreditLimit, CreditLimit)" +
               "AND PersonType = ISNULL(@PersonType, PersonType)" +
               "AND Status = ISNULL(@Status, Status)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ClientConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Client>(sql, new
                {
                    ID = id,
                    Name = name,
                    IdentificationCard = identificationCard,
                    CardNumber = cardNumber,
                    CreditLimit = creditLimit,
                    PersonType = personType,
                    Status = status
                });
                return result;
            }
        }

        public async Task<int> Update(Client entity)
        {
            var sql = "UPDATE Clients SET Name = @Name, IdentificationCard = @IdentificationCard, CardNumber = @CardNumber," +
                "CreditLimit = @CreditLimit, Status = @Status, PersonType = @PersonType " +
                "WHERE ID = @ID;";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ClientConnection")))
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
