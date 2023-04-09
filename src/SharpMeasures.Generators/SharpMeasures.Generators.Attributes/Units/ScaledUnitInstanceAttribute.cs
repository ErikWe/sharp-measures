namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, defining an instance of the unit by scaling another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScaledUnitInstanceAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitInstanceAttribute.Name"/>
    public string Name { get; }

    /// <inheritdoc cref="FixedUnitInstanceAttribute.PluralForm"/>
    public string? PluralForm { get; }

    /// <summary>The name of the unit instance that is scaled.</summary>
    public string OriginalUnitInstance { get; }

    /// <summary>The value by which the original unit instance is scaled.</summary>
    public double Scale { get; }

    /// <summary>The expression used to compute the value by which the original unit instance is scaled.</summary>
    public string? ScaleExpression { get; }

    /// <inheritdoc cref="ScaledUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="scale"><inheritdoc cref="Scale" path="/summary"/></param>
    public ScaledUnitInstanceAttribute(string name, string? pluralForm, string originalUnitInstance, double scale)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        Scale = scale;
    }

    /// <inheritdoc cref="ScaledUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="scale"><inheritdoc cref="Scale" path="/summary"/></param>
    public ScaledUnitInstanceAttribute(string name, string originalUnitInstance, double scale)
    {
        Name = name;
        OriginalUnitInstance = originalUnitInstance;
        Scale = scale;
    }

    /// <summary><inheritdoc cref="ScaledUnitInstanceAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="scaleExpression"><inheritdoc cref="ScaleExpression" path="/summary"/></param>
    /// <remarks>When applicable, prefer <see cref="ScaledUnitInstanceAttribute(string, string, string, double)"/>.</remarks>
    public ScaledUnitInstanceAttribute(string name, string? pluralForm, string originalUnitInstance, string scaleExpression)
    {
        Name = name;
        PluralForm = pluralForm;
        OriginalUnitInstance = originalUnitInstance;
        ScaleExpression = scaleExpression;
    }

    /// <inheritdoc cref="ScaledUnitInstanceAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="OriginalUnitInstance" path="/summary"/></param>
    /// <param name="scaleExpression"><inheritdoc cref="ScaleExpression" path="/summary"/></param>
    /// <remarks>When applicable, prefer <see cref="ScaledUnitInstanceAttribute(string, string, double)"/>.</remarks>
    public ScaledUnitInstanceAttribute(string name, string originalUnitInstance, string scaleExpression)
    {
        Name = name;
        OriginalUnitInstance = originalUnitInstance;
        ScaleExpression = scaleExpression;
    }
}
