namespace Infrastructure.Sql
{
    public class SqlField
    {
        public string Name { get; set; } 

        public object? Value { get; set; }

        public bool IsPrimaryKey { get; set; }
    }
}
