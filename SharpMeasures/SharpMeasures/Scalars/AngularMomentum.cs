namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfAngularMomentum), DefaultUnitInstanceName = "KilogramSquareMetrePerSecond", DefaultUnitInstanceSymbol = "kg∙m²∙s⁻¹")]
public readonly partial record struct AngularMomentum { }

[DerivedQuantity("{0} * {1}", typeof(MomentOfInertia), typeof(AngularSpeed), Permutations = true)]
public readonly partial record struct AngularMomentum { }
