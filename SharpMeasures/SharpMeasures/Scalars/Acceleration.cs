namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfAcceleration), DefaultUnitInstanceName = "MetrePerSecondSquared", DefaultUnitInstanceSymbol = "m∙s⁻²")]
public readonly partial record struct Acceleration { }

[DerivedQuantity("{0} / {1}", typeof(Force), typeof(Mass))]
[DerivedQuantity("{0} / {1}", typeof(FrequencyDrift), typeof(SpatialFrequency))]
[DerivedQuantity("{0} / {1}", typeof(Jerk), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(Jerk), typeof(Time), Permutations = true)]
[DerivedQuantity("{0} * {1}", typeof(Length), typeof(FrequencyDrift), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(TimeSquared))]
[DerivedQuantity("{0} * {1}", typeof(Speed), typeof(Frequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Time))]
public readonly partial record struct Acceleration { }

[ScalarConstant("StandardGravity", "MetrePerSecondSquared", 9.81)]
public readonly partial record struct Acceleration { }
