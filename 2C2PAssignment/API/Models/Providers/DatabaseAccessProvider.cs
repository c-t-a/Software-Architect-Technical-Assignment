using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Exceptions;
using Npgsql;
using System.Data;
using System.IO;
using System.Text;

namespace API.Models.Providers
{
    public class DatabaseAccessProvider : IDatabaseAccessProvider
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hostingEnvironment;

        public DatabaseAccessProvider(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public void CreateDatabase(string dbName)
        {
            string connectionString = configuration["ServerConnection"];
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            //SqlConnection connection = new SqlConnection(connectionString);
            try {
                NpgsqlCommand dbListCmd = new("SELECT datname FROM pg_database WHERE datistemplate = false", connection);
                //SqlCommand dbListCmd = new("SELECT name FROM (localdb)", connection);
                connection.Open();
                NpgsqlDataReader reader = dbListCmd.ExecuteReader();
                //SqlDataReader reader = dbListCmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    if (dbName == name)
                    {
                        connection.Close();
                        throw new AppException(new ResponseData
                        {
                            Code = "Database is already exit",
                            Message = "Database is already existed. Please delted that database to test this."
                        });
                    }
                }
                connection.Close();

                StringBuilder createDbSql = new();
                createDbSql.AppendFormat(@"
                CREATE DATABASE ""{0}""
                WITH OWNER = postgres
                ENCODING = 'UTF8'
                CONNECTION LIMIT = -1;
                ", dbName);
                //createDbSql.AppendFormat($"CREATE DATABASE {dbName};");

                NpgsqlCommand createDbCmd = new(createDbSql.ToString(), connection);
                //SqlCommand createDbCmd = new(createDbSql.ToString(), connection);
                connection.Open();
                createDbCmd.ExecuteNonQuery();
                connection.Close();

                string databaseURL = Path.Combine(hostingEnvironment.WebRootPath, "Database/Migrate/Group/");

                #region Create Table
                connectionString = $"{configuration["ServerConnection"]}Database={dbName}";
                connection = new NpgsqlConnection(connectionString);
                //connection = new SqlConnection(connectionString);
                string[] filePaths = Directory.GetFiles(Path.Combine(databaseURL, "Table/"));
                foreach (string filePath in filePaths)
                {
                    string sql = File.ReadAllText(filePath);
                    NpgsqlCommand createTableCmd = new NpgsqlCommand(sql, connection);
                    //SqlCommand createTableCmd = new SqlCommand(sql, connection);
                    connection.Open();
                    createTableCmd.ExecuteNonQuery();
                    connection.Close();
                }
                #endregion

                #region Create Index
                filePaths = Directory.GetFiles(Path.Combine(databaseURL, "Index/"));
                foreach (string filePath in filePaths)
                {
                    string sql = File.ReadAllText(filePath);
                    NpgsqlCommand createTableCmd = new NpgsqlCommand(sql, connection);
                    //SqlCommand createTableCmd = new SqlCommand(sql, connection);
                    connection.Open();
                    createTableCmd.ExecuteNonQuery();
                    connection.Close();
                }
                #endregion
            }
            catch{
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                throw;
            }
        }
    }
}
