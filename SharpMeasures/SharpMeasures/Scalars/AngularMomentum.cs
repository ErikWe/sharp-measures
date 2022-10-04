namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAngularMomentum), DefaultUnitInstanceName = "KilogramSquareMetrePerSecond", DefaultUnitInstanceSymbol = "kg∙m²∙s⁻¹")]
public readonly partial record struct AngularMomentum { }

[QuantityOperation(typeof(SpecificAngularMomentum), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(MomentOfInertia), typeof(AngularSpeed), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(SpecificAngularMomentum), OperatorType.Division)]
[QuantityOperation(typeof(AngularSpeed), typeof(MomentOfInertia), OperatorType.Division)]
public readonly partial record struct AngularMomentum { }
