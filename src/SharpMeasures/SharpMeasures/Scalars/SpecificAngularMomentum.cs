namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpecificAngularMomentum), Vector = typeof(SpecificAngularMomentumN), DefaultUnit = "SquareMetrePerSecond", DefaultSymbol = "m²∙s⁻¹")]
public readonly partial record struct SpecificAngularMomentum { }

[QuantityOperation(typeof(AngularMomentum), typeof(Mass), OperatorType.Multiplication)]
public readonly partial record struct SpecificAngularMomentum { }
