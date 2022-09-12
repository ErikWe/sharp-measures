namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("MetrePerSecondSquared", "MetresPerSecondSquared", new[] { "MetrePerSecond", "Second" })]
[DerivedUnitInstance("FootPerSecondSquared", "FeetPerSecondSquared", new[] { "FootPerSecond", "Second" })]
[DerivedUnitInstance("KilometrePerHourPerSecond", "KilometresPerHourPerSecond", new[] { "KilometrePerHour", "Second" })]
[DerivedUnitInstance("MilePerHourPerSecond", "MilesPerHourPerSecond", new[] { "MilePerHour", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfSpeed), typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(Acceleration))]
public readonly partial record struct UnitOfAcceleration { }
