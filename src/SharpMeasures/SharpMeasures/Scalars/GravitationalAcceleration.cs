namespace SharpMeasures;

using SharpMeasures.Generators;

[QuantityOperation(typeof(Weight), typeof(Mass), OperatorType.Multiplication)]
[SpecializedScalarQuantity(typeof(Acceleration), Vector = typeof(GravitationalAccelerationN))]
public readonly partial record struct GravitationalAcceleration { }
