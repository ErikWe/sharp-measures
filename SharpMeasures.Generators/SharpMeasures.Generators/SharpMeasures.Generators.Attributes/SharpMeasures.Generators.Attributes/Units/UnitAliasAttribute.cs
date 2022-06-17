namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit as an alias for another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class UnitAliasAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form. May be identical to the singular form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</remarks>
    public string Plural { get; }
    /// <summary>The name of the original instance, of which this instance is an alias.</summary>
    public string AliasOf { get; }

    /// <summary>Constructs a definition of an instance of the unit, using a fixed value.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form. May be identical to the singular form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para></param>
    /// <param name="aliasOf">The name of the original instance, for which this is an alias.</param>
    public UnitAliasAttribute(string name, string plural, string aliasOf)
    {
        Name = name;
        Plural = plural;
        AliasOf = aliasOf;
    }
}
