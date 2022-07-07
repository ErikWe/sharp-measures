namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal interface IUnresolvedDependantUnitDefinition<out TLocations> : IUnresolvedUnitDefinition<TLocations>, IUnresolvedDependantUnitInstance
    where TLocations : IDependantUnitLocations
{ }
