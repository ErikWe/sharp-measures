namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("SquareSecond", "[*]s", new[] { "Second" })]
[DerivableUnit("{0} * {0}", typeof(UnitOfTime))]
[Unit(typeof(TimeSquared))]
public readonly partial record struct UnitOfTimeSquared { }
