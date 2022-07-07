namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Derives an instance of a unit from instances of other units, according to a definition in a specified <see cref="DerivableUnitAttribute"/>.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DerivedUnitAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitAttribute.Plural"/>
    public string Plural { get; }
    /// <summary>The ID of the intended derivation signature.</summary>
    /// <remarks>This is only required to be explicitly specified if more than one derivation is defined for the unit.</remarks>
    public string? DerivationID { get; }
    /// <summary>The names of the instances of other units from which this instance is derived. The order must match that of the derivation signature
    /// specified by <see cref="DerivationID"/>.</summary>
    public string[] Units { get; }

    /// <inheritdoc cref="DerivedUnitAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/><para><inheritdoc cref="DerivationID" path="/remarks"/></para></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/><para><inheritdoc cref="Units" path="/remarks"/></para></param>
    public DerivedUnitAttribute(string name, string plural, string derivationID, params string[] units)
    {
        Name = name;
        Plural = plural;
        DerivationID = derivationID;
        Units = units;
    }

    /// <summary><inheritdoc cref="DerivedUnitAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="units"><inheritdoc cref="Units" path="/summary"/><para><inheritdoc cref="Units" path="/remarks"/></para></param>
    /// <remarks>This constructor may only be used if the unit is decorated with exactly one <see cref="DerivableUnitAttribute"/>. Otherwise,
    /// the derivation ID should be explicitly specified - using <see cref="DerivedUnitAttribute(string, string, string, string[])"/>.</remarks>
    public DerivedUnitAttribute(string name, string plural, string[] units)
    {
        Name = name;
        Plural = plural;
        Units = units;

        DerivationID = null;
    }
}
