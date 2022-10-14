namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("SecondPerMetre", "s[Per]", new[] { "Second", "Metre" })]
[DerivedUnitInstance("MinutePerKilometre", "s[Per]", new[] { "Minute", "Kilometre" })]
[DerivedUnitInstance("MinutePerMile", "s[Per]", new[] { "Minute", "Mile" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfTime), typeof(UnitOfLength))]
[Unit(typeof(Pace))]
public readonly partial record struct UnitOfPace { }
