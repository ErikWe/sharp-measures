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

    /// <summary>Describes whether the derivation is implemented through operators - and if so, how it is implemented. The default behaviour is <see cref="DerivationOperatorImplementation.AlgebraicallyEquivalent"/>.</summary>
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
