namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramMetrePerSecond", "s[Per]", "Momentum", new[] { "Kilogram", "MetrePerSecond" })]
[DerivedUnitInstance("NewtonSecond", "[*]s", "Impulse", new[] { "Newton", "Second" })]
[DerivableUnit("Momentum", "{0} * {1}", typeof(UnitOfMass), typeof(UnitOfSpeed), Permutations = true)]
[DerivableUnit("Impulse", "{0} * {1}", typeof(UnitOfForce), typeof(UnitOfTime), Permutations = true)]
[Unit(typeof(Momentum))]
public readonly partial record struct UnitOfMomentum { }
