namespace SharpMeasures.Generators;

/// <summary>Describes operators acting on two vector quantities.</summary>
/// <remarks>See <see cref="OperatorType"/> for operators not limited to vector quantities.</remarks>
public enum VectorOperatorType
{
    /// <summary>The { ⋅ } operator, describing dot-product.</summary>
    Dot,
    /// <summary>The { ⨯ } operator, decribing cross-product.</summary>
    Cross
}
