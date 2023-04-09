namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures vector quantities, defining a constant value of the quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class VectorConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public string UnitInstanceName { get; }

    /// <summary>The value of the constant, when expressed in the provided unit.</summary>
    public double[]? Value { get; }

    /// <summary>The expressions used to compute the value of the constant, when expressed in the provided unit.</summary>
    public string[]? Expressions { get; }

    /// <inheritdoc cref="VectorConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/></param>
    /// <param name="value"><inheritdoc cref="Value" path="/summary"/></param>
    public VectorConstantAttribute(string name, string unitInstanceName, params double[] value)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Value = value;
    }

    /// <inheritdoc cref="VectorConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/></param>
    /// <param name="expressions"><inheritdoc cref="Expressions" path="/summary"/></param>
    public VectorConstantAttribute(string name, string unitInstanceName, params string[] expressions)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Expressions = expressions;
    }
}
