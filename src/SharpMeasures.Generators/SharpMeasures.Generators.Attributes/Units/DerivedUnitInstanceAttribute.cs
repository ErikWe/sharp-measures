namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit using instances of other units, according to a definition provided by a <see cref="DerivableUnitAttribute"/>.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string? PluralForm { get; }

    /// <summary>The ID of the intended derivation signature, as defined by a <see cref="DerivableUnitAttribute"/> - or <see langword="null"/> if there is exactly one derivation available.</summary>
    public string? DerivationID { get; }

    /// <summary>The names of the unit instances of other units from which this unit instance is derived. The order must match that of the derivation signature.</summary>
    public string[] Units { get; }

    /// <inheritdoc cref="DerivedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/></param>
    public DerivedUnitInstanceAttribute(string name, string? pluralForm, string? derivationID, string[] units)
    {
        Name = name;
        PluralForm = pluralForm;
        DerivationID = derivationID;
        Units = units;
    }

    /// <summary><inheritdoc cref="DerivedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/></param>
    public DerivedUnitInstanceAttribute(string name, string? pluralForm, string[] units)
    {
        Name = name;
        PluralForm = pluralForm;
        Units = units;
    }

    /// <summary><inheritdoc cref="DerivedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/></param>
    public DerivedUnitInstanceAttribute(string name, string[] units)
    {
        Name = name;
        Units = units;
    }
}
