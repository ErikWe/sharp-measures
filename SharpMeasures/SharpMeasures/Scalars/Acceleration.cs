namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} * {1}", typeof(Jerk), typeof(Time), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Time))]
[SharpMeasuresScalar(typeof(UnitOfAcceleration), DefaultUnitInstanceName = "MetrePerSecondSquared", DefaultUnitInstanceSymbol = "m∙s⁻²")]
public readonly partial record struct Acceleration { }
