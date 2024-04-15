using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    internal class DatabaseCreation
    {
        internal void CreateDatabase()
        {
            string connectionString = @"Data Source=coding-tracker.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "CREATE TABLE IF NOT EXISTS codingSession( Id INTEGER PRIMARY KEY AUTOINCREMENT, StartTime TEXT, EndTime TEXT, Duration INTEGER )";

                tableCmd.ExecuteNonQuery();
            };
        }
    }
}