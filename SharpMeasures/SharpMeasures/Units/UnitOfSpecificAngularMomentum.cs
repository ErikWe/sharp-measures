namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("SquareMetrePerSecond", "s[Per]", new[] { "KilogramSquareMetrePerSecond", "Kilogram" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAngularMomentum), typeof(UnitOfMass))]
[SharpMeasuresUnit(typeof(SpecificAngularMomentum))]
public readonly partial record struct UnitOfSpecificAngularMomentum { }
