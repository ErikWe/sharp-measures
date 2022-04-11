namespace SharpMeasures.Generators;

using System;

/// <summary>Describes how a unit may be derived from other units.</summary>
/// <remarks>This attribute is expected to be used in conjunction with <see cref="GeneratedUnitAttribute"/> or <see cref="GeneratedBiasedUnitAttribute"/>.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitAttribute : Attribute
{
    /// <summary>The unit may be derived from a combination of the units of these <see cref="Type"/>s, according to <see cref="Expression"/>.</summary>
    public Type[] Signature { get; }
    /// <summary>The unit may be derived according to this expression. The types from <see cref="Signature"/> are inserted into this expression,
    /// replacing occurences of "{x}" - where x is the zero-based index of that type in <see cref="Signature"/>.</summary>
    /// <remarks>See <see cref="Utility.UnitDerivations"/> for some common expressions.</remarks>
    public string Expression { get; }

    /// <summary>Constructs a description of how a unit may be derived from other units.</summary>
    /// <param name="expression">he unit may be derived according to this expression. The types from <paramref name="signature"/> are inserted into this expression,
    /// replacing occurences of "{x}" - where x is the zero-based index of that type in <paramref name="signature"/>.
    /// <para>See <see cref="Utility.UnitDerivations"/> for some common expressions.</para></param>
    /// <param name="signature">The unit may be derived from a combination of the units of these <see cref="Type"/>s, according to <paramref name="expression"/>.</param>
    public DerivedUnitAttribute(string expression, params Type[] signature)
    {
        Signature = signature;
        Expression = expression;
    }
}
