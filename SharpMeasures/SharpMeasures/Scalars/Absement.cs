namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfAbsement), DefaultUnitInstanceName = "MetreSecond", DefaultUnitInstanceSymbol = "m∙s")]
public readonly partial record struct Absement { }

[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(FrequencyDrift))]
[DerivedQuantity("{0} * {1}", typeof(Speed), typeof(TimeSquared), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Length), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(Length), typeof(Time), Permutations = true)]
public readonly partial record struct Absement { }
