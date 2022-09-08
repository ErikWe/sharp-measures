namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit by applying a prefix to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class PrefixedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <summary><inheritdoc cref="FixedUnitInstanceAttribute.PluralForm" path="/summary"/></summary>
    /// <remarks>If <see cref="PluralFormRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotation"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string PluralForm { get; }
    /// <summary>The name of the instance to which the prefix is applied.</summary>
    public string OriginalUnitInstance { get; }
    /// <summary>The metric prefix.</summary>
    public MetricPrefixName MetricPrefix { get; }
    /// <summary>The binary prefix.</summary>
    public BinaryPrefixName BinaryPrefix { get; }

    /// <summary>Used as the .NET Regex substitution string when producing the plural form of the unit, with <see cref="PluralForm"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="PluralForm"/> will not be used as a .NET regex pattern.</remarks>
    public string PluralFormRegexSubstitution { get; init; } = string.Empty;

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
