namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfArea), DefaultUnitInstanceName = "SquareMetre", DefaultUnitInstanceSymbol = "m²")]
public readonly partial record struct Area { }

[DerivedQuantity("{0} * {1}", typeof(SpatialFrequency), typeof(Volume), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Mass), typeof(ArealDensity))]
[DerivedQuantity("1 / {0}", typeof(ArealFrequency))]
[DerivedQuantity("{0} * {1}", typeof(Volume), typeof(SpatialFrequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Volume), typeof(Length))]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(SpatialFrequency))]
[DerivedQuantity("{0} * {1}", typeof(Length), typeof(Length))]
public readonly partial record struct Area { }

[ProcessedQuantity("SquareRoot", typeof(Length), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct Area { }
