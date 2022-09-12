namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("KilogramPerMetre", "s[Per]", new[] { "Kilogram", "Metre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfLength))]
[SharpMeasuresUnit(typeof(LinearDensity))]
public readonly partial record struct UnitOfLinearDensity { }
