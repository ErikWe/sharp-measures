namespace SharpMeasures;

/// <summary>Describes the position of a quantity in an operation of two quantities.</summary>
public enum OperationPosition
{
    /// <summary>The <see cref="OperationPosition"/> is unknown.</summary>
    Unknown,
    /// <summary>The quantity is the left-hand-side of the operation.</summary>
    Left,
    /// <summary>The quantity is the right-hand-side of the operation.</summary>
    Right
}
