namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("MetrePerSecond", "s[Per]", new[] { "Metre", "Second" })]
[DerivedUnitInstance("KilometrePerSecond", "s[Per]", new[] { "Kilometre", "Second" })]
[DerivedUnitInstance("KilometrePerHour", "s[Per]", new[] { "Kilometre", "Hour" })]
[DerivedUnitInstance("FootPerSecond", "FeetPerSecond", new[] { "Foot", "Second" })]
[DerivedUnitInstance("YardPerSecond", "s[Per]", new[] { "Yard", "Second" })]
[DerivedUnitInstance("MilePerHour", "s[Per]", new[] { "Mile", "Hour" })]
[DerivedUnitInstance("Knot", "[*]s", new[] { "NauticalMile", "Hour" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
[Unit(typeof(Speed))]
public readonly partial record struct UnitOfSpeed { }
