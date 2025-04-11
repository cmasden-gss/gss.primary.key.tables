using System;
using System.Threading.Tasks;
using DatabaseTransfer.Application.Models;
using Npgsql;

namespace DatabaseTransfer.Application.Postgres.Extensions
{
    public class PostgreDatabaseConnectionExtensions
    {
        /// <summary>
        ///     Is the connection string valid
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static async Task<Result<bool>> IsValidConnection(string connectionString)
        {
            try
            {
                var dbConnection = new NpgsqlConnection(connectionString);
                await dbConnection.OpenAsync();
            }
            catch (Exception e)
            {
                return Result<bool>.Fail(e);
            }

            return Result<bool>.Success(true);
        }
    }
}