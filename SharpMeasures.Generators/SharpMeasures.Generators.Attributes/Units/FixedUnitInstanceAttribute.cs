namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class FixedUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="CommonPluralNotations"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string PluralForm { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    public FixedUnitInstanceAttribute(string name, string pluralForm)
    {
        Name = name;
        PluralForm = pluralForm;
    }
}
