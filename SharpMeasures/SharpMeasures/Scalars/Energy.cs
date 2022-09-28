namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} * {1}", typeof(Momentum), typeof(Speed))]
[DerivedQuantity("{0} * {1}", typeof(Force), typeof(Length), Permutations = true)]
[SharpMeasuresScalar(typeof(UnitOfEnergy), DefaultUnitInstanceName = "Joule", DefaultUnitInstanceSymbol = "J")]
public readonly partial record struct Energy { }
