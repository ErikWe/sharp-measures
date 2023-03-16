namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramPerSquareMetre", "s[Per]", new[] { "Kilogram", "SquareMetre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfArea))]
[Unit(typeof(ArealDensity))]
public readonly partial record struct UnitOfArealDensity { }
