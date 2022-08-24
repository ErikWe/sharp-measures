namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.UnitInstances;

internal interface IUnitDefinition<out TLocations> : IAttributeDefinition<TLocations>, IUnitInstance
    where TLocations : IUnitLocations
{ }
