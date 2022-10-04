namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("MinutePerMile", "s[Per]", new[] { "Minute", "Mile" })]
[DerivedUnitInstance("MinutePerKilometre", "s[Per]", new[] { "Minute", "Kilometre" })]
[DerivedUnitInstance("SecondPerMetre", "s[Per]", new[] { "Second", "Metre" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfTime), typeof(UnitOfLength))]
[Unit(typeof(Pace))]
public readonly partial record struct UnitOfPace { }
