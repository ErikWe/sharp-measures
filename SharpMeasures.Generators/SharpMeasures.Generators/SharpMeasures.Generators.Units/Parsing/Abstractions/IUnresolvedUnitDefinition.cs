namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal interface IUnresolvedUnitDefinition<out TLocations> : IAttributeDefinition<TLocations>, IUnresolvedUnitInstance
    where TLocations : IUnitLocations
{ }
