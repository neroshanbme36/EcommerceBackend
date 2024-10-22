
namespace Application.Helpers
{
    public static class SqlServerHelper
    {
        public static string GetMssqlConnectionString(string serverName, string databaseName, int port, string password)
        {
            string connectionString = string.Empty;
            connectionString += port == 0 ? $"Data Source={serverName};" : $"Data Source={serverName},{port};";
            // Set Integrated Security to false if you are going to be providing the username and password.
            connectionString += $"Initial Catalog={databaseName};User Id=sa;Password={password};TrustServerCertificate=true;MultipleActiveResultSets=true";
            // connectionString += $"Initial Catalog={databaseName};Trusted_Connection=True;MultipleActiveResultSets=true";
            return connectionString;
        }

        public static string GetMySqlConnectionString(string serverName, int port, string databaseName, string username, string password, bool isSsl)
        {
            string connectionString = string.Empty;
            connectionString += port == 0 ? $"Server={serverName};Port=3306;" : $"Server={serverName};Port={port};";
            connectionString += $"Database={databaseName};User={username};Password={password};";
            if (!isSsl) connectionString += "SSL Mode=None;";
            return connectionString;
        }
    }
}