namespace SharpMeasures.Generators.Units;

using System;

/// <summary>Defines an instance of a unit according to a bias relative to another instance of the same unit.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class BiasedUnitAttribute : Attribute
{
    /// <inheritdoc cref="FixedUnitAttribute.Name"/>
    public string Name { get; }
    /// <inheritdoc cref="FixedUnitAttribute.Plural"/>
    public string Plural { get; }
    /// <summary>The name of the original instance, relative to which this instance is biased.</summary>
    public string From { get; }
    /// <summary>The bias relative to the original instance. This is the value of this instance when the original instance is zero.</summary>
    /// <remarks>For example; if <i>Kelvin</i> is the original instance, <i>Celsius</i> should have a bias of { -273.15 }.</remarks>
    public double Bias { get; }
    /// <summary>An expression that computes the bias relative to the original instance. The computed value should represent value of this instance when the original
    /// instance is zero.</summary>
    /// <remarks><inheritdoc cref="Bias" path="/remarks"/></remarks>
    public string? Expression { get; }

    /// <inheritdoc cref="BiasedUnitAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/><para><inheritdoc cref="From" path="/remarks"/></para></param>
    /// <param name="bias"><inheritdoc cref="Bias" path="/summary"/><para><inheritdoc cref="Bias" path="/remarks"/></para></param>
    public BiasedUnitAttribute(string name, string plural, string from, double bias)
    {
        Name = name;
        Plural = plural;
        From = from;
        Bias = bias;
    }

    /// <summary><inheritdoc cref="BiasedUnitAttribute" path="/summary"/></summary>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="plural"><inheritdoc cref="Plural" path="/summary"/><para><inheritdoc cref="Plural" path="/remarks"/></para></param>
    /// <param name="from"><inheritdoc cref="From" path="/summary"/><para><inheritdoc cref="From" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <remarks><inheritdoc cref="BiasedUnitAttribute" path="/remarks"/>
    /// <para>When applicable, <see cref="BiasedUnitAttribute(string, string, string, double)"/> should be preferred.</para></remarks>
    public BiasedUnitAttribute(string name, string plural, string from, string expression)
    {
        Name = name;
        Plural = plural;
        From = from;
        Expression = expression;
    }
}
