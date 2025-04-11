using System;
using System.Linq;
using TableRelations.Application.DataTransfers;
using TableRelations.Application.KeyMappers;

namespace TableRelations.FrameworkConsoleApp
{
    /// <summary>
    ///     System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 - .NET Framework
    ///     System.DateTime, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e - .NET
    ///     Core
    /// </summary>
    /// <remarks>
    ///     Framework edition had to be made because we are too lazy to see the big picture and you can't deserialize Types.
    /// </remarks>
    internal class Startup
    {
        private static void Main(string[] arguments)
        {
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