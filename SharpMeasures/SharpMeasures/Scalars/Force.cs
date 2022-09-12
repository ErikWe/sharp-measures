namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfForce), DefaultUnitInstanceName = "Newton", DefaultUnitInstanceSymbol = "N")]
public readonly partial record struct Force { }
