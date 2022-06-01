namespace SharpMeasures.Generators.Quantities.Utility;

/// <summary>Describes whether the conversion operation is implemented as an explicit or an implicit operator.</summary>
public enum ConversionOperationBehaviour
{
    /// <summary>The conversion operation is not implemented.</summary>
    None,
    /// <summary>The conversion operation is implemented as an explicit operator.</summary>
    Explicit,
    /// <summary>The conversion operation is implemented as an implicit operator.</summary>
    Implicit
}