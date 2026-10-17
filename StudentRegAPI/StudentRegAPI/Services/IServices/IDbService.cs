using System.Data;

namespace StudentRegAPI.Services.IServices
{
    public interface IDbService
    {

        public IDbConnection ConnectDatabase();
    }
}
