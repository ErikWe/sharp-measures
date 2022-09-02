namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Utility;

using System;

/// <summary>Describes how a quantity may be derived from other quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedQuantityAttribute : Attribute
{
    /// <summary>The expression used to derive this quantity. The types of <see cref="Signature"/> are inserted into this expression,
    /// using normal string formatting syntax - occurences of "{X}" are replaced with the X´th element of <see cref="Signature"/>.</summary>
    /// <remarks>Some common expressions are defined in <see cref="CommonAlgebraicDerivations"/>.</remarks>
    public string Expression { get; }
    /// <summary>The quantity may be derived from these quantities, according to <see cref="Expression"/>.</summary>
    public Type[] Signature { get; }

    /// <summary>Dictates whether operators should be implemented on the deriving types. The default behaviour is <see langword="true"/>, but will silently
    /// fail if not applicable.</summary>
    /// <remarks>For example; for a derived quantity { <i>Speed</i> = <i>Length</i> / <i>Time</i> }, <i>Length</i> would implement the operator { / }, with
    /// arguments (<i>Length</i>, <i>Time</i>), resulting in a <i>Speed</i>.
    /// <para>Operators will only be implemented for types that share assembly with this quantity - and also requires the expressions to include no more than two
    /// quantities, separated by one of the operators { +, -, *, and / }.</para></remarks>
    public bool ImplementOperators { get; init; }

    /// <summary>Dictates whether the algebraically equivalent derivations are also implemented. The default behaviour is <see langword="true"/>, but will silently
    /// fail if not applicable.</summary>
    /// <remarks>For example; a definition { <i>Speed</i> = <i>Length</i> / <i>Time</i> } would result in the following derivations:
    /// <list type="bullet">
    /// <item>
    /// <term>Speed</term>
    /// <description>Derived according to { <i>Length</i> / <i>Time</i> }.</description>
    /// </item>
    /// <item>
    /// <term>Length</term>
    /// <description>Derived according to { <i>Speed</i> * <i>Time</i> }.</description>
    /// </item>
    /// <item>
    /// <term>Time</term>
    /// <description>Derived according to { <i>Length</i> / <i>Speed</i> }.</description>
    /// </item>
    /// </list>
    /// <para>If <see cref="ImplementOperators"/> is enabled, operators will be implemented also for the algebraically equivalent derivations.</para>
    /// <para>Derivations will only be implemented for types that share assembly with this quantity.</para></remarks>
    public bool ImplementAlgebraicallyEquivalentDerivations { get; init; }

    /// <inheritdoc cref="DerivedQuantityAttribute"/>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/><para><inheritdoc cref="Signature" path="/remarks"/></para></param>
    public DerivedQuantityAttribute(string expression, params Type[] signature)
    {
        Expression = expression;
        Signature = signature;
    }
}
