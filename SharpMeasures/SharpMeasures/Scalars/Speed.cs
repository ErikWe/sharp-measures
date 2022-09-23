namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} * {1}", typeof(Acceleration), typeof(Time), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(Time))]
[SharpMeasuresScalar(typeof(UnitOfSpeed), DefaultUnitInstanceName = "MetrePerSecond", DefaultUnitInstanceSymbol = "m∙s⁻¹")]
public readonly partial record struct Speed { }
