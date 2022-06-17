namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit, using a fixed value.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class FixedUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form. May be identical to the singular form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</remarks>
    public string Plural { get; }
    /// <summary>The fixed value of the instance of the unit.</summary>
    public double Value { get; }
    /// <summary>The bias of the instance of the unit. This is only used if the unit includes a bias term.</summary>
    /// <remarks>This is the value of this instance of the unit when the "reference instance" is zero. What instance is defined as the "reference instance"
    /// is arbitrary, but should be consistent across all instances.</remarks>
    public double Bias { get; init; }

    /// <summary>Constructs a definition of an instance of the unit, using a fixed value.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form. May be identical to the singular form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para></param>
    /// <param name="value">The fixed value of the instance of the unit.</param>
    public FixedUnitAttribute(string name, string plural, double value)
    {
        Name = name;
        Plural = plural;
        Value = value;
    }
}
