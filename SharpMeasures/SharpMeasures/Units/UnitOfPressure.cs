namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("Pascal", "[*]s", new[] { "Newton", "SquareMetre" })]
[PrefixedUnitInstance("Kilopascal", "[*]s", "Pascal", MetricPrefixName.Kilo)]
[ScaledUnitInstance("Bars", "[*]", "Kilopascal", 100)]
[DerivedUnitInstance("PoundForcePerSquareInch", "PoundsForcePerSquareInch", new[] { "PoundForce", "SquareInch" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfForce), typeof(UnitOfArea))]
[SharpMeasuresUnit(typeof(Pressure))]
public readonly partial record struct UnitOfPressure { }
