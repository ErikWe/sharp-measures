namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, describing how unit instances may be derived from instances of other units.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivableUnitAttribute : Attribute
{
    /// <summary>The unique ID of this derivation - allowed to be <see langword="null"/> if this is the only derivation defined by the unit.</summary>
    public string? DerivationID { get; }

    /// <summary>The expression used to derive new instances of this unit. Occurrences of "{k}" are replaced by the k-th unit in the provided signature.</summary>
    /// <remarks>Some common expressions are defined in <see cref="CommonAlgebraicDerivations"/>.</remarks>
    public string Expression { get; }

    /// <summary>The units used to derive instances of this unit, according to the provided expression.</summary>
    public Type[] Signature { get; }

    /// <inheritdoc cref="DerivableUnitAttribute"/>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    public DerivableUnitAttribute(string? derivationID, string expression, params Type[] signature)
    {
        DerivationID = derivationID;
        Expression = expression;
        Signature = signature;
    }

    /// <inheritdoc cref="DerivableUnitAttribute"/>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    public DerivableUnitAttribute(string expression, params Type[] signature)
    {
        DerivationID = null;
        Expression = expression;
        Signature = signature;
    }
}
