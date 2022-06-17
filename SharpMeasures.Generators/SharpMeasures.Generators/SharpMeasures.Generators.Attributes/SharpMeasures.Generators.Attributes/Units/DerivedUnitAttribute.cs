namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit as a derivation from instances of other units.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.
    /// <para>This value is ignored if the unit is a constant.</para></remarks>
    public string Plural { get; }
    /// <summary>The types of units that this unit is derived from.</summary>
    /// <remarks>The unit itself has to be marked with a <see cref="DerivableUnitAttribute"/> describing an identical signature.</remarks>
    public Type[] Signature { get; }
    /// <summary>The names of the instances of other units from which this instance is derived. The order must match that of <see cref="Signature"/>.</summary>
    public string[] Units { get; }

    /// <summary>Constructs a definition of an instance of the unit as a derivation from instances of other units.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para>
    /// <para>This value is ignored if the unit is a constant.</para></param>
    /// <param name="signature">The types of units that this unit is derived from.
    /// <para>The unit itself has to be marked with a <see cref="DerivableUnitAttribute"/> describing an identical signature.</para></param>
    /// <param name="units">The names of the instances of other units from which this instance is derived. The order must match
    /// that of <paramref name="signature"/>.</param>
    public DerivedUnitAttribute(string name, string plural, Type[] signature, string[] units)
    {
        Name = name;
        Plural = plural;
        Units = units;
        Signature = signature;
    }
}
