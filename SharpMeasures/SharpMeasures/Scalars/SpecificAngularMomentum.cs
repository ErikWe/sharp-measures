namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpecificAngularMomentum), DefaultUnitInstanceName = "SquareMetrePerSecond", DefaultUnitInstanceSymbol = "m²∙s⁻¹")]
public readonly partial record struct SpecificAngularMomentum { }

[QuantityOperation(typeof(AngularMomentum), typeof(Mass), OperatorType.Multiplication)]
public readonly partial record struct SpecificAngularMomentum { }
