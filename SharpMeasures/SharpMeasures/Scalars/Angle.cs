namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfAngle), DefaultUnitInstanceName = "Radian", DefaultUnitInstanceSymbol = "rad")]
public readonly partial record struct Angle { }

[DerivedQuantity("{0} / {1}", typeof(AngularAcceleration), typeof(FrequencyDrift))]
[DerivedQuantity("{0} * {1}", typeof(AngularAcceleration), typeof(TimeSquared), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(AngularSpeed), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(AngularSpeed), typeof(Time), Permutations = true)]
public readonly partial record struct Angle { }
