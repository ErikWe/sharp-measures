namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramSquareMetrePerSecond", "s[Per]", new[] { "KilogramSquareMetre", "RadianPerSecond" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfMomentOfInertia), typeof(UnitOfAngularSpeed), Permutations = true)]
[Unit(typeof(AngularMomentum))]
public readonly partial record struct UnitOfAngularMomentum { }
