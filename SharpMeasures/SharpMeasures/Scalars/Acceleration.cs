namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[ScalarConstant("StandardGravity", "MetrePerSecondSquared", 9.81)]
[DerivedQuantity("{0} / {1}", typeof(Distance), typeof(TimeSquared))]
[DerivedQuantity("{0} / {1}", typeof(Speed), typeof(Time))]
[SharpMeasuresScalar(typeof(UnitOfAcceleration), DefaultUnitInstanceName = "MetrePerSecondSquared", DefaultUnitInstanceSymbol = "m∙s⁻²")]
public readonly partial record struct Acceleration { }
