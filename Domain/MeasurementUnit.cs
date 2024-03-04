﻿using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("MeasurementUnits")]
    public class MeasurementUnit : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public void Test() { }
    }
}
