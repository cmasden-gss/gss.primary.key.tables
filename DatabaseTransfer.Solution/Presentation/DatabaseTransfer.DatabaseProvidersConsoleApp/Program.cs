using System;
using System.Data.Common;
using System.Data.OleDb;
using DatabaseTransfer.DatabaseProvidersConsoleApp.Extensions;

namespace DatabaseTransfer.DatabaseProvidersConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var oleDbEnumerator = new OleDbEnumerator();
            var oldDbElements = oleDbEnumerator.GetElements();

            oldDbElements.Print();

            var dbProviderFactories = DbProviderFactories.GetFactoryClasses();
            dbProviderFactories.Print();

            Console.Read();
        }
    }
}