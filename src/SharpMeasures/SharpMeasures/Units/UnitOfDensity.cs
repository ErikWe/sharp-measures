namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramPerCubicMetre", "s[Per]", new[] { "Kilogram", "CubicMetre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfVolume))]
[Unit(typeof(Density))]
public readonly partial record struct UnitOfDensity { }
