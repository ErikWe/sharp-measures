namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("NewtonSecond", "[*]s", new[] { "Newton", "Second" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfForce), typeof(UnitOfTime), Permutations = true)]
[Unit(typeof(Impulse))]
public readonly partial record struct UnitOfImpulse { }
