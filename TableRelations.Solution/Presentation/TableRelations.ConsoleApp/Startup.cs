using System;
using System.Linq;
using TableRelations.Application.DataTransfers;
using TableRelations.Application.KeyMappers;

namespace TableRelations.ConsoleApp
{
    internal class Startup
    {
        private static void Main(string[] arguments)
        {
            //    [Trunk] = 45
            //    [V2018_1] = 44
            //    [V2017_1] = 43
            //    [V2017_0] = 42
            //    [V2016_2] = 40
            //    [V2016_1] = 38
            //    [V2015_3] = 37
            //    [V2015_2] = 36
            //    [V2015_1] = 35

            if (arguments.Length > 0)
            {
                var argumentList = arguments.OfType<string>().ToList();

                var releaseVersionId = int.Parse(argumentList.First());

                KeyMapperRoutine.HistoryRoutine(releaseVersionId);
                DataTransferRoutine.HistoryRoutine(releaseVersionId);
            }

            Console.WriteLine("No release version was passed.");
        }
    }
}