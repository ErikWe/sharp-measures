namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("SquareSecond", "[*]s", new[] { "Second" })]
[DerivableUnit("{0} * {0}", typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(TimeSquared))]
public readonly partial record struct UnitOfTimeSquared { }
