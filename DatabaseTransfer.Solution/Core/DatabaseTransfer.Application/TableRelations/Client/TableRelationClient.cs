using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DatabaseTransfer.Application.TableRelations.Models;
using Newtonsoft.Json;

namespace DatabaseTransfer.Application.TableRelations.Client
{
    public class TableRelationClient
    {
        public List<SqlTable> GetTableRelations()
        {
            //var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "global.shop.database.transfer", "table.relations.transfer.version.json");
            //return File.Exists(filePath) ? JsonConvert.DeserializeObject<List<SqlTable>>(File.ReadAllText(filePath)) : new List<SqlTable>();

            var tableRelationString = ReadResource("table.relations.transfer.version.json");
            return !string.IsNullOrWhiteSpace(tableRelationString) ? JsonConvert.DeserializeObject<List<SqlTable>>(tableRelationString) : new List<SqlTable>();
        }

        private string ReadResource(string name)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = name;

            // Format: "{Namespace}.{Folder}.{filename}.{Extension}"
            if (!name.StartsWith(nameof(TableRelations)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(name));
            }

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}