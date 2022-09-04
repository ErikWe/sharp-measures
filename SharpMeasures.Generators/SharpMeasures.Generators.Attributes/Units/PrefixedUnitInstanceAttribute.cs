namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit by applying a prefix to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class PrefixedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string PluralForm { get; }
    /// <summary>The name of the instance to which the prefix is applied.</summary>
    public string OriginalUnitInstance { get; }
    /// <summary>The metric prefix.</summary>
    public MetricPrefixName MetricPrefix { get; }
    /// <summary>The binary prefix.</summary>
    public BinaryPrefixName BinaryPrefix { get; }

    /// <summary>Defines an instance of a unit by applying a metric prefix to another instance of the same unit.</summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/><para><inheritdoc cref="OriginalUnitInstance" path="/remarks"/></para></param>
    /// <param name="metricPrefix"><inheritdoc cref="MetricPrefix" path="/summary"/><para><inheritdoc cref="MetricPrefix" path="/remarks"/></para></param>
    public PrefixedUnitInstanceAttribute(string name, string pluralForm, string originalUnitInstance, MetricPrefixName metricPrefix)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        MetricPrefix = metricPrefix;
    }

    /// <summary>Defines an instance of a unit by applying a binary prefix to another instance of the same unit.</summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/><para><inheritdoc cref="OriginalUnitInstance" path="/remarks"/></para></param>
    /// <param name="binaryPrefix"><inheritdoc cref="BinaryPrefix" path="/summary"/><para><inheritdoc cref="BinaryPrefix" path="/remarks"/></para></param>
    public PrefixedUnitInstanceAttribute(string name, string pluralForm, string originalUnitInstance, BinaryPrefixName binaryPrefix)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        BinaryPrefix = binaryPrefix;
    }
}
