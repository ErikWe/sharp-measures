namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Describes how a unit may be derived from other units.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivableUnitAttribute : Attribute
{
    /// <summary>A unique ID for referring to this derivation definition.</summary>
    public string DerivationID { get; }
    /// <summary>The unit may be derived according to this expression. The types from <see cref="Signature"/> are inserted into this expression,
    /// replacing occurences of "{x}" - where x is the zero-based index of that type in <see cref="Signature"/>.</summary>
    /// <remarks>See <see cref="Utility.UnitDerivations"/> for some common expressions.</remarks>
    public string Expression { get; }
    /// <summary>The unit may be derived from a combination of the units of these <see cref="Type"/>s, according to <see cref="Expression"/>.</summary>
    public Type[] Signature { get; }

    /// <summary>Constructs a description of how a unit may be derived from other units.</summary>
    /// <param name="derivationID">A unique ID for referring to this derivation definition.</param>
    /// <param name="expression">he unit may be derived according to this expression. The types from <paramref name="signature"/> are inserted into this expression,
    /// replacing occurences of "{x}" - where x is the zero-based index of that type in <paramref name="signature"/>.
    /// <para>See <see cref="Utility.UnitDerivations"/> for some common expressions.</para></param>
    /// <param name="signature">The unit may be derived from a combination of the units of these <see cref="Type"/>s, according to <paramref name="expression"/>.</param>
    public DerivableUnitAttribute(string derivationID, string expression, params Type[] signature)
    {
        Expression = expression;
        DerivationID = derivationID;
        Signature = signature;
    }
}
