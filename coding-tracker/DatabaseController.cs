﻿using Dapper;
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
                if (codingSessions.Any())
                {
                    foreach (var session in codingSessions)
                    {
                        Console.WriteLine($"{session.Id} Start time: {session.StartTime} Endtime: {session.EndTime} Duration: {session.Duration}");
                    }
                }
                else Console.WriteLine("No rows found");

                return codingSessions;
            }
        }

        internal void Delete(CodingSession coding)
        {
            var sql = $"DELETE FROM codingSession WHERE Id = {coding.Id}";

            using (var connection = new SqliteConnection(connectionString))
            {
                var deletedRows = connection.Execute(sql);
                if (deletedRows == 0) Console.WriteLine("Rows can not be deleted or does not exist");
            }
        }

        internal void Update(CodingSession coding)
        {
            var sql = $@"UPDATE codingSession SET StartTime = {coding.StartTime}, EndTime = {coding.EndTime}, Duration = {coding.Duration} WHERE Id = {coding.Id}";

            using (var connection = new SqliteConnection(connectionString))
            {
                if (coding.Id == 0) Console.Write("no rows available to edit");
                var updatedRows = connection.Execute(sql);
            }
        }
    }
}