namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("MinutePerMile", "s[Per]", new[] { "Minute", "Mile" })]
[DerivedUnitInstance("MinutePerKilometre", "s[Per]", new[] { "Minute", "Kilometre" })]
[DerivedUnitInstance("SecondPerMetre", "s[Per]", new[] { "Second", "Metre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfTime), typeof(UnitOfLength))]
[SharpMeasuresUnit(typeof(Pace))]
public readonly partial record struct UnitOfPace { }
