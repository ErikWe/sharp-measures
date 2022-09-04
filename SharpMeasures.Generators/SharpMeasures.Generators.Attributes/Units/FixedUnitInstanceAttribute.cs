namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class FixedUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>If <see cref="PluralFormRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotations"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string PluralForm { get; }

    /// <summary>Used as the .NET Regex substitution string when producing the plural form of the unit, with <see cref="PluralForm"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="PluralForm"/> will not be used as a .NET regex pattern.</remarks>
    public string PluralFormRegexSubstitution { get; init; } = string.Empty;

    /// <inheritdoc cref="FixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    public FixedUnitInstanceAttribute(string name, string pluralForm)
    {
        Name = name;
        PluralForm = pluralForm;
    }
}
