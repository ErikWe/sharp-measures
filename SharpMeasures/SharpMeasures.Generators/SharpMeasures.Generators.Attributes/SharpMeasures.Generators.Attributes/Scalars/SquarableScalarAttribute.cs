namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the scalar quantity as supporting the square operation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class SquarableScalarAttribute : Attribute
{
    /// <summary>The scalar quantity that represents the square of this quantity.</summary>
    public Type Quantity { get; }

    /// <summary>Additional scalar quantities that also represents the square of this quantity.</summary>
    public Type[] SecondaryQuantities { get; }

    /// <summary>Marks the scalar quantity as supporting the square operation.</summary>
    /// <param name="quantity">The primary scalar quantity that represents the square of this quantity.</param>
    /// <param name="secondaryQuantities">Additional scalar quantities that also represents the square of this quantity.</param>
    public SquarableScalarAttribute(Type quantity, params Type[] secondaryQuantities)
    {
        Quantity = quantity;
        SecondaryQuantities = secondaryQuantities;
    }
}
