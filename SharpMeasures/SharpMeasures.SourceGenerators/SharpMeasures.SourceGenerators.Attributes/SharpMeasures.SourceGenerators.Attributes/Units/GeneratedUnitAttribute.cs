namespace SharpMeasures;

using System;

/// <summary>Marks the type as a unit, and enables source generation.</summary>
/// <remarks>Accompanying attributes, such as <see cref="DerivedUnitAttribute"/> and <see cref="FixedUnitInstanceAttribute"/>, may be used to extend
/// the source generation.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedUnitAttribute : Attribute
{
    /// <summary>The scalar quantity that the unit describes.</summary>
    public Type Quantity { get; }

    /// <summary>Marks the type as a unit, and enables source generation.</summary>
    /// <param name="quantity">The scalar quantity that the unit describes.</param>
    public GeneratedUnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
