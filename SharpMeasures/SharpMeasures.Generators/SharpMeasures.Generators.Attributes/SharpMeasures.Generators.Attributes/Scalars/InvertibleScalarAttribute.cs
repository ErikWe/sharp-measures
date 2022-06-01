namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the scalar quantity as supporting the invert operation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class InvertibleScalarAttribute : Attribute
{
    /// <summary>The scalar quantity that represents the inverse of this quantity.</summary>
    public Type Quantity { get; }

    /// <summary>Additional scalar quantities that also represents the inverse of this quantity.</summary>
    public Type[] SecondaryQuantities { get; }

    /// <summary>Marks the scalar quantity as supporting the invert operation.</summary>
    /// <param name="quantity">The primary scalar quantity that represents the inverse of this quantity.</param>
    /// <param name="secondaryQuantities">Additional scalar quantities that also represents the inverse of this quantity.</param>
    public InvertibleScalarAttribute(Type quantity, params Type[] secondaryQuantities)
    {
        Quantity = quantity;
        SecondaryQuantities = secondaryQuantities;
    }
}
