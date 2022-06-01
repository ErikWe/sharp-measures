namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the scalar quantity as supporting the cube operation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class CubableScalarAttribute : Attribute
{
    /// <summary>The scalar quantity that represents the cube of this quantity.</summary>
    public Type Quantity { get; }

    /// <summary>Additional scalar quantities that also represents the cube of this quantity.</summary>
    public Type[] SecondaryQuantities { get; }

    /// <summary>Marks the scalar quantity as supporting the cube operation.</summary>
    /// <param name="quantity">The primary scalar quantity that represents the cube of this quantity.</param>
    /// <param name="secondaryQuantities">Additional scalar quantities that also represents the cube of this quantity.</param>
    public CubableScalarAttribute(Type quantity, params Type[] secondaryQuantities)
    {
        Quantity = quantity;
        SecondaryQuantities = secondaryQuantities;
    }
}
