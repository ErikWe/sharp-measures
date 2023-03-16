namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramSquareMetre", "[*]s", new[] { "Kilogram", "Metre" })]
[DerivableUnit("{0} * {1} * {1}", typeof(UnitOfMass), typeof(UnitOfLength))]
[Unit(typeof(MomentOfInertia))]
public readonly partial record struct UnitOfMomentOfInertia { }
