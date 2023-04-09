namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit.</summary>
/// <remarks>A fixed unit instance should define the base of a unit, from which other units are derived.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class FixedUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the unit instance, in singular form.</summary>
    public string Name { get; }

    /// <summary>The name of the unit instance, in plural form - or <see langword="null"/> if the plural form is identical to the singular form.</summary>
    public string? PluralForm { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    public FixedUnitInstanceAttribute(string name, string? pluralForm)
    {
        Name = name;
        PluralForm = pluralForm;
    }

    /// <inheritdoc cref="FixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    public FixedUnitInstanceAttribute(string name)
    {
        Name = name;
    }
}
