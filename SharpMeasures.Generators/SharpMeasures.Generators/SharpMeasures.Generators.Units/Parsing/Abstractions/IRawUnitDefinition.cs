namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal interface IRawUnitDefinition<out TLocations> : IAttributeDefinition<TLocations>, IRawUnitInstance
    where TLocations : IUnitLocations
{ }
