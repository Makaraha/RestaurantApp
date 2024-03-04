using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("Orders")]
    public record Order : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }
    }
}
