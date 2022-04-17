namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as a scalar quantity, and enables source generation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedScalarQuantityAttribute : Attribute
{
    /// <summary>The unit that describes the scalar quantity.</summary>
    public Type Unit { get; }

    /// <summary>Dictates whether this quantity requires a bias term.</summary>
    public bool Biased { get; init; }

    /// <summary>Name of the property that represents the magnitude of the scalar quantity.</summary>
    public string MagnitudePropertyName { get; init; } = "Magnitude";

    /// <summary>Marks the type as a scalar quantity, and enables source generation.</summary>
    /// <param name="unit">The unit that describes the scalar quantity.</param>
    public GeneratedScalarQuantityAttribute(Type unit)
    {
        Unit = unit;
    }
}
