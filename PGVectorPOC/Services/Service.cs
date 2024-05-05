using Dapper;
using Npgsql;
using Pgvector;
using Pgvector.Dapper;
using Pgvector.Npgsql;
using PGVectorPOC.Helper;
using PGVectorPOC.Services.Interface;

namespace PGVectorPOC.Services
{
    public class Service : IService
    {
        private readonly DBHelper _dBHelper;
        public Service(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }
        public async Task CreateTable()
        {
            try
            {
                using (var conn = _dBHelper.GetConnection())
                {

                    SqlMapper.AddTypeHandler(new VectorTypeHandler());
                    conn.Execute("CREATE EXTENSION IF NOT EXISTS vector");
                    conn.ReloadTypes();

                    conn.Execute("DROP TABLE IF EXISTS dapper_items");
                    conn.Execute("CREATE TABLE dapper_items (id serial PRIMARY KEY, embedding vector(3))");

                    var embedding1 = new Vector(new float[] { 1, 1, 1 });
                    var embedding2 = new Vector(new float[] { 2, 2, 2 });
                    var embedding3 = new Vector(new float[] { 1, 1, 2 });
                    var embedding4 = new Vector(new float[] { 3, 1, 2 });
                    var embedding5 = new Vector(new float[] { 2, 6, 2 });
                    var embedding6 = new Vector(new float[] { 5, 1, 9 });

                    conn.Execute(@"INSERT INTO dapper_items (embedding) VALUES (@embedding1), (@embedding2), (@embedding3), (@embedding4), (@embedding5), (@embedding6)", new { embedding1, embedding2, embedding3, embedding4, embedding5, embedding6 });

                }

            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;

                throw;
            }
        }
    }
}
