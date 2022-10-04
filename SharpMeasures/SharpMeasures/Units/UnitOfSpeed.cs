namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
[DerivedUnitInstance("KilometrePerSecond", "KilometresPerSecond", new[] { "Kilometre", "Second" })]
[DerivedUnitInstance("KilometrePerHour", "KilometresPerHour", new[] { "Kilometre", "Hour" })]
[DerivedUnitInstance("FootPerSecond", "FeetPerSecond", new[] { "Foot", "Second" })]
[DerivedUnitInstance("YardPerSecond", "YardsPerSecond", new[] { "Yard", "Second" })]
[DerivedUnitInstance("MilePerHour", "MilesPerHour", new[] { "Mile", "Hour" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
[Unit(typeof(Speed))]
public readonly partial record struct UnitOfSpeed { }
