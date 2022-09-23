namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} / {1}", typeof(Acceleration), typeof(Time))]
[SharpMeasuresScalar(typeof(UnitOfJerk), DefaultUnitInstanceName = "MetrePerSecondCubed", DefaultUnitInstanceSymbol = "m∙s⁻³")]
public readonly partial record struct Jerk { }
