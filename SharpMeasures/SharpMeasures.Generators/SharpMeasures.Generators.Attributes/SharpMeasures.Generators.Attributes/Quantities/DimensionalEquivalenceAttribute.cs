namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Quantities.Utility;

using System;

/// <summary>Describes the set of quantities that are dimensionally equivalent to this quantity, and that this quantity may be converted to.</summary>
/// <remarks>For example, <i>Energy</i> could be considered dimensionally equivalant to <i>Potential Energy</i>.
/// <para>For a vector quantity, this applies to sets of associated vector quantities rather than the individual vector quantity. For example; if
/// this attribute is applied to a type <i>DisplacementX</i>, with <i>PositionY</i> listed as a dimensionally equivalent quantity - this will be
/// interpreted as the following conversions being supported:</para>
/// <list type="bullet">
/// <item>DisplacementX → PositionX</item>
/// <item>DisplacementY → PositionY</item>
/// <item>DisplacementZ → PositionZ</item>
/// <item>... (for all pairs where both types exists)</item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DimensionalEquivalenceAttribute  : Attribute
{
    /// <summary>The set of quantities that are dimensionally equivalent to this quantity.</summary>
    public Type[] Quantities { get; }

    /// <summary>Determines whether the operators for converting from this quantity to any of the listed quantities are implemented
    /// as explicit or implicit operators.
    /// <para>The default behaviour is <see cref="ConversionOperationBehaviour.Explicit"/>.</para></summary>
    public ConversionOperationBehaviour CastOperatorBehaviour { get; init; } = ConversionOperationBehaviour.Explicit;

    /// <inheritdoc cref="DimensionalEquivalenceAttribute"/>
    /// <param name="quantities"><inheritdoc cref="Quantities" path="/summary"/></param>
    public DimensionalEquivalenceAttribute(params Type[] quantities)
    {
        Quantities = quantities;
    }
}
