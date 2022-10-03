namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAcceleration), DefaultUnitInstanceName = "MetrePerSecondSquared", DefaultUnitInstanceSymbol = "m∙s⁻²")]
public readonly partial record struct Acceleration { }

[ScalarConstant("StandardGravity", "MetrePerSecondSquared", 9.81)]
public readonly partial record struct Acceleration { }
