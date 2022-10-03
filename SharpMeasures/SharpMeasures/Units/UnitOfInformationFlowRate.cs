namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("BitPerSecond", "s[Per]", new[] { "Bit", "Second" })]
[DerivedUnitInstance("KilobitPerSecond", "s[Per]", new[] { "Kilobit", "Second" })]
[DerivedUnitInstance("MegabitPerSecond", "s[Per]", new[] { "Megabit", "Second" })]
[DerivedUnitInstance("GigabitPerSecond", "s[Per]", new[] { "Gigabit", "Second" })]
[DerivedUnitInstance("TerabitPerSecond", "s[Per]", new[] { "Terabit", "Second" })]
[DerivedUnitInstance("KibibitPerSecond", "s[Per]", new[] { "Kibibit", "Second" })]
[DerivedUnitInstance("MebibitPerSecond", "s[Per]", new[] { "Mebibit", "Second" })]
[DerivedUnitInstance("GibibitPerSecond", "s[Per]", new[] { "Gibibit", "Second" })]
[DerivedUnitInstance("TebibitPerSecond", "s[Per]", new[] { "Tebibit", "Second" })]
[DerivedUnitInstance("BytePerSecond", "s[Per]", new[] { "Byte", "Second" })]
[DerivedUnitInstance("KilobytePerSecond", "s[Per]", new[] { "Kilobyte", "Second" })]
[DerivedUnitInstance("MegabytePerSecond", "s[Per]", new[] { "Megabyte", "Second" })]
[DerivedUnitInstance("GigabytePerSecond", "s[Per]", new[] { "Gigabyte", "Second" })]
[DerivedUnitInstance("TerabytePerSecond", "s[Per]", new[] { "Terabyte", "Second" })]
[DerivedUnitInstance("KibibytePerSecond", "s[Per]", new[] { "Kibibyte", "Second" })]
[DerivedUnitInstance("MebibytePerSecond", "s[Per]", new[] { "Mebibyte", "Second" })]
[DerivedUnitInstance("GibibytePerSecond", "s[Per]", new[] { "Gibibyte", "Second" })]
[DerivedUnitInstance("TebibytePerSecond", "s[Per]", new[] { "Tebibyte", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfInformation), typeof(UnitOfTime))]
[Unit(typeof(InformationFlowRate))]
public readonly partial record struct UnitOfInformationFlowRate { }
