namespace SharpMeasures.Generators;

using System;

/// <summary>Defines a constant of the scalar quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScalarConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }
    /// <summary>The name of the unit instance in which <see cref="Value"/> or <see cref="Expression"/> is expressed.</summary>
    public string UnitInstanceName { get; }
    /// <summary>The value of the constant, when expressed in the unit instance described by <see cref="UnitInstanceName"/>.</summary>
    public double Value { get; }
    /// <summary>An expression that computes the value of the constant, when expressed in the unit instance described by <see cref="UnitInstanceName"/>.</summary>
    public string? Expression { get; }

    /// <summary>Determines whether to generate a property describing the magnitude of the scalar in terms of multiples of this constant. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>If <see langword="true"/>, <see cref="Multiples"/> is used to determine the name of the property.</remarks>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>The name describing multiples of this constant. This name must differ from the name of the constant itself. The default behaviour is prepending { MultiplesOf}  to the name of the constant.
    /// <para>If <see cref="MultiplesRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotation"/> for some short-hand notations.</para></summary>
    public string Multiples { get; init; } = CommonPluralNotation.PrependMultiplesOf;

    /// <summary>Used as the .NET Regex substitution string when producing the name describing multiples of this constant, with <see cref="Multiples"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="Multiples"/> will not be used as a .NET regex pattern.</remarks>
    public string MultiplesRegexSubstitution { get; init; } = string.Empty;

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/><para><inheritdoc cref="UnitInstanceName" path="/remarks"/></para></param>
    /// <param name="value"><inheritdoc cref="Value" path="/summary"/><para><inheritdoc cref="Value" path="/remarks"/></para></param>
    public ScalarConstantAttribute(string name, string unitInstanceName, double value)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Value = value;
    }

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/><para><inheritdoc cref="UnitInstanceName" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    public ScalarConstantAttribute(string name, string unitInstanceName, string expression)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Expression = expression;
    }
}
