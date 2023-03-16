namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("NewtonMetre", "[*]s", new[] { "Newton", "Metre" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfForce), typeof(UnitOfLength), Permutations = true)]
[Unit(typeof(Torque))]
public readonly partial record struct UnitOfTorque { }
