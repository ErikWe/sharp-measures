namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit by applying a prefix to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class PrefixedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string? PluralForm { get; }

    /// <summary>The name of the unit instance to which the prefix is applied.</summary>
    public string OriginalUnitInstance { get; }

    /// <summary>The name of the metric prefix that is applied to the original unit instance.</summary>
    public MetricPrefixName MetricPrefix { get; }

    /// <summary>The name of the binary prefix that is applied to the original unit instance.</summary>
    public BinaryPrefixName BinaryPrefix { get; }

    /// <inheritdoc cref="PrefixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="metricPrefix"><inheritdoc cref="MetricPrefix" path="/summary"/></param>
    public PrefixedUnitInstanceAttribute(string name, string? pluralForm, string originalUnitInstance, MetricPrefixName metricPrefix)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        MetricPrefix = metricPrefix;
    }

    /// <inheritdoc cref="PrefixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="metricPrefix"><inheritdoc cref="MetricPrefix" path="/summary"/></param>
    public PrefixedUnitInstanceAttribute(string name, string originalUnitInstance, MetricPrefixName metricPrefix)
    {
        Name = name;
        OriginalUnitInstance = originalUnitInstance;
        MetricPrefix = metricPrefix;
    }

    /// <inheritdoc cref="PrefixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="binaryPrefix"><inheritdoc cref="BinaryPrefix" path="/summary"/></param>
    public PrefixedUnitInstanceAttribute(string name, string? pluralForm, string originalUnitInstance, BinaryPrefixName binaryPrefix)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        BinaryPrefix = binaryPrefix;
    }

    /// <inheritdoc cref="PrefixedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="binaryPrefix"><inheritdoc cref="BinaryPrefix" path="/summary"/></param>
    public PrefixedUnitInstanceAttribute(string name, string originalUnitInstance, BinaryPrefixName binaryPrefix)
    {
        Name = name;
        OriginalUnitInstance = originalUnitInstance;
        BinaryPrefix = binaryPrefix;
    }
}
