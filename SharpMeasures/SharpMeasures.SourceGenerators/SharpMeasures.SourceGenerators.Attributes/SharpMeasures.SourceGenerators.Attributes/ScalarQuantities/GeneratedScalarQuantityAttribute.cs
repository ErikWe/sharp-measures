namespace SharpMeasures;

using System;

/// <summary>Marks the type as a scalar quantity, and enables source generation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedScalarQuantityAttribute : Attribute
{
    /// <summary>Name of the property that represents the magnitude of the scalar quantity.</summary>
    public string MagnitudePropertyName { get; set; } = "Magnitude";
}
