namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMomentOfInertia), DefaultUnitInstanceName = "KilogramSquareMetre", DefaultUnitInstanceSymbol = "kg∙m²")]
public readonly partial record struct MomentOfInertia { }

[QuantityOperation(typeof(AngularMomentum), typeof(AngularSpeed), OperatorType.Multiplication)]
public readonly partial record struct MomentOfInertia { }
