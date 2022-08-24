namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Units.UnitInstances;

internal interface IDependantUnitDefinition<out TLocations> : IUnitDefinition<TLocations>, IDependantUnitInstance
    where TLocations : IDependantUnitLocations
{ }
