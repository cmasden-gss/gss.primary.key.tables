using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DatabaseTransfer.DependenciesCheck.CoreConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter plugins directory.");
                var consoleInput = Console.ReadLine();

                if (!Directory.Exists(consoleInput))
                {
                    Console.WriteLine("Invalid directory.");
                    return;
                }

                Console.WriteLine("Duplicate Dependencies:");

                var directoryFiles = Directory.GetFiles(consoleInput, "*.dll");
                var directoryFileNames = directoryFiles.Select(directoryFile => new FileInfo(directoryFile)).Select(fileInfo => fileInfo.Name).ToList();

                var dependencyFiles = GetDependencyCheckFiles();

                foreach (var dependencyFile in from dependencyFile in dependencyFiles let hasFile = directoryFileNames.Any(c => c.Equals(dependencyFile, StringComparison.InvariantCultureIgnoreCase)) where hasFile select dependencyFile)
                {
                    Console.WriteLine($"{dependencyFile}");
                }

                Console.WriteLine("---");
            }
        }

        private static List<string> GetDependencyCheckFiles()
        {
            var checkFiles = new List<string>();
            checkFiles.Add("AutoMapper.dll");
            checkFiles.Add("Costura.dll");
            checkFiles.Add("DatabaseTransfer.Application.dll");
            checkFiles.Add("DevExpress.Data.v19.2.dll");
            checkFiles.Add("DevExpress.Images.v19.2.dll");
            checkFiles.Add("DevExpress.Office.v19.2.Core.dll");
            checkFiles.Add("DevExpress.Pdf.v19.2.Core.dll");
            checkFiles.Add("DevExpress.Printing.v19.2.Core.dll");
            checkFiles.Add("DevExpress.RichEdit.v19.2.Core.dll");
            checkFiles.Add("DevExpress.Sparkline.v19.2.Core.dll");
            checkFiles.Add("DevExpress.Utils.v19.2.dll");
            checkFiles.Add("DevExpress.XtraBars.v19.2.dll");
            checkFiles.Add("DevExpress.XtraEditors.v19.2.dll");
            checkFiles.Add("DevExpress.XtraGrid.v19.2.dll");
            checkFiles.Add("DevExpress.XtraLayout.v19.2.dll");
            checkFiles.Add("DevExpress.XtraPrinting.v19.2.dll");
            checkFiles.Add("DevExpress.XtraTreeList.v19.2.dll");
            checkFiles.Add("Microsoft.Bcl.AsyncInterfaces.dll");
            checkFiles.Add("Microsoft.Win32.Primitives.dll");
            checkFiles.Add("netstandard.dll");
            checkFiles.Add("Newtonsoft.Json.dll");
            checkFiles.Add("Npgsql.dll");
            checkFiles.Add("System.AppContext.dll");
            checkFiles.Add("System.Buffers.dll");
            checkFiles.Add("System.Collections.Concurrent.dll");
            checkFiles.Add("System.Collections.dll");
            checkFiles.Add("System.Collections.NonGeneric.dll");
            checkFiles.Add("System.Collections.Specialized.dll");
            checkFiles.Add("System.ComponentModel.dll");
            checkFiles.Add("System.ComponentModel.EventBasedAsync.dll");
            checkFiles.Add("System.ComponentModel.Primitives.dll");
            checkFiles.Add("System.ComponentModel.TypeConverter.dll");
            checkFiles.Add("System.Console.dll");
            checkFiles.Add("System.Data.Common.dll");
            checkFiles.Add("System.Diagnostics.Contracts.dll");
            checkFiles.Add("System.Diagnostics.Debug.dll");
            checkFiles.Add("System.Diagnostics.FileVersionInfo.dll");
            checkFiles.Add("System.Diagnostics.Process.dll");
            checkFiles.Add("System.Diagnostics.StackTrace.dll");
            checkFiles.Add("System.Diagnostics.TextWriterTraceListener.dll");
            checkFiles.Add("System.Diagnostics.Tools.dll");
            checkFiles.Add("System.Diagnostics.TraceSource.dll");
            checkFiles.Add("System.Diagnostics.Tracing.dll");
            checkFiles.Add("System.Drawing.Primitives.dll");
            checkFiles.Add("System.Dynamic.Runtime.dll");
            checkFiles.Add("System.Globalization.Calendars.dll");
            checkFiles.Add("System.Globalization.dll");
            checkFiles.Add("System.Globalization.Extensions.dll");
            checkFiles.Add("System.IO.Compression.dll");
            checkFiles.Add("System.IO.Compression.ZipFile.dll");
            checkFiles.Add("System.IO.dll");
            checkFiles.Add("System.IO.FileSystem.dll");
            checkFiles.Add("System.IO.FileSystem.DriveInfo.dll");
            checkFiles.Add("System.IO.FileSystem.Primitives.dll");
            checkFiles.Add("System.IO.FileSystem.Watcher.dll");
            checkFiles.Add("System.IO.IsolatedStorage.dll");
            checkFiles.Add("System.IO.MemoryMappedFiles.dll");
            checkFiles.Add("System.IO.Pipes.dll");
            checkFiles.Add("System.IO.UnmanagedMemoryStream.dll");
            checkFiles.Add("System.Linq.dll");
            checkFiles.Add("System.Linq.Expressions.dll");
            checkFiles.Add("System.Linq.Parallel.dll");
            checkFiles.Add("System.Linq.Queryable.dll");
            checkFiles.Add("System.Memory.dll");
            checkFiles.Add("System.Net.Http.dll");
            checkFiles.Add("System.Net.NameResolution.dll");
            checkFiles.Add("System.Net.NetworkInformation.dll");
            checkFiles.Add("System.Net.Ping.dll");
            checkFiles.Add("System.Net.Primitives.dll");
            checkFiles.Add("System.Net.Requests.dll");
            checkFiles.Add("System.Net.Security.dll");
            checkFiles.Add("System.Net.Sockets.dll");
            checkFiles.Add("System.Net.WebHeaderCollection.dll");
            checkFiles.Add("System.Net.WebSockets.Client.dll");
            checkFiles.Add("System.Net.WebSockets.dll");
            checkFiles.Add("System.Numerics.Vectors.dll");
            checkFiles.Add("System.ObjectModel.dll");
            checkFiles.Add("System.Reflection.dll");
            checkFiles.Add("System.Reflection.Extensions.dll");
            checkFiles.Add("System.Reflection.Primitives.dll");
            checkFiles.Add("System.Resources.Reader.dll");
            checkFiles.Add("System.Resources.ResourceManager.dll");
            checkFiles.Add("System.Resources.Writer.dll");
            checkFiles.Add("System.Runtime.CompilerServices.Unsafe.dll");
            checkFiles.Add("System.Runtime.CompilerServices.VisualC.dll");
            checkFiles.Add("System.Runtime.dll");
            checkFiles.Add("System.Runtime.Extensions.dll");
            checkFiles.Add("System.Runtime.Handles.dll");
            checkFiles.Add("System.Runtime.InteropServices.dll");
            checkFiles.Add("System.Runtime.InteropServices.RuntimeInformation.dll");
            checkFiles.Add("System.Runtime.Numerics.dll");
            checkFiles.Add("System.Runtime.Serialization.Formatters.dll");
            checkFiles.Add("System.Runtime.Serialization.Json.dll");
            checkFiles.Add("System.Runtime.Serialization.Primitives.dll");
            checkFiles.Add("System.Runtime.Serialization.Xml.dll");
            checkFiles.Add("System.Security.Claims.dll");
            checkFiles.Add("System.Security.Cryptography.Algorithms.dll");
            checkFiles.Add("System.Security.Cryptography.Csp.dll");
            checkFiles.Add("System.Security.Cryptography.Encoding.dll");
            checkFiles.Add("System.Security.Cryptography.Primitives.dll");
            checkFiles.Add("System.Security.Cryptography.ProtectedData.dll");
            checkFiles.Add("System.Security.Cryptography.X509Certificates.dll");
            checkFiles.Add("System.Security.Principal.dll");
            checkFiles.Add("System.Security.SecureString.dll");
            checkFiles.Add("System.Text.Encoding.dll");
            checkFiles.Add("System.Text.Encoding.Extensions.dll");
            checkFiles.Add("System.Text.Encodings.Web.dll");
            checkFiles.Add("System.Text.Json.dll");
            checkFiles.Add("System.Text.RegularExpressions.dll");
            checkFiles.Add("System.Threading.Channels.dll");
            checkFiles.Add("System.Threading.dll");
            checkFiles.Add("System.Threading.Overlapped.dll");
            checkFiles.Add("System.Threading.Tasks.dll");
            checkFiles.Add("System.Threading.Tasks.Extensions.dll");
            checkFiles.Add("System.Threading.Tasks.Parallel.dll");
            checkFiles.Add("System.Threading.Thread.dll");
            checkFiles.Add("System.Threading.ThreadPool.dll");
            checkFiles.Add("System.Threading.Timer.dll");
            checkFiles.Add("System.ValueTuple.dll");
            checkFiles.Add("System.Xml.ReaderWriter.dll");
            checkFiles.Add("System.Xml.XDocument.dll");
            checkFiles.Add("System.Xml.XmlDocument.dll");
            checkFiles.Add("System.Xml.XmlSerializer.dll");
            checkFiles.Add("System.Xml.XPath.dll");
            checkFiles.Add("System.Xml.XPath.XDocument.dll");

            return checkFiles;
        }
    }
}