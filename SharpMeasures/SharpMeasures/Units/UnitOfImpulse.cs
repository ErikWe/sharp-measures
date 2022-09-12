namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("NewtonSecond", "[*]s", new[] { "Newton", "Second" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfForce), typeof(UnitOfTime), Permutations = true)]
[SharpMeasuresUnit(typeof(Impulse))]
public readonly partial record struct UnitOfImpulse { }
