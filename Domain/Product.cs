﻿using Domain.Attributes;
using Domain.Interfaces;
namespace Domain
{
    [TableName("Products")]
    public class Product : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int MeasurementUnitId { get; set; }
    }
}
