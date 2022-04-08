namespace SharpMeasures.Attributes;

using System;

/// <summary>Allows additional functionality to be added to a unit through source generation.</summary>
/// <remarks>Accompanying attributes, such as <see cref="DerivedUnitAttribute"/> and <see cref="FixedUnitInstanceAttribute"/>, may be used to
/// improve the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class UnitAttribute : Attribute
{
    /// <summary>The scalar quantity that the unit describes, which should be marked with <see cref="ScalarQuantityAttribute"/>.</summary>
    public Type Quantity { get; }

    /// <summary>Adds additional functionality to a unit through source generation.</summary>
    /// <param name="quantity">The scalar quantity that the unit describes, which should be marked with <see cref="ScalarQuantityAttribute"/>.</param>
    public UnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
