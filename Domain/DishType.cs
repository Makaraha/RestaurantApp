using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("DishTypes")]
    public record DishType : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
