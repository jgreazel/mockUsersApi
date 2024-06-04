using Dapper;
using System.Data;
using System.Data.SqlClient;
using UsersApi.Models;

namespace UsersApi.Data
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IConfiguration configuration)
        {
            _dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbConnection.Query<User>("SELECT * FROM Users");
        }

        public User GetUser(int id)
        {
            return _dbConnection.QuerySingle<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }

        public User CreateUser(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", user.Name);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _dbConnection.Execute("InsertUser", parameters, commandType: CommandType.StoredProcedure);

            user.Id = parameters.Get<int>("@Id");
            return user;
        }

        public User UpdateUser(int id, User user)
        {
            var existingUser = GetUser(id);
            if (existingUser == null)
                return null;

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@Name", user.Name);
            parameters.Add("@Email", user.Email);

            _dbConnection.Execute("UpdateUser", parameters, commandType: CommandType.StoredProcedure);

            return GetUser(id);
        }

        public bool DeleteUser(int id)
        {
            var existingUser = GetUser(id);
            if (existingUser == null)
                return false;

            _dbConnection.Execute("DeleteUser", new { Id = id }, commandType: CommandType.StoredProcedure);
            return true;
        }
    }
}