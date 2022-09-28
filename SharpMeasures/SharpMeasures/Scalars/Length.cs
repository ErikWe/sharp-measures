namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
public readonly partial record struct Length { }

[DerivedQuantity("{0} / {1}", typeof(Energy), typeof(Force))]
[DerivedQuantity("{0} / {1}", typeof(ArealFrequency), typeof(VolumetricFrequency))]
[DerivedQuantity("{0} * {1}", typeof(ArealFrequency), typeof(Volume), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(SpatialFrequency), typeof(ArealFrequency))]
[DerivedQuantity("{0} / {1}", typeof(ArealDensity), typeof(Density))]
[DerivedQuantity("{0} / {1}", typeof(LinearDensity), typeof(ArealDensity))]
[DerivedQuantity("{0} * {1}", typeof(Absement), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Absement), typeof(Length))]
[DerivedQuantity("{0} / {1}", typeof(Acceleration), typeof(FrequencyDrift))]
[DerivedQuantity("{0} * {1}", typeof(Acceleration), typeof(TimeSquared), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(Speed), typeof(Time), Permutations = true)]
[DerivedQuantity("{0} * {1}", typeof(Volume), typeof(ArealFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Volume), typeof(Area))]
[DerivedQuantity("{0} * {1}", typeof(Area), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Area), typeof(Length))]
public readonly partial record struct Length { }
