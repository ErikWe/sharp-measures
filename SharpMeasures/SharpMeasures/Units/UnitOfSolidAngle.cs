namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("SquareRadian", "[*]s", new[] { "Radian" })]
[UnitInstanceAlias("Steradian", "[*]s", "SquareRadian")]
[DerivedUnitInstance("SquareDegree", "[*]s", new[] { "Degree" })]
[DerivedUnitInstance("SquareArcminute", "[*]s", new[] { "Arcminute" })]
[DerivedUnitInstance("SquareArcsecond", "[*]s", new[] { "Arcsecond" })]
[DerivedUnitInstance("SquareMilliarcsecond", "[*]s", new[] { "Milliarcsecond" })]
[DerivedUnitInstance("SquareMicroarcsecond", "[*]s", new[] { "Microarcsecond" })]
[DerivableUnit("{0} * {0}", typeof(UnitOfAngle))]
[SharpMeasuresUnit(typeof(SolidAngle))]
public readonly partial record struct UnitOfSolidAngle { }
