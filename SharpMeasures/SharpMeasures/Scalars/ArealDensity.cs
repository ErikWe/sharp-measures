namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfArealDensity), DefaultUnitInstanceName = "KilogramPerSquareMetre", DefaultUnitInstanceSymbol = "kg∙m⁻²")]
public readonly partial record struct ArealDensity { }

[DerivedQuantity("{0} * {1}", typeof(LinearDensity), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(LinearDensity), typeof(Length))]
[DerivedQuantity("{0} / {1}", typeof(Density), typeof(SpatialFrequency))]
[DerivedQuantity("{0} * {1}", typeof(Density), typeof(Length), Permutations = true)]
[DerivedQuantity("{0} * {1}", typeof(Mass), typeof(ArealFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Mass), typeof(Area))]
public readonly partial record struct ArealDensity { }
