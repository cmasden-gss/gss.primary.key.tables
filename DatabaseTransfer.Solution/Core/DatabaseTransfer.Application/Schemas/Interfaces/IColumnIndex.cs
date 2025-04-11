namespace DatabaseTransfer.Application.Schemas.Interfaces
{
    public interface IColumnIndex
    {
        string Name { get; set; }

        int Ordinal { get; set; }
    }
}