namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("HertzPerSecond", "[*]", new[] { "Hertz", "Second" })]
[UnitInstanceAlias("PerSecondSquared", "[*]", "HertzPerSecond")]
[DerivableUnit("{0} / {1}", typeof(UnitOfFrequency), typeof(UnitOfTime))]
[Unit(typeof(FrequencyDrift))]
public readonly partial record struct UnitOfFrequencyDrift { }
