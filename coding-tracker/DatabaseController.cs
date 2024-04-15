using Dapper;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    internal class DatabaseController
    {
        private string connectionString = @"Data Source=coding-tracker.db";

        internal void Insert(CodingSession coding)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = $"INSERT INTO codingSession(StartTime, EndTime, Duration) VALUES('{coding.StartTime}', '{coding.EndTime}', '{coding.Duration}')";

                var session = new { StartTime = coding.StartTime, EndTime = coding.EndTime, Duration = coding.Duration };

                connection.Execute(sql, session);
            }
        }

        internal List<CodingSession> Read()

        {
            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = @"SELECT * FROM codingSession";

                var codingSessions = connection.Query<CodingSession>(sql).ToList();

                foreach (var session in codingSessions)
                {
                    Console.WriteLine($"{session.Id} Start time: {session.StartTime} Endtime: {session.EndTime} Duration: {session.Duration}");
                }

                return codingSessions;
            }
        }
    }
}