namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("CubicMetrePerKilogram", "s[Per]", new[] { "CubicMetre", "Kilogram" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfVolume), typeof(UnitOfMass))]
[Unit(typeof(SpecificVolume))]
public readonly partial record struct UnitOfSpecificVolume { }
