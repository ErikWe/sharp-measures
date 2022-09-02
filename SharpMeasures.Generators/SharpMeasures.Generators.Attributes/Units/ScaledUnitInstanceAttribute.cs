namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit by scaling another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScaledUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string PluralForm { get; }
    /// <summary>The name of the instance that is scaled.</summary>
    public string OriginalUnitInstance { get; }
    /// <summary>The value by which the original instance is scaled.</summary>
    public double Scale { get; }
    /// <summary>An expression that computes the value by which the original instance is scaled.</summary>
    public string? Expression { get; }

    /// <inheritdoc cref="ScaledUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/><para><inheritdoc cref="OriginalUnitInstance" path="/remarks"/></para></param>
    /// <param name="scale"><inheritdoc cref="Scale" path="/summary"/><para><inheritdoc cref="Scale" path="/remarks"/></para></param>
    public ScaledUnitInstanceAttribute(string name, string pluralForm, string originalUnitInstance, double scale)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        Scale = scale;
    }

    /// <summary><inheritdoc cref="ScaledUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/><para><inheritdoc cref="PluralForm" path="/remarks"/></para></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/><para><inheritdoc cref="OriginalUnitInstance" path="/remarks"/></para></param>
    /// <param name="expression">An expression that computes the value by which the original instance is scaled.</param>
    /// <remarks><inheritdoc cref="ScaledUnitInstanceAttribute" path="/remarks"/>
    /// <para>When applicable, <see cref="ScaledUnitInstanceAttribute(string, string, string, double)"/> should be preferred.</para></remarks>
    public ScaledUnitInstanceAttribute(string name, string pluralForm, string originalUnitInstance, string expression)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        Expression = expression;
    }
}
