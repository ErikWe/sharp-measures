namespace SharpMeasures.Generators;

using System;

/// <summary>Derives an instance of a unit from instances of other units, according to a definition in a specified <see cref="DerivableUnitAttribute"/>.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <summary><inheritdoc cref="FixedUnitInstanceAttribute.PluralForm" path="/summary"/></summary>
    /// <remarks>If <see cref="PluralFormRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotation"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string PluralForm { get; }
    /// <summary>The ID of the intended derivation signature.</summary>
    /// <remarks>This is only required to be explicitly specified if more than one derivation is defined for the unit.</remarks>
    public string? DerivationID { get; }
    /// <summary>The names of the instances of other units from which this instance is derived. The order must match that of the derivation signature
    /// specified by <see cref="DerivationID"/>.</summary>
    public string[] Units { get; }

    /// <summary>Used as the .NET Regex substitution string when producing the plural form of the unit, with <see cref="PluralForm"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="PluralForm"/> will not be used as a .NET regex pattern.</remarks>
    public string PluralFormRegexSubstitution { get; init; } = string.Empty;

    /// <inheritdoc cref="DerivedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/><para><inheritdoc cref="DerivationID" path="/remarks"/></para></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/><para><inheritdoc cref="Units" path="/remarks"/></para></param>
    public DerivedUnitInstanceAttribute(string name, string pluralForm, string derivationID, string[] units)
    {
        Name = name;
        PluralForm = pluralForm;
        DerivationID = derivationID;
        Units = units;
    }

    /// <summary><inheritdoc cref="DerivedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/><para><inheritdoc cref="Units" path="/remarks"/></para></param>
    /// <remarks>This constructor may only be used if the unit is decorated with exactly one <see cref="DerivableUnitAttribute"/>. Otherwise,
    /// the derivation ID should be explicitly specified - using <see cref="DerivedUnitInstanceAttribute(string, string, string, string[])"/>.</remarks>
    public DerivedUnitInstanceAttribute(string name, string pluralForm, string[] units)
    {
        Name = name;
        PluralForm = pluralForm;
        Units = units;

        DerivationID = null;
    }
}
