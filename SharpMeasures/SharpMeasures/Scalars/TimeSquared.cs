namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} / {1}", typeof(Distance), typeof(Acceleration))]
[SharpMeasuresScalar(typeof(UnitOfTimeSquared), DefaultUnitInstanceName = "SquareSecond", DefaultUnitInstanceSymbol = "s²")]
public readonly partial record struct TimeSquared { }
