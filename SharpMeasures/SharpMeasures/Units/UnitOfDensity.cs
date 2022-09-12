namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("KilogramPerCubicMetre", "s[Per]", new[] { "Kilogram", "CubicMetre"} )]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfVolume))]
[SharpMeasuresUnit(typeof(Density))]
public readonly partial record struct UnitOfDensity { }
