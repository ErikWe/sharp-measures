namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(Acceleration), Vector = typeof(GravitationalAccelerationN))]
public readonly partial record struct GravitationalAcceleration { }
