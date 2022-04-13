namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Marks the scalar quantity as supporting the cube root operation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class CubeRootableQuantityAttribute : Attribute
{
    /// <summary>The scalar quantity that represents the cube root of this quantity.</summary>
    public Type Quantity { get; }

    /// <summary>Additional scalar quantities that also represents the cube root of this quantity.</summary>
    public IEnumerable<Type> SecondaryQuantities { get; }

    /// <summary>Marks the scalar quantity as supporting the cube root operation.</summary>
    /// <param name="quantity">The scalar quantity that represents the cube root of this quantity.</param>
    public CubeRootableQuantityAttribute(Type quantity)
    {
        Quantity = quantity;
        SecondaryQuantities = Array.Empty<Type>();
    }

    /// <summary>Marks the scalar quantity as supporting the cube root operation.</summary>
    /// <param name="quantity">The primary scalar quantity that represents the cube root of this quantity.</param>
    /// <param name="secondaryQuantities">Additional scalar quantities that also represents the cube root of this quantity.</param>
    public CubeRootableQuantityAttribute(Type quantity, IEnumerable<Type> secondaryQuantities)
    {
        Quantity = quantity;
        SecondaryQuantities = secondaryQuantities;
    }
}
