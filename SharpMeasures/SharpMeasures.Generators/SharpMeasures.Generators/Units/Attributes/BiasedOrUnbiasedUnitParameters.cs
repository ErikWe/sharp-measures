namespace SharpMeasures.Generators.Units.Attributes;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal readonly record struct BiasedOrUnbiasedUnitParameters(GeneratedUnitAttributeParameters? UnbiasedParameters,
    GeneratedBiasedUnitAttributeParameters? BiasedParameters);