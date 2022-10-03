namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Bit", "[*]s")]
[PrefixedUnitInstance("Kilobit", "[*]s", "Bit", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megabit", "[*]s", "Bit", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigabit", "[*]s", "Bit", MetricPrefixName.Giga)]
[PrefixedUnitInstance("Terabit", "[*]s", "Bit", MetricPrefixName.Tera)]
[PrefixedUnitInstance("Kibibit", "[*]s", "Bit", BinaryPrefixName.Kibi)]
[PrefixedUnitInstance("Mebibit", "[*]s", "Bit", BinaryPrefixName.Mebi)]
[PrefixedUnitInstance("Gibibit", "[*]s", "Bit", BinaryPrefixName.Gibi)]
[PrefixedUnitInstance("Tebibit", "[*]s", "Bit", BinaryPrefixName.Tebi)]
[ScaledUnitInstance("Byte", "[*]s", "Bit", 8)]
[PrefixedUnitInstance("Kilobyte", "[*]s", "Byte", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megabyte", "[*]s", "Byte", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigabyte", "[*]s", "Byte", MetricPrefixName.Giga)]
[PrefixedUnitInstance("Terabyte", "[*]s", "Byte", MetricPrefixName.Tera)]
[PrefixedUnitInstance("Kibibyte", "[*]s", "Byte", BinaryPrefixName.Kibi)]
[PrefixedUnitInstance("Mebibyte", "[*]s", "Byte", BinaryPrefixName.Mebi)]
[PrefixedUnitInstance("Gibibyte", "[*]s", "Byte", BinaryPrefixName.Gibi)]
[PrefixedUnitInstance("Tebibyte", "[*]s", "Byte", BinaryPrefixName.Tebi)]
[Unit(typeof(Information))]
public readonly partial record struct UnitOfInformation { }
