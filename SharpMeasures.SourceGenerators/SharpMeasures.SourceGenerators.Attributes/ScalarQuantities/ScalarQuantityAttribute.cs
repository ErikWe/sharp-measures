namespace ErikWe.SharpMeasures.Attributes;

using System;

/// <summary>Allows a scalar quantity to be enhanced through source generation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ScalarQuantityAttribute : Attribute
{
    /// <summary>Name of the property that represents the magnitude of the scalar quantity.</summary>
    public string MagnitudePropertyName { get; set; } = "Magnitude";
}
