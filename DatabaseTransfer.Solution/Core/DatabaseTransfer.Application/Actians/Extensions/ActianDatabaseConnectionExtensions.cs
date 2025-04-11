using System;
using System.Data.Common;
using System.Threading.Tasks;
using DatabaseTransfer.Application.Models;

namespace DatabaseTransfer.Application.Actians.Extensions
{
    public static class ActianDatabaseConnectionExtensions
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
                var providerFactory = DbProviderFactories.GetFactory("Pervasive.Data.SqlClient");
                var dbConnection = providerFactory.CreateConnection();
                dbConnection.ConnectionString = connectionString;

                await dbConnection.OpenAsync();
                dbConnection.Close();
            }
            catch (Exception e)
            {
                return Result<bool>.Fail(e);
            }

            return Result<bool>.Success(true);
        }
    }
}