namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} / {1}", typeof(Absement), typeof(Length))]
[DerivedQuantity("{0} / {1}", typeof(Acceleration), typeof(Jerk))]
[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Acceleration))]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(Speed))]
[SharpMeasuresScalar(typeof(UnitOfTime), DefaultUnitInstanceName = "Second", DefaultUnitInstanceSymbol = "s")]
public readonly partial record struct Time { }
