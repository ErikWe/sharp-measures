namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[DerivedQuantity("{0} / {1}", typeof(Absement), typeof(Time))]
[DerivedQuantity("{0} * {1}", typeof(Speed), typeof(Time), Permutations = true)]
[SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
public readonly partial record struct Length { }
