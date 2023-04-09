namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit as an alias for another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class UnitInstanceAliasAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string? PluralForm { get; }

    /// <summary>The name of the original unit instance, for which this unit instance is an alias.</summary>
    public string AliasOf { get; }

    /// <inheritdoc cref="UnitInstanceAliasAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="aliasOf"><inheritdoc cref="AliasOf" path="/summary"/></param>
    public UnitInstanceAliasAttribute(string name, string? pluralForm, string aliasOf)
    {
        Name = name;
        PluralForm = pluralForm;
        AliasOf = aliasOf;
    }

    /// <inheritdoc cref="UnitInstanceAliasAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="aliasOf"><inheritdoc cref="AliasOf" path="/summary"/></param>
    public UnitInstanceAliasAttribute(string name, string aliasOf)
    {
        Name = name;
        AliasOf = aliasOf;
    }
}
