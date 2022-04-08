namespace ErikWe.SharpMeasures.Attributes;

using System;

/// <summary>Allows additional functionality to be added to a biased unit through source generation.</summary>
/// <remarks>This attribute may not be used together with <see cref="UnitAttribute"/>.
/// <para>Accompanying attributes, such as <see cref="DerivedUnitAttribute"/> and <see cref="FixedUnitInstanceAttribute"/>, may be used to extend
/// source generation further.</para></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class BiasedUnitAttribute : Attribute
{
    /// <summary>The biased scalar quantity that the unit describes, which should be marked with <see cref="ScalarQuantityAttribute"/>.</summary>
    public Type BiasedQuantity { get; }
    /// <summary>The unbiased scalar quantity that the unit describes, which should be marked with <see cref="ScalarQuantityAttribute"/>. May be
    /// <see langword="null"/> if no such quantity exists.</summary>
    public Type? UnbiasedQuantity { get; }

    /// <summary>Allows a biased unit to be enhanced through source generation.</summary>
    /// <param name="biasedQuantity">The biased quantity that the unit describes.</param>
    public BiasedUnitAttribute(Type biasedQuantity)
    {
        BiasedQuantity = biasedQuantity;
    }

    /// <summary>Adds additional functionality to a biased unit through source generation.</summary>
    /// <param name="biasedQuantity">The biased scalar quantity that the unit describes, which should be marked with <see cref="ScalarQuantityAttribute"/>.</param>
    /// <param name="unbiasedQuantity">The unbiased scalar quantity that the unit describes, which should be marked with <see cref="ScalarQuantityAttribute"/>.</param>
    public BiasedUnitAttribute(Type biasedQuantity, Type unbiasedQuantity)
    {
        BiasedQuantity = biasedQuantity;
        UnbiasedQuantity = unbiasedQuantity;
    }
}
