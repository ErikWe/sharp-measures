namespace SharpMeasures.Generators;

/// <summary>Describes operators acting on two quantities.</summary>
/// <remarks>See <see cref="VectorOperatorType"/> for operators limited to vector quantities.</remarks>
public enum OperatorType
{
    /// <summary>The { + } operator, describing addition.</summary>
    Addition,
    /// <summary>The { - } operator, describing subtraction.</summary>
    Subtraction,
    /// <summary>The scalar { ⋅ } operator, describing multiplication.</summary>
    /// <remarks>For the vector operators { ⋅ , ⨯ } see <see cref="VectorOperatorType.Dot"/> and <see cref="VectorOperatorType.Cross"/>, respectively.</remarks>
    Multiplication,
    /// <summary>The { ÷ } operator, describing division.</summary>
    Division
}
