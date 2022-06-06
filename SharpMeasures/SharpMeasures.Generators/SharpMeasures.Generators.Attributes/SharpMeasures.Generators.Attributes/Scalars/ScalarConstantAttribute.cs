namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities.Utility;

using System;

/// <summary>Defines a constant of the scalar quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ScalarConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }
    /// <summary>The name of the unit in which the value is specified.</summary>
    public string Unit { get; }
    /// <summary>The value of the constant, in the specified unit.</summary>
    public double Value { get; }

    /// <summary>Determines whether to generate a property for retrieving the magnitude of the scalar in terms of multiples of this constant. The
    /// default behaviour is <see langword="true"/>.</summary>
    /// <remarks>If set to <see langword="true"/>, <see cref="MultiplesName"/> is used to determine the name of the property.</remarks>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>If <see cref="GenerateMultiplesProperty"/> is set to <see langword="true"/>, this value is used as the name of the property.
    /// <para>The default behaviour is prepending "InMultiplesOf" to the name of the constant.</para></summary>
    /// <remarks>See <see cref="ConstantMultiplesCodes"/> for some short-hand notations for producing this name based on the name of the constant.</remarks>
    public string MultiplesName { get; init; } = ConstantMultiplesCodes.InMultiplesOfConstant;

    /// <summary>Defines a constant.</summary>
    /// <param name="name">The name of the constant.</param>
    /// <param name="unit">The name of the unit in which the value was specified.</param>
    /// <param name="value">The value of the constant, in the specified unit.</param>
    public ScalarConstantAttribute(string name, string unit, double value)
    {
        Name = name;
        Unit = unit;
        Value = value;
    }
}
