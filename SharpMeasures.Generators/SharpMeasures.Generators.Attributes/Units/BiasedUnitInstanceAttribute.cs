namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit according to a bias relative to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class BiasedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <summary><inheritdoc cref="FixedUnitInstanceAttribute.PluralForm" path="/summary"/></summary>
    /// <remarks>If <see cref="PluralFormRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotations"/> for some common notations for producing the plural form based on the singular form.</remarks>
    public string PluralForm { get; }
    /// <summary>The name of the original instance, relative to which this instance is biased.</summary>
    public string OriginalUnitInstance { get; }
    /// <summary>The bias relative to the original instance. This is the value of this instance when the original instance is zero.</summary>
    /// <remarks>For example; if <i>Kelvin</i> is the original instance, <i>Celsius</i> should have a bias of { -273.15 }.</remarks>
    public double Bias { get; }
    /// <summary>An expression that computes the bias relative to the original instance. The computed value should represent value of this instance when the original
    /// instance is zero.</summary>
    /// <remarks><inheritdoc cref="Bias" path="/remarks"/></remarks>
    public string? Expression { get; }

    /// <summary>Used as the .NET Regex substitution string when producing the plural form of the unit, with <see cref="PluralForm"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="PluralForm"/> will not be used as a .NET regex pattern.</remarks>
    public string PluralFormRegexSubstitution { get; init; } = string.Empty;

    /// <inheritdoc cref="BiasedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/><para><inheritdoc cref="OriginalUnitInstance" path="/remarks"/></para></param>
    /// <param name="bias"><inheritdoc cref="Bias" path="/summary"/><para><inheritdoc cref="Bias" path="/remarks"/></para></param>
    public BiasedUnitInstanceAttribute(string name, string pluralForm, string originalUnitInstance, double bias)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        Bias = bias;
    }

    /// <summary><inheritdoc cref="BiasedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/><para><inheritdoc cref="OriginalUnitInstance" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <remarks><inheritdoc cref="BiasedUnitInstanceAttribute" path="/remarks"/>
    /// <para>When applicable, <see cref="BiasedUnitInstanceAttribute(string, string, string, double)"/> should be preferred.</para></remarks>
    public BiasedUnitInstanceAttribute(string name, string pluralForm, string originalUnitInstance, string expression)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        Expression = expression;
    }
}
