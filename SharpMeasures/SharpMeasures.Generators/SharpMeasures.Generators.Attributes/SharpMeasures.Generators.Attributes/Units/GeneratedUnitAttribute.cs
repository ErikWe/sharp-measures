namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Marks the type as a unit, and enables source generation.</summary>
/// <remarks>Accompanying attributes, such as <see cref="Units.DerivableUnitAttribute"/> and <see cref="Units.FixedUnitAttribute"/>, may be used to extend
/// the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedUnitAttribute : Attribute
{
    /// <summary>The scalar quantity that the unit describes.</summary>
    /// <remarks>For biased units, this represents the associated unbiased quantity.</remarks>
    public Type Quantity { get; }
    /// <summary>Dictates whether this unit should include a bias term.</summary>
    public bool Biased { get; init; }

    /// <summary>Marks the type as a unit, and enables source generation.</summary>
    /// <param name="quantity">The scalar quantity that the unit describes
    /// <para>For biased units, this represents the associated unbiased quantity.</para></param>
    public GeneratedUnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
