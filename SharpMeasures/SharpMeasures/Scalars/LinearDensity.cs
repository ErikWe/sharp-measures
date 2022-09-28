namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfLinearDensity), DefaultUnitInstanceName = "KilogramPerMetre", DefaultUnitInstanceSymbol = "kg∙m⁻¹")]
public readonly partial record struct LinearDensity { }

[DerivedQuantity("{0} / {1}", typeof(Density), typeof(ArealFrequency))]
[DerivedQuantity("{0} * {1}", typeof(Density), typeof(Area), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(ArealDensity), typeof(SpatialFrequency))]
[DerivedQuantity("{0} * {1}", typeof(ArealDensity), typeof(Length), Permutations = true)]
[DerivedQuantity("{0} * {1}", typeof(Mass), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Mass), typeof(Length))]
public readonly partial record struct LinearDensity { }
