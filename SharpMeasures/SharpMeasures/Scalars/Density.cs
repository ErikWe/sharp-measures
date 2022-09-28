namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfDensity), DefaultUnitInstanceName = "KilogramPerCubicMetre", DefaultUnitInstanceSymbol = "kg∙m⁻³")]
public readonly partial record struct Density { }

[DerivedQuantity("{0} * {1}", typeof(LinearDensity), typeof(ArealFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(LinearDensity), typeof(Area))]
[DerivedQuantity("{0} * {1}", typeof(ArealDensity), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(ArealDensity), typeof(Length))]
[DerivedQuantity("{0} * {1}", typeof(Mass), typeof(VolumetricFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Mass), typeof(Volume))]
public readonly partial record struct Density { }
