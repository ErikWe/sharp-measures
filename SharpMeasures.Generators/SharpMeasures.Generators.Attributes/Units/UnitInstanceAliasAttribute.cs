namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit as an alias for another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class UnitInstanceAliasAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string PluralForm { get; }
    /// <summary>The name of the original instance, for which this instance is an alias.</summary>
    public string AliasOf { get; }

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
