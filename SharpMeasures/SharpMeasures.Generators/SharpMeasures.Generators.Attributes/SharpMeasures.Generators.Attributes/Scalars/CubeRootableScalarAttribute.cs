namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the scalar quantity as supporting the cube root operation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class CubeRootableScalarAttribute : Attribute
{
    /// <summary>The scalar quantity that represents the cube root of this quantity.</summary>
    public Type Quantity { get; }

    /// <summary>Additional scalar quantities that also represents the cube root of this quantity.</summary>
    public Type[] SecondaryQuantities { get; }

    /// <summary>Marks the scalar quantity as supporting the cube root operation.</summary>
    /// <param name="quantity">The primary scalar quantity that represents the cube root of this quantity.</param>
    /// <param name="secondaryQuantities">Additional scalar quantities that also represents the cube root of this quantity.</param>
    public CubeRootableScalarAttribute(Type quantity, params Type[] secondaryQuantities)
    {
        Quantity = quantity;
        SecondaryQuantities = secondaryQuantities;
    }
}
