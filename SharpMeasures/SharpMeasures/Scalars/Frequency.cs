namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfFrequency), DefaultUnitInstanceName = "Hertz", DefaultUnitInstanceSymbol = "Hz")]
public readonly partial record struct Frequency { }

[DerivedQuantity("{0} / {1}", typeof(InformationFlowRate), typeof(Information))]
[DerivedQuantity("{0} / {1}", typeof(AngularAcceleration), typeof(AngularSpeed))]
[DerivedQuantity("{0} / {1}", typeof(AngularSpeed), typeof(Angle))]
[DerivedQuantity("{0} / {1}", typeof(Jerk), typeof(Acceleration))]
[DerivedQuantity("{0} / {1}", typeof(Acceleration), typeof(Speed))]
[DerivedQuantity("{0} * {1}", typeof(Speed), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Length))]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(Absement))]
[DerivedQuantity("{0} / {1}", typeof(FrequencyDrift), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(FrequencyDrift), typeof(Time), Permutations = true)]
[DerivedQuantity("1 / {0}", typeof(Time))]
public readonly partial record struct Frequency { }
