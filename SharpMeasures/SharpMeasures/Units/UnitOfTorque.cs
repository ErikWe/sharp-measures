namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("NewtonMetre", "[*]s", new[] { "Newton", "Metre" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfForce), typeof(UnitOfLength), Permutations = true)]
[SharpMeasuresUnit(typeof(Torque))]
public readonly partial record struct UnitOfTorque { }
