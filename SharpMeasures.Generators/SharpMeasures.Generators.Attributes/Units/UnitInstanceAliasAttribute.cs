namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit as an alias for another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class UnitInstanceAliasAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <summary><inheritdoc cref="FixedUnitInstanceAttribute.PluralForm" path="/summary"/></summary>
    /// <remarks>If <see cref="PluralFormRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotations"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string PluralForm { get; }
    /// <summary>The name of the original instance, for which this instance is an alias.</summary>
    public string AliasOf { get; }

    /// <summary>Used as the .NET Regex substitution string when producing the plural form of the unit, with <see cref="PluralForm"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="PluralForm"/> will not be used as a .NET regex pattern.</remarks>
    public string PluralFormRegexSubstitution { get; init; } = string.Empty;

    /// <inheritdoc cref="UnitInstanceAliasAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="aliasOf"><inheritdoc cref="AliasOf" path="/summary"/><para><inheritdoc cref="AliasOf" path="/remarks"/></para></param>
    public UnitInstanceAliasAttribute(string name, string pluralForm, string aliasOf)
    {
        Name = name;
        PluralForm = pluralForm;
        AliasOf = aliasOf;
    }
}
