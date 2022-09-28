namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfFrequencyDrift), DefaultUnitInstanceName = "HertzPerSecond", DefaultUnitInstanceSymbol = "Hz∙s⁻¹")]
public readonly partial record struct FrequencyDrift { }

[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Absement))]
[DerivedQuantity("{0} / {1}", typeof(AngularAcceleration), typeof(Angle))]
[DerivedQuantity("{0} * {1}", typeof(Acceleration), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Acceleration), typeof(Length))]
[DerivedQuantity("{0} * {1}", typeof(Frequency), typeof(Frequency))]
[DerivedQuantity("{0} / {1}", typeof(Frequency), typeof(Time))]
public readonly partial record struct FrequencyDrift { }

[ProcessedQuantity("SquareRoot", typeof(Frequency), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct FrequencyDrift { }
