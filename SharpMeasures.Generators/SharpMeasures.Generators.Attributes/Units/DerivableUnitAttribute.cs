namespace SharpMeasures.Generators;

using System;

/// <summary>Describes how an instance of a unit may be derived from instances of other units. The defined derivation may then be used through <see cref="DerivedUnitInstanceAttribute"/>.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivableUnitAttribute : Attribute
{
    /// <summary>A unique ID for this definition, used by <see cref="DerivedUnitInstanceAttribute"/> to specify a derivation.</summary>
    /// <remarks>This is only required to be explicitly specified if more than one derivation is defined for the unit.</remarks>
    public string? DerivationID { get; }
    /// <summary>The expression used to derive new instances of this unit. The types of <see cref="Signature"/> are inserted into this expression,
    /// using normal string formatting syntax - occurences of "{X}" are replaced with the X´th element of <see cref="Signature"/>.</summary>
    /// <remarks>Some common expressions are defined in <see cref="CommonAlgebraicDerivations"/>.</remarks>
    public string Expression { get; }
    /// <summary>The unit may be derived from instances of these units, according to <see cref="Expression"/>.</summary>
    public Type[] Signature { get; }
    /// <summary>Dictates whether to also allow derivation using permutations of the signature. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>This does not apply to deriving units through <see cref="DerivedUnitInstanceAttribute"/>.</remarks>
    public bool Permutations { get; init; }

    /// <inheritdoc cref="DerivableUnitAttribute"/>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/><para><inheritdoc cref="DerivationID" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/><para><inheritdoc cref="Signature" path="/remarks"/></para></param>
    public DerivableUnitAttribute(string derivationID, string expression, params Type[] signature)
    {
        DerivationID = derivationID;
        Expression = expression;
        Signature = signature;
    }

    /// <inheritdoc cref="DerivableUnitAttribute"/>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/><para><inheritdoc cref="Signature" path="/remarks"/></para></param>
    /// <remarks>This constructor may only be used if the unit is decorated with exactly one <see cref="DerivableUnitAttribute"/>. Otherwise,
    /// the derivation ID should be explicitly specified - using <see cref="DerivableUnitAttribute(string, string, Type[])"/>.</remarks>
    public DerivableUnitAttribute(string expression, params Type[] signature)
    {
        DerivationID = null;
        Expression = expression;
        Signature = signature;
    }
}
