namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit by scaling another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScaledUnitAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitAttribute.Plural"/>
    public string Plural { get; }
    /// <summary>The name of the instance that is scaled.</summary>
    public string From { get; }
    /// <summary>The value by which the original instance is scaled.</summary>
    public double Scale { get; }
    /// <summary>An expression that computes the value by which the original instance is scaled.</summary>
    public string? Expression { get; }

    /// <inheritdoc cref="ScaledUnitAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/><para><inheritdoc cref="From" path="/remarks"/></para></param>
    /// <param name="scale"><inheritdoc cref="Scale" path="/summary"/><para><inheritdoc cref="Scale" path="/remarks"/></para></param>
    public ScaledUnitAttribute(string name, string plural, string from, double scale)
    {
        Name = name;
        Plural = plural;
        From = from;
        Scale = scale;
    }

    /// <summary><inheritdoc cref="ScaledUnitAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/><para><inheritdoc cref="From" path="/remarks"/></para></param>
    /// <param name="expression">An expression that computes the value by which the original instance is scaled.</param>
    /// <remarks><inheritdoc cref="ScaledUnitAttribute" path="/remarks"/>
    /// <para>When applicable, <see cref="ScaledUnitAttribute(string, string, string, double)"/> should be preferred.</para></remarks>
    public ScaledUnitAttribute(string name, string plural, string from, string expression)
    {
        Name = name;
        Plural = plural;
        From = from;
        Expression = expression;
    }
}
