namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, defining a constant value of the quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScalarConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public string UnitInstanceName { get; }

    /// <summary>The value of the constant, when expressed in the provided unit.</summary>
    public double Value { get; }

    /// <summary>The expression used to compute the value of the constant, when expressed in the provided unit.</summary>
    public string? Expression { get; }

    /// <summary>Determines whether to implement a property describing the magnitude of the scalar in terms of multiples of this constant. The default behaviour is <see langword="true"/>.</summary>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>The name describing multiples of this constant - or <see langword="null"/> if the associated property is not implemented. This name must differ from the name of the constant itself.</summary>
    public string? MultiplesName { get; init; }

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/></param>
    /// <param name="value"><inheritdoc cref="Value" path="/summary"/></param>
    public ScalarConstantAttribute(string name, string unitInstanceName, double value)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Value = value;
    }

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    /// <remarks>When applicable, prefer <see cref="ScalarConstantAttribute(string, string, double)"/>.</remarks>
    public ScalarConstantAttribute(string name, string unitInstanceName, string expression)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Expression = expression;
    }
}
