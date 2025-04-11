using System;
using System.Threading.Tasks;
using Npgsql;

namespace DatabaseTransfer.PostgresConnection.CoreConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter connection string, then press enter.");
                await IsValidConnection(Console.ReadLine());

                Console.WriteLine("\n-----\n");
            }
        }

        public static async Task<bool> IsValidConnection(string connectionString)
        {
            try
            {
                var dbConnection = new NpgsqlConnection(connectionString);
                await dbConnection.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed Connection.");
                Console.WriteLine(e);
                return false;
            }

            Console.WriteLine("Connected.");
            return true;
        }
    }
}