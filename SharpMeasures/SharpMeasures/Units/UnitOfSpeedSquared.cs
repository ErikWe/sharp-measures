namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("SquareMetrePerSecondSquared", "s[Per]", new[] { "MetrePerSecond" })]
[DerivableUnit("{0} * {0}", typeof(UnitOfSpeed))]
[SharpMeasuresUnit(typeof(SpeedSquared))]
public readonly partial record struct UnitOfSpeedSquared { }
