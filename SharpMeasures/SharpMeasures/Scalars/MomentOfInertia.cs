namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfMomentOfInertia), DefaultUnitInstanceName = "KilogramSquareMetre", DefaultUnitInstanceSymbol = "kg∙m²")]
public readonly partial record struct MomentOfInertia { }

[DerivedQuantity("{0} / {1}", typeof(AngularMomentum), typeof(AngularSpeed)]
public readonly partial record struct MomentOfInertia { }
