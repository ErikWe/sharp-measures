namespace SharpMeasures.Generators.Utility;

/// <summary>Possible behaviours for the conversion operator.</summary>
public enum ConversionOperatorBehaviour
{
    /// <summary>The conversion operation is not implemented.</summary>
    None,
    /// <summary>The conversion operation is implemented as an explicit operator.</summary>
    Explicit,
    /// <summary>The conversion operation is implemented as an implicit operator.</summary>
    Implicit
}
