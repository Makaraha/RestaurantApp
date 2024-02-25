using Domain.Attributes;
namespace Domain
{
    [TableName("Products")]
    public class Product
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int MeasurementUnitId { get; set; }
    }
}
