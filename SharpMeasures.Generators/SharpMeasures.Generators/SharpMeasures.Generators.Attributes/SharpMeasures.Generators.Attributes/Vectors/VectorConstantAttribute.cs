﻿namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities.Utility;

using System;

/// <summary>Defines a constant of the vector quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class VectorConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }
    /// <summary>The name of the unit in which the value is specified.</summary>
    public string Unit { get; }
    /// <summary>The value of the constant, in the specified unit.</summary>
    /// <remarks>The number of elements should match the dimension of the vector.</remarks>
    public double[] Value { get; }

    /// <summary>Determines whether to generate a property for retrieving the magnitude of the scalar in terms of multiples of this constant. The
    /// default behaviour is <see langword="true"/>.</summary>
    /// <remarks>If set to <see langword="true"/>, <see cref="Multiples"/> is used to determine the name of the property.</remarks>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>If <see cref="GenerateMultiplesProperty"/> is set to <see langword="true"/>, this value is used as the name of the property.
    /// <para>The default behaviour is prepending "InMultiplesOf" to the name of the constant.</para></summary>
    /// <remarks>See <see cref="ConstantMultiplesCodes"/> for some short-hand notations for producing this name based on the name of the constant.</remarks>
    public string Multiples { get; init; } = ConstantMultiplesCodes.InMultiplesOfConstant;

    /// <summary>Defines a constant.</summary>
    /// <param name="name">The name of the constant.</param>
    /// <param name="unit">The name of the unit in which the value was specified.</param>
    /// <param name="value">The value of the constant, in the specified unit.
    /// <para>The number of elements should match the dimension of the vector.</para></param>
    public VectorConstantAttribute(string name, string unit, params double[] value)
    {
        Name = name;
        Unit = unit;
        Value = value;
    }
}
