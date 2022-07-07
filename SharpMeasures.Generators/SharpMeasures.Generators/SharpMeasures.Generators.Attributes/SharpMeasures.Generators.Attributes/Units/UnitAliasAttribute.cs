namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit as an alias for another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class UnitAliasAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitAttribute.Plural"/>
    public string Plural { get; }
    /// <summary>The name of the original instance, for which this instance is an alias.</summary>
    public string AliasOf { get; }

    /// <inheritdoc cref="UnitAliasAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="aliasOf"><inheritdoc cref="AliasOf" path="/summary"/><para><inheritdoc cref="AliasOf" path="/remarks"/></para></param>
    public UnitAliasAttribute(string name, string plural, string aliasOf)
    {
        Name = name;
        Plural = plural;
        AliasOf = aliasOf;
    }
}
