using System.ComponentModel;

namespace DatabaseTransfer.Application.Clients.Models
{
    /// <summary>
    ///     Transfer Database Types
    /// </summary>
    public enum TransferDatabaseTypes
    {
        [Description("Postgres Database")] PostgreDatabase,

        [Description("Microsoft Database")] MicrosoftDatabase
    }
}