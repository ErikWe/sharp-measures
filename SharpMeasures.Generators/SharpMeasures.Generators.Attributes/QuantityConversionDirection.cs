namespace SharpMeasures.Generators;

/// <summary>Possible directions for conversions between quantities.</summary>
public enum QuantityConversionDirection
{
    /// <summary>A given quantity may be converted to another quantity.</summary>
    Onedirectional,
    /// <summary>Two quantities may be converted to each other.</summary>
    Bidirectional,
    /// <summary>Another quantity may be converted to the given quantity.</summary>
    Antidirectional
}
