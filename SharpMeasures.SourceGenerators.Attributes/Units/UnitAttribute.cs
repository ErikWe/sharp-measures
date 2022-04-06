namespace ErikWe.SharpMeasures.Attributes;

using System;

/// <summary>Allows additional functionality to be added to a unit through source generation.</summary>
/// <remarks>Accompanying attributes, such as <see cref="DerivedUnitAttribute"/> and <see cref="FixedUnitInstanceAttribute"/>, may be used to
/// improve the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class UnitAttribute : Attribute
{
    /// <summary>The quantity that the unit describes.</summary>
    public Type Quantity { get; }

    /// <summary>Adds additional functionality to a unit through source generation.</summary>
    /// <param name="quantity">The quantity that the unit describes.</param>
    public UnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
