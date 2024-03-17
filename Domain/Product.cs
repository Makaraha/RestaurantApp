using Domain.Attributes;
using Domain.Interfaces;
namespace Domain
{
    [TableName("Products")]
    public record Product : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public int MeasurementUnitId { get; set; }
    }
}
