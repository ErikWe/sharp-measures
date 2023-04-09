namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit using a bias relative to another instance of the same unit.</summary>
/// <remarks>Defining a biased unit instance requires the unit to include a bias term.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class BiasedUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string? PluralForm { get; }

    /// <summary>The name of the original unit instance, relative to which this instance is biased.</summary>
    public string OriginalUnitInstance { get; }

    /// <summary>The bias relative to the original unit instance, representing the value of this unit instance when the original unit instance is { 0 }.</summary>
    /// <remarks>For example, if <i>Kelvin</i> is the original unit instance, <i>Celsius</i> should have a bias of { -273.15 }.</remarks>
    public double Bias { get; }

    /// <summary>An expression that computes the bias relative to the original unit instance. The resulting value should represent the value of this unit instance when the original unit instance is { 0 }.</summary>
    /// <remarks><inheritdoc cref="Bias" path="/remarks"/></remarks>
    public string? BiasExpression { get; }

    /// <inheritdoc cref="BiasedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="bias"><inheritdoc cref="Bias" path="/summary"/><para><inheritdoc cref="Bias" path="/remarks"/></para></param>
    public BiasedUnitInstanceAttribute(string name, string? pluralForm, string originalUnitInstance, double bias)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        Bias = bias;
    }

    /// <inheritdoc cref="BiasedUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="bias"><inheritdoc cref="Bias" path="/summary"/><para><inheritdoc cref="Bias" path="/remarks"/></para></param>
    public BiasedUnitInstanceAttribute(string name, string originalUnitInstance, double bias)
    {
        Name = name;
        OriginalUnitInstance = originalUnitInstance;
        Bias = bias;
    }

    /// <summary><inheritdoc cref="BiasedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="BiasExpression" path="/summary"/><para><inheritdoc cref="BiasExpression" path="/remarks"/></para></param>
    /// <remarks><inheritdoc cref="BiasedUnitInstanceAttribute" path="/remarks"/>
    /// <para>When applicable, prefer <see cref="BiasedUnitInstanceAttribute(string, string, string, double)"/>.</para></remarks>
    public BiasedUnitInstanceAttribute(string name, string? pluralForm, string originalUnitInstance, string expression)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        BiasExpression = expression;
    }

    /// <summary><inheritdoc cref="BiasedUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="BiasExpression" path="/summary"/><para><inheritdoc cref="BiasExpression" path="/remarks"/></para></param>
    /// <remarks><inheritdoc cref="BiasedUnitInstanceAttribute" path="/remarks"/>
    /// <para>When applicable, prefer <see cref="BiasedUnitInstanceAttribute(string, string, double)"/>.</para></remarks>
    public BiasedUnitInstanceAttribute(string name, string originalUnitInstance, string expression)
    {
        Name = name;
        OriginalUnitInstance = originalUnitInstance;
        BiasExpression = expression;
    }
}
