namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} * {1}", typeof(Length), typeof(Time), Permutations = true)]
[SharpMeasuresScalar(typeof(UnitOfAbsement), DefaultUnitInstanceName = "MetreSecond", DefaultUnitInstanceSymbol = "m∙s")]
public readonly partial record struct Absement { }
