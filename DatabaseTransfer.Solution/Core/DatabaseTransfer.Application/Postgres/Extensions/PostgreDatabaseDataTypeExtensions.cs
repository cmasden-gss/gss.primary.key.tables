using System;
using NpgsqlTypes;

namespace DatabaseTransfer.Application.Postgres.Extensions
{
    public static class PostgreDatabaseDataTypeExtensions
    {
        public static NpgsqlDbType GetPostgreDatabaseDataType(Type dataType)
        {
            var columnType = dataType.ToString();
            switch (columnType)
            {
                case "System.Int32":
                    {
                        return NpgsqlDbType.Integer;
                    }

                case "System.Int64":
                    {
                        return NpgsqlDbType.Bigint;
                    }

                case "System.Int16":
                    {
                        return NpgsqlDbType.Smallint;
                    }

                case "System.Byte":
                case "System.SByte":
                    {
                        return NpgsqlDbType.Bytea;
                    }

                case "System.Boolean":
                    {
                        return NpgsqlDbType.Boolean;
                    }

                case "System.Decimal":
                    {
                        return NpgsqlDbType.Numeric;
                    }

                case "System.DateTime":
                    {
                        return NpgsqlDbType.Date;
                    }

                case "System.TimeSpan":
                    {
                        return NpgsqlDbType.Interval;
                    }

                case "System.String":
                default:
                    {
                        return NpgsqlDbType.Varchar;
                    }
            }
        }
    }
}