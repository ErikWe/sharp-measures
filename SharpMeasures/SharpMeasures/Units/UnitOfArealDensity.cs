namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("KilogramPerSquareMetre", "s[Per]", new[] { "Kilogram", "SquareMetre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfArea))]
[SharpMeasuresUnit(typeof(ArealDensity))]
public readonly partial record struct UnitOfArealDensity { }
