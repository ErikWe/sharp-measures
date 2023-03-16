namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("SquareMetrePerSecondSquared", "s[Per]", new[] { "MetrePerSecond" })]
[DerivableUnit("{0} * {0}", typeof(UnitOfSpeed))]
[Unit(typeof(SpeedSquared))]
public readonly partial record struct UnitOfSpeedSquared { }
