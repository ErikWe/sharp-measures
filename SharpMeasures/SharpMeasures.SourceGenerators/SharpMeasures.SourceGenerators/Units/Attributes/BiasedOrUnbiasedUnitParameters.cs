namespace SharpMeasures.SourceGenerators.Units.Attributes;

using SharpMeasures.Attributes.Meta.Units;

internal readonly record struct BiasedOrUnbiasedUnitParameters(GeneratedUnitAttributeParameters? UnbiasedParameters,
    GeneratedBiasedUnitAttributeParameters? BiasedParameters);