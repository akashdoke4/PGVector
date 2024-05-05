using Npgsql;
using Pgvector.Npgsql;

namespace PGVectorPOC.Helper
{
    public class DBHelper
    {
        private readonly IConfiguration _configuration;
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection GetConnection()
        {
            string constring = _configuration["ConnectionStrings:PostgresConnection"] ?? throw new ArgumentNullException("PostgresConnection is null");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(constring);
            dataSourceBuilder.UseVector();
            var dataSource = dataSourceBuilder.Build();
            var conn = dataSource.OpenConnection();
            return conn;
        }   
    }
}
