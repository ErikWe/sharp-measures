namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Utility;

using System;

/// <summary>Defines an instance of a unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class FixedUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="CommonPluralNotations"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string Plural { get; }

    /// <inheritdoc cref="FixedUnitAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    public FixedUnitAttribute(string name, string plural)
    {
        Name = name;
        Plural = plural;
    }
}
