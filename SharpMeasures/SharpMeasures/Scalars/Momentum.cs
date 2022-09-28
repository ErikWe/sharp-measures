namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfMomentum), DefaultUnitInstanceName = "KilogramMetrePerSecond", DefaultUnitInstanceSymbol = "kg∙m∙s⁻¹")]
public readonly partial record struct Momentum { }

[DerivedQuantity("{0} * {1}", typeof(Mass), typeof(Speed), Permutations = true)]
public readonly partial record struct Momentum { }
