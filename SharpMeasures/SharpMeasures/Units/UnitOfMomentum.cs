namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramMetrePerSecond", "s[Per]", new[] { "Kilogram", "MetrePerSecond" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfMass), typeof(UnitOfSpeed), Permutations = true)]
[Unit(typeof(Momentum))]
public readonly partial record struct UnitOfMomentum { }
