namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit as a derivation from instances of other units.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form. May be identical to the singular form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</remarks>
    public string Plural { get; }
    /// <summary>The unique ID of the intended derivation signature.</summary>
    /// <remarks>The unit itself has to be marked with a <see cref="DerivableUnitAttribute"/> describing a derivation with this ID.
    /// <para>If <see langword="null"/> and only one signature is defined for the unit, that definition is used. If more than one definition exists, the
    /// signature must be explicitly specified.</para></remarks>
    public string? SignatureID { get; }
    /// <summary>The names of the instances of other units from which this instance is derived. The order must match that of the derivation signature
    /// with ID <see cref="SignatureID"/>.</summary>
    public string[] Units { get; }

    /// <summary>Constructs a definition of an instance of the unit as a derivation from instances of other units.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form. May be identical to the singular form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para></param>
    /// <param name="signatureID">The unique ID of the intended derivation signature.
    /// <para>The unit itself has to be marked with a <see cref="DerivableUnitAttribute"/> describing a derivation with this ID.</para>
    /// <para>If <see langword="null"/> and only one signature is defined for the unit, that definition is used. If more than one definition exists, the
    /// signature must be explicitly specified.</para></param>
    /// <param name="units">The names of the instances of other units from which this instance is derived. The order must match that of the derivation signature
    /// with ID <paramref name="signatureID"/>.</param>
    public DerivedUnitAttribute(string name, string plural, string signatureID, params string[] units)
    {
        Name = name;
        Plural = plural;
        SignatureID = signatureID;
        Units = units;
    }

    /// <summary>Constructs a definition of an instance of the unit as a derivation from instances of other units.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form. May be identical to the singular form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para></param>
    /// <param name="units">The names of the instances of other units from which this instance is derived. The order must match that of the definition provided
    /// in the only <see cref="DerivableUnitAttribute"/> on the unit.</param>
    /// <remarks>This constructor may only be used if the unit is decorated with exactly one <see cref="DerivableUnitAttribute"/>. Otherwise,
    /// <see cref="DerivedUnitAttribute(string, string, string, string[])"/> should be used - where the signature ID is explicitly specified.</remarks>
    public DerivedUnitAttribute(string name, string plural, string[] units)
    {
        Name = name;
        Plural = plural;
        Units = units;

        SignatureID = null;
    }
}
