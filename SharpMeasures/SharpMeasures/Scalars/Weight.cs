namespace SharpMeasures;

using SharpMeasures.Generators;

[QuantityOperation(typeof(GravitationalAcceleration), typeof(Mass), OperatorType.Division)]
[SpecializedScalarQuantity(typeof(Force), Vector = typeof(WeightN))]
public readonly partial record struct Weight { }
