namespace SharpMeasures.Generators;

using System;

/// <summary>Defines an instance of the unit by applying a metric prefix to another instance of the same unit.</summary>
/// <remarks>This attribute is expected to be used in conjunction with <see cref="GeneratedUnitAttribute"/> or <see cref="GeneratedBiasedUnitAttribute"/>.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class PrefixedUnitInstanceAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</remarks>
    public string Plural { get; }
    /// <summary>The symbol of the instance of the unit.</summary>
    public string Symbol { get; init; } = string.Empty;
    /// <summary>Indicates whether this is a SI unit.</summary>
    public bool IsSIUnit { get; init; }
    /// <summary>Indicates whether this is a constant.</summary>
    public bool IsConstant { get; init; }
    /// <summary>The name of the instance to which the metric prefix is applied.</summary>
    public string From { get; }
    /// <summary>The name of the metric prefix.</summary>
    public string Prefix { get; }

    /// <summary>Constructs a definition of an instance of the unit by applying a metric prefix to another instance of the same unit.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.</param>
    /// <param name="from">The name of the instance to which the metric prefix is applied.</param>
    /// <param name="prefix">The name of the metric prefix.</param>
    public PrefixedUnitInstanceAttribute(string name, string plural, string from, string prefix)
    {
        Name = name;
        Plural = plural;
        From = from;
        Prefix = prefix;
    }
}
