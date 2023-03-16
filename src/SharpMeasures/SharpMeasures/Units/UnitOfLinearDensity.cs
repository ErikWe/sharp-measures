namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramPerMetre", "s[Per]", new[] { "Kilogram", "Metre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfLength))]
[Unit(typeof(LinearDensity))]
public readonly partial record struct UnitOfLinearDensity { }
