namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfImpulse), DefaultUnitInstanceName = "NewtonSecond", DefaultUnitInstanceSymbol = "N∙s")]
public readonly partial record struct Impulse { }

[DerivedQuantity("{0} * {1}", typeof(Force), typeof(Time), Permutations = true)]
public readonly partial record struct Impulse { }
