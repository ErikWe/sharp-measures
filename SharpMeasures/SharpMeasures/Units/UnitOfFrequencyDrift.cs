namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("HertzPerSecond", "[*]", new[] { "Hertz", "Second" })]
[UnitInstanceAlias("PerSecondSquared", "[*]", "HertzPerSecond")]
[DerivableUnit("{0} / {1}", typeof(UnitOfFrequency), typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(FrequencyDrift))]
public readonly partial record struct UnitOfFrequencyDrift { }
