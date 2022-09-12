namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("CubicMetrePerKilogram", "s[Per]", new[] { "CubicMetre", "Kilogram" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfVolume), typeof(UnitOfMass))]
[SharpMeasuresUnit(typeof(SpecificVolume))]
public readonly partial record struct UnitOfSpecificVolume { }
