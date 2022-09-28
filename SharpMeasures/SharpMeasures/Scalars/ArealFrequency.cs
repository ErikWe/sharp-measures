namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfArealFrequency), DefaultUnitInstanceName = "PerSquareMetre", DefaultUnitInstanceSymbol = "m⁻²")]
public readonly partial record struct ArealFrequency { }

[DerivedQuantity("{0} * {1}", typeof(Length), typeof(VolumetricFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(Volume))]
[DerivedQuantity("{0} * {1}", typeof(SpatialFrequency), typeof(SpatialFrequency))]
[DerivedQuantity("{0} / {1}", typeof(SpatialFrequency), typeof(Length))]
[DerivedQuantity("{0} / {1}", typeof(Density), typeof(LinearDensity))]
[DerivedQuantity("{0} / {1}", typeof(ArealDensity), typeof(Mass))]
[DerivedQuantity("1 / {0}", typeof(Area))]
public readonly partial record struct ArealFrequency { }

[ProcessedQuantity("SquareRoot", typeof(SpatialFrequency), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct ArealFrequency { }
