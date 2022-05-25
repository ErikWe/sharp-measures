namespace SharpMeasures.Generators.Quantities;

using System;

/// <summary>Describes the set of quantities that are dimensionally equivalent to this quantity, and that this quantity may be converted to.</summary>
/// <remarks>For example, <i>Energy</i> is considered dimensionally equivalant to <i>Potential Energy</i>.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]

public sealed class DimensionalEquivalenceAttribute  : Attribute
{
    /// <summary>The set of quantities that are dimensionally equivalent to this quantity.</summary>
    public Type[] Quantities { get; }

    /// <inheritdoc cref="DimensionalEquivalenceAttribute"/>
    /// <param name="quantities"><inheritdoc cref="Quantities" path="/summary"/></param>
    public DimensionalEquivalenceAttribute(params Type[] quantities)
    {
        Quantities = quantities;
    }
}
