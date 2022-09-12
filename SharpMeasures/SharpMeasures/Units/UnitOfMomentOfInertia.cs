namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("KilogramSquareMetre", "[*]s", new[] { "Kilogram", "Metre" })]
[DerivableUnit("{0} * {1} * {1}", typeof(UnitOfMass), typeof(UnitOfLength))]
[SharpMeasuresUnit(typeof(MomentOfInertia))]
public readonly partial record struct UnitOfMomentOfInertia { }
