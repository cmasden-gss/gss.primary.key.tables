using System;

namespace TableRelations.Application.DataTransfers.Models
{
    public class SqlColumnMask
    {
        public SqlColumnMask(int dataTypeId, string format)
        {
            //[DL_DataTypes]
            switch (dataTypeId)
            {
                case 1:
                    DataType = typeof(bool);
                    break;

                case 2:
                    DataType = typeof(string);
                    break;

                case 3:
                    DataType = typeof(DateTime);
                    break;

                case 4:
                    DataType = typeof(double);
                    break;

                case 5:
                    DataType = typeof(int);
                    break;

                case 50:
                    DataType = typeof(TimeSpan);
                    break;
            }

            Format = format;
        }

        public Type DataType { get; set; }

        public string Format { get; set; }

        public override string ToString()
        {
            return $"{DataType}: {Format}";
        }
    }
}