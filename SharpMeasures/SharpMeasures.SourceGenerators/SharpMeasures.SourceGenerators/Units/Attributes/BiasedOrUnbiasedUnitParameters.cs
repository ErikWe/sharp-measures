namespace SharpMeasures.SourceGenerators.Units.Attributes;

using SharpMeasures.Attributes.Parsing.Units;

internal readonly record struct BiasedOrUnbiasedUnitParameters(GeneratedUnitAttributeParameters? UnbiasedParameters,
    GeneratedBiasedUnitAttributeParameters? BiasedParameters);