namespace SharpMeasures;

/// <summary>Describes operators acting on two quantities.</summary>
public enum OperatorType
{
    /// <summary>The <see cref="OperatorType"/> is unknown.</summary>
    Unknown,
    /// <summary>The { + } operator, describing addition.</summary>
    Addition,
    /// <summary>The { - } operator, describing subtraction.</summary>
    Subtraction,
    /// <summary>The scalar { ⋅ } operator acting on scalar quantities, describing multiplication.</summary>
    Multiplication,
    /// <summary>The { ÷ } operator, describing division.</summary>
    Division,
    /// <summary>The { ⋅ } operator acting on vector quantities, describing dot-product.</summary>
    Dot,
    /// <summary>The { ⨯ } operator, decribing cross-product.</summary>
    Cross
}
