namespace SharpMeasures.Generators.Quantities;

using System;

/// <summary>Describes how a quantity may be derived from other quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedQuantityAttribute : Attribute
{
    /// <summary>The expression used to derive this quantity. The quantities of <see cref="Signature"/> are inserted into this expression,
    /// using normal string formatting syntax - occurences of "{X}" are replaced with the X´th element of <see cref="Signature"/>.</summary>
    /// <remarks>Some common expressions are defined in <see cref="CommonAlgebraicDerivations"/>.</remarks>
    public string Expression { get; }
    /// <summary>The quantity may be derived from these quantities, according to <see cref="Expression"/>.</summary>
    public Type[] Signature { get; }

    /// <summary>Describes whether this type implements the algebrically equivalent operations as operators - and if so, how they are implemented. The default behaviour is <see cref="DerivationOperatorImplementation.Suitable"/>.</summary>
    /// <remarks>For example, if a type <i>Speed</i> defines a derivation { <i>Length</i> / <i>Time</i> } with <see cref="DerivationOperatorImplementation.All"/>, the following operators would be implemented:
    /// <list type="bullet">
    /// <item><i>Length</i> = { <i>Speed</i> * <i>Time</i> }</item>
    /// <item><i>Length</i> = { <i>Time</i> * <i>Speed</i> }</item>
    /// <item><i>Time</i> = { <i>Length</i> / <i>Speed</i> }</item>
    /// </list>
    /// If <i>Length</i> defines the algebrically equivalent derivations, this would result in duplicate operator implementations. In such cases, <see cref="DerivationOperatorImplementation.Suitable"/>, <see cref="DerivationOperatorImplementation.LeftHandSide"/>, or <see cref="DerivationOperatorImplementation.RightHandSide"/> is recommended.</remarks>
    public DerivationOperatorImplementation OperatorImplementation { get; init; }

    /// <summary>Whether to also allow derivation using permutations of the signature. The default behaviour is <see langword="false"/>.</summary>
    public bool Permutations { get; init; }

    /// <inheritdoc cref="DerivedQuantityAttribute"/>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/><para><inheritdoc cref="Signature" path="/remarks"/></para></param>
    public DerivedQuantityAttribute(string expression, params Type[] signature)
    {
        Expression = expression;
        Signature = signature;
    }
}
