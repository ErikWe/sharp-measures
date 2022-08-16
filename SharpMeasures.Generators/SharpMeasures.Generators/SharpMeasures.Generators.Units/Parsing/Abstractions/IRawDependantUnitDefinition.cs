namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal interface IRawDependantUnitDefinition<out TLocations> : IRawUnitDefinition<TLocations>, IRawDependantUnitInstance
    where TLocations : IDependantUnitLocations
{ }
