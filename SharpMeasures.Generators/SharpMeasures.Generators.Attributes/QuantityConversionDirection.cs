namespace SharpMeasures.Generators;

using System;

/// <summary>Describes the direction of conversion between two quantities.</summary>
[Flags]
public enum QuantityConversionDirection
{
    /// <summary>Indicates that neither quantity may be converted to the other quantity.</summary>
    None = 0,
    /// <summary>Indicates that a given quantity may be converted to another quantity, but the reverse conversion (<see cref="Antidirectional"/>) is not supported.</summary>
    Onedirectional = 1,
    /// <summary>Indicates that another quantity may be converted to the given quantity, but the reverse conversion (<see cref="Onedirectional"/>) is not supported.</summary>
    Antidirectional = 2,
    /// <summary>Indicates that both quantities may be converted to the other quantity.</summary>
    Bidirectional = Onedirectional | Antidirectional
}
