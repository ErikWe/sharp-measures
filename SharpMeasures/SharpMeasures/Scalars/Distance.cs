namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} * {1}", typeof(Acceleration), typeof(TimeSquared), Permutations = true)]
[SpecializedSharpMeasuresScalar(typeof(Length))]
public readonly partial record struct Distance { }
