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

    /// <summary>Determines whether to generate a property describing the components of the vector in terms of multiples of this constant. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>If <see langword="true"/>, <see cref="Multiples"/> is used to determine the name of the property.</remarks>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>The name describing multiples of this constant. This name must differ from the name of the constant itself. The default behaviour is prepending "MultiplesOf" to the name of the constant.
    /// <para>If <see cref="MultiplesRegexSubstitution"/> is set, this value is used as a .NET regex pattern. Alternatively, see <see cref="CommonPluralNotation"/> for some short-hand notations.</para></summary>
    public string Multiples { get; init; } = CommonPluralNotation.PrependMultiplesOf;

    /// <summary>Used as the .NET Regex substitution string when producing the name describing multiples of this constant, with <see cref="Multiples"/> being used as the .NET regex pattern.</summary>
    /// <remarks>If this property is ignored, <see cref="Multiples"/> will not be used as a .NET regex pattern.</remarks>
    public string MultiplesRegexSubstitution { get; init; } = string.Empty;

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
