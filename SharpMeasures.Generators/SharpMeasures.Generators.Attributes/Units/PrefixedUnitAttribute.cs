namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Units.Utility;

using System;

/// <summary>Defines an instance of a unit by applying a prefix to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class PrefixedUnitAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitAttribute.Plural"/>
    public string Plural { get; }
    /// <summary>The name of the instance to which the prefix is applied.</summary>
    public string From { get; }
    /// <summary>The metric prefix.</summary>
    public MetricPrefixName MetricPrefixName { get; }
    /// <summary>The binary prefix.</summary>
    public BinaryPrefixName BinaryPrefixName { get; }

    /// <summary>Defines an instance of a unit by applying a metric prefix to another instance of the same unit.</summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/><para><inheritdoc cref="From" path="/remarks"/></para></param>
    /// <param name="metricPrefixName"><inheritdoc cref="MetricPrefixName" path="/summary"/><para><inheritdoc cref="MetricPrefixName" path="/remarks"/></para></param>
    public PrefixedUnitAttribute(string name, string plural, string from, MetricPrefixName metricPrefixName)
    {
        Name = name;
        Plural = plural;
        From = from;
        MetricPrefixName = metricPrefixName;
    }

    /// <summary>Defines an instance of a unit by applying a binary prefix to another instance of the same unit.</summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/><para><inheritdoc cref="From" path="/remarks"/></para></param>
    /// <param name="binaryPrefixName"><inheritdoc cref="BinaryPrefixName" path="/summary"/><para><inheritdoc cref="BinaryPrefixName" path="/remarks"/></para></param>
    public PrefixedUnitAttribute(string name, string plural, string from, BinaryPrefixName binaryPrefixName)
    {
        Name = name;
        Plural = plural;
        From = from;
        BinaryPrefixName = binaryPrefixName;
    }
}
