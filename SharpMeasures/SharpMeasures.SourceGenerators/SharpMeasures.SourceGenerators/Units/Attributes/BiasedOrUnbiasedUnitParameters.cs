namespace SharpMeasures.SourceGeneration.Units.Attributes;

using SharpMeasures.SourceGeneration.Attributes.Parsing.Units;

internal readonly record struct BiasedOrUnbiasedUnitParameters(GeneratedUnitAttributeParameters? UnbiasedParameters,
    GeneratedBiasedUnitAttributeParameters? BiasedParameters);