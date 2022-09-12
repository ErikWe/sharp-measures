namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("KilogramMetrePerSecond", "s[Per]", new[] { "Kilogram", "MetrePerSecond" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfMass), typeof(UnitOfSpeed), Permutations = true)]
[SharpMeasuresUnit(typeof(Momentum))]
public readonly partial record struct UnitOfMomentum { }
