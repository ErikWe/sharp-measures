namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} / {1}", typeof(Distance), typeof(Speed))]
[SharpMeasuresScalar(typeof(UnitOfTime), DefaultUnitInstanceName = "Second", DefaultUnitInstanceSymbol = "s")]
public readonly partial record struct Time { }
