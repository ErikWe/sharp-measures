namespace SharpMeasures.Generators.Vectors;

using System;

/// <summary>Defines a constant of a vector quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class VectorConstantAttribute : Attribute
{
    /// <summary>The name of the constant.</summary>
    public string Name { get; }
    /// <summary>The name of the unit instance in which the value is specified.</summary>
    public string UnitInstanceName { get; }
    /// <summary>The value of the constant, in the specified unit.</summary>
    /// <remarks>The number of elements should match the dimension of the vector.</remarks>
    public double[] Value { get; }

    /// <summary>Determines whether to generate a property for retrieving the magnitude of the scalar in terms of multiples of this constant. The
    /// default behaviour is <see langword="true"/>.</summary>
    /// <remarks>If set to <see langword="true"/>, <see cref="Multiples"/> is used to determine the name of the property.</remarks>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>If <see cref="GenerateMultiplesProperty"/> is set to <see langword="true"/>, this value is used as the name of the property.
    /// <para>The default behaviour is prepending "InMultiplesOf" to the name of the constant.</para></summary>
    /// <remarks>See <see cref="CommonConstantMultiplesPropertyNotations"/> or <see cref="CommonPluralNotations"/> for some short-hand notations for producing
    /// this name based on the singular name of the constant.</remarks>
    public string Multiples { get; init; } = CommonConstantMultiplesPropertyNotations.PrependInMultiplesOf;

    /// <inheritdoc cref="VectorConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="unitInstanceName"><inheritdoc cref="UnitInstanceName" path="/summary"/><para><inheritdoc cref="UnitInstanceName" path="/remarks"/></para></param>
    /// <param name="value"><inheritdoc cref="Value" path="/summary"/><para><inheritdoc cref="Value" path="/remarks"/></para></param>
    public VectorConstantAttribute(string name, string unitInstanceName, params double[] value)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        Value = value;
    }
}
