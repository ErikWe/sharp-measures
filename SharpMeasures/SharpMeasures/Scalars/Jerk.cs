namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfJerk), DefaultUnitInstanceName = "MetrePerSecondCubed", DefaultUnitInstanceSymbol = "m∙s⁻³")]
public readonly partial record struct Jerk { }

[DerivedQuantity("{0} * {1}", typeof(Acceleration), typeof(Frequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Acceleration), typeof(Time))]
public readonly partial record struct Jerk { }
