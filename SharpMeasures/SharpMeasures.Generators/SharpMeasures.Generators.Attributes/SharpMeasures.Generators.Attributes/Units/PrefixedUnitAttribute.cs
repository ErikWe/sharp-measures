namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of the unit by applying a prefix to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class PrefixedUnitAttribute : Attribute
{
    /// <summary>The name of the instance of the unit, in singular form.</summary>
    public string Name { get; }
    /// <summary>The name of the instance of the unit, in plural form.</summary>
    /// <remarks>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.
    /// <para>This value is ignored if the unit is a constant.</para></remarks>
    public string Plural { get; }
    /// <summary>The name of the instance to which the prefix is applied.</summary>
    public string From { get; }
    /// <summary>The metric prefix.</summary>
    public MetricPrefixName MetricPrefixName { get; }
    /// <summary>The binary prefix.</summary>
    public BinaryPrefixName BinaryPrefixName { get; }

    /// <summary>Constructs a definition of an instance of the unit by applying a metric prefix to another instance of the same unit.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para>
    /// <para>This value is ignored if the unit is a constant.</para></param>
    /// <param name="from">The name of the instance to which the metric prefix is applied.</param>
    /// <param name="metricPrefixName">The metric prefix.</param>
    public PrefixedUnitAttribute(string name, string plural, string from, MetricPrefixName metricPrefixName)
    {
        Name = name;
        Plural = plural;
        From = from;
        MetricPrefixName = metricPrefixName;
    }

    /// <summary>Constructs a definition of an instance of the unit by applying a binary prefix to another instance of the same unit.</summary>
    /// <param name="name">The name of the instance of the unit, in singular form.</param>
    /// <param name="plural">The name of the instance of the unit, in plural form.
    /// <para>See <see cref="Utility.UnitPluralCodes"/> for some short-hand notations for producing the plural form based on the singular form.</para>
    /// <para>This value is ignored if the unit is a constant.</para></param>
    /// <param name="from">The name of the instance to which the binary prefix is applied.</param>
    /// <param name="binaryPrefixName">The binary prefix.</param>
    public PrefixedUnitAttribute(string name, string plural, string from, BinaryPrefixName binaryPrefixName)
    {
        Name = name;
        Plural = plural;
        From = from;
        BinaryPrefixName = binaryPrefixName;
    }
}
