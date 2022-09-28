namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfForce), DefaultUnitInstanceName = "Newton", DefaultUnitInstanceSymbol = "N")]
public readonly partial record struct Force { }

[DerivedQuantity("{0} / {1}", typeof(Impulse), typeof(Time))]
[DerivedQuantity("{0} / {1}", typeof(Energy), typeof(Length))]
[DerivedQuantity("{0} * {1}", typeof(Mass), typeof(Acceleration), Permutations = true)]
public readonly partial record struct Force { }
