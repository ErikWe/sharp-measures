namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfMass), DefaultUnitInstanceName = "Kilogram", DefaultUnitInstanceSymbol = "kg")]
public readonly partial record struct Mass { }

[DerivedQuantity("{0} / {1}", typeof(Momentum), typeof(Speed))]
[DerivedQuantity("{0} / {1}", typeof(Force), typeof(Acceleration))]
[DerivedQuantity("{0} / {1}", typeof(Density), typeof(VolumetricFrequency))]
[DerivedQuantity("{0} * {1}", typeof(Density), typeof(Volume), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(ArealDensity), typeof(ArealFrequency))]
[DerivedQuantity("{0} * {1}", typeof(ArealDensity), typeof(Area), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(LinearDensity), typeof(SpatialFrequency))]
[DerivedQuantity("{0} * {1}", typeof(LinearDensity), typeof(Length), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(MassFlowRate), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(MassFlowRate), typeof(Time), Permutations = true)]
public readonly partial record struct Mass { }
