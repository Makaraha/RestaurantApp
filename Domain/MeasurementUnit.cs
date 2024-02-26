using Domain.Attributes;

namespace Domain
{
    [TableName("MeasurementUnits")]
    public class MeasurementUnit
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public void Test() { }
    }
}
