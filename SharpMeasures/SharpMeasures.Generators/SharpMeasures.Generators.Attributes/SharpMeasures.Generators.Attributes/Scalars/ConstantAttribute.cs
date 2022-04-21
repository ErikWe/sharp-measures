namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Defines a constant of the scalar quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }
    /// <summary>The value of the constant, in the specified unit.</summary>
    public double Value { get; }
    /// <summary>The name of the unit in which the value was specified.</summary>
    public string Unit { get; }

    /// <summary>Defines a constant.</summary>
    /// <param name="name">The name of the constant.</param>
    /// <param name="value">The value of the constnat, in the specified unit.</param>
    /// <param name="unit">The name of the unit in which the value was specified.</param>
    public ConstantAttribute(string name, double value, string unit)
    {
        Name = name;
        Value = value;
        Unit = unit;
    }
}
