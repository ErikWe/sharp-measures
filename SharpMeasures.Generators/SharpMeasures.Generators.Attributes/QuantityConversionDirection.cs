namespace SharpMeasures.Generators;

/// <summary>Possible directions for conversions between quantities.</summary>
public enum QuantityConversionDirection
{
    /// <summary>A given quantity may be converted to another quantity.</summary>
    Onedirectional,
    /// <summary>Two quantities are convertible to each other.</summary>
    Bidirectional,
    /// <summary>Another quantity is convertible to the given quantity.</summary>
    Antidirectional
}
