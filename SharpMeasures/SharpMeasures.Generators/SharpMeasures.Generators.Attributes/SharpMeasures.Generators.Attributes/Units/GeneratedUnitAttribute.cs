namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Marks the type as a unit, and enables source generation.</summary>
/// <remarks>Accompanying attributes, such as <see cref="Units.DerivableUnitAttribute"/> and <see cref="Units.FixedUnitAttribute"/>, may be used to extend
/// the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedUnitAttribute : Attribute
{
    /// <summary>The scalar quantity that the unit describes.</summary>
    /// <remarks>For biased units, this should represent the associated unbiased quantity.
    /// <para>There may be multiple such quantities, in which case the most fundamental quantity is expected.</para></remarks>
    public Type Quantity { get; }
    /// <summary>Dictates whether this unit should include a bias term.</summary>
    public bool AllowBias { get; init; }

    /// <summary>Marks the type as a unit, and enables source generation.</summary>
    /// <param name="quantity">The scalar quantity that the unit describes.
    /// <para>For biased units, this represents the associated unbiased quantity.</para>
    /// <para>There may be multiple such quantities, in which case the most fundamental quantity is expected.</para></param>
    public GeneratedUnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
