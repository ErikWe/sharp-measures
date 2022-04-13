namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Marks the scalar quantity as supporting the cube operation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class CubableQuantityAttribute : Attribute
{
    /// <summary>The scalar quantity that represents the cube of this quantity.</summary>
    public Type Quantity { get; }

    /// <summary>Additional scalar quantities that also represents the cube of this quantity.</summary>
    public IEnumerable<Type> SecondaryQuantities { get; }

    /// <summary>Marks the scalar quantity as supporting the cube operation.</summary>
    /// <param name="quantity">The scalar quantity that represents the cube of this quantity.</param>
    public CubableQuantityAttribute(Type quantity)
    {
        Quantity = quantity;
        SecondaryQuantities = Array.Empty<Type>();
    }

    /// <summary>Marks the scalar quantity as supporting the cube operation.</summary>
    /// <param name="quantity">The primary scalar quantity that represents the cube of this quantity.</param>
    /// <param name="secondaryQuantities">Additional scalar quantities that also represents the cube of this quantity.</param>
    public CubableQuantityAttribute(Type quantity, IEnumerable<Type> secondaryQuantities)
    {
        Quantity = quantity;
        SecondaryQuantities = secondaryQuantities;
    }
}
