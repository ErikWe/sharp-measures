namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit according to a bias relative to another instance of the same unit.</summary>
/// <remarks>Applying a bias requires the unit to include a bias term. This is configured through <see cref="SharpMeasuresUnitAttribute"/>.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class BiasedUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.
    /// <para>This value is ignored if the unit is a constant.</para></remarks>
    public string Plural { get; }
    /// <summary>The name of the original instance, relative to which this instance is biased.</summary>
    public string From { get; }
    /// <summary>The bias relative to the original instance. This is the value of this instance when the original instance is zero.</summary>
    public double Bias { get; }

    /// <summary>Defines an instance of the unit according to a bias relative to another instance of the same unit.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para>
    /// <para>This value is ignored if the unit is a constant.</para></param>
    /// <param name="from">The name of the original instance, relative to which this instance is biased.</param>
    /// <param name="bias">The bias relative to the original instance. This is the value of this instance when the original instance is zero.</param>
    public BiasedUnitAttribute(string name, string plural, string from, double bias)
    {
        Name = name;
        Plural = plural;
        From = from;
        Bias = bias;
    }
}
