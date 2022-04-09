namespace SharpMeasures.SourceGeneration;

using System;

/// <summary>Marks the type as a biased unit, and enables source generation.</summary>
/// <remarks>Accompanying attributes, such as <see cref="DerivedUnitAttribute"/> and <see cref="FixedUnitInstanceAttribute"/>, may be used to extend
/// the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedBiasedUnitAttribute : Attribute
{
    /// <summary>The biased scalar quantity that the unit describes.</summary>
    public Type BiasedQuantity { get; }
    /// <summary>The unbiased scalar quantity that the unit describes. If no such quantity exists, this value should be <see langword="null"/>.</summary>
    public Type? UnbiasedQuantity { get; }

    /// <summary>Marks the type as a biased unit, and enables source generation.</summary>
    /// <param name="biasedQuantity">The biased scalar quantity that the unit describes.</param>
    /// <param name="unbiasedQuantity">The unbiased scalar quantity that the unit describes, or <see langword="null"/> if no such quantity exists.</param>
    public GeneratedBiasedUnitAttribute(Type biasedQuantity, Type? unbiasedQuantity = null)
    {
        BiasedQuantity = biasedQuantity;
        UnbiasedQuantity = unbiasedQuantity;
    }
}
