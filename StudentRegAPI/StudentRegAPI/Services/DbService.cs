using Microsoft.Data.SqlClient;
using StudentRegAPI.Services.IServices;
using System.Data;
using System.Data.Common;

namespace StudentRegAPI.Services
{
    public class DbService : IDbService
    {
        private string _connectionString;
        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection ConnectDatabase()
        {
            try
            {

                return new SqlConnection(_connectionString);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
