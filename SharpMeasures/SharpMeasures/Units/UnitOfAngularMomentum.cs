namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("KilogramSquareMetrePerSecond", "s[Per]", new[] { "KilogramSquareMetre", "RadianPerSecond" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfMomentOfInertia), typeof(UnitOfAngularSpeed), Permutations = true)]
[SharpMeasuresUnit(typeof(AngularMomentum))]
public readonly partial record struct UnitOfAngularMomentum { }
