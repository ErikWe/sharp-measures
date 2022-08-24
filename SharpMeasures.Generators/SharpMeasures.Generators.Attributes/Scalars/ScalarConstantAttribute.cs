namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Utility;

using System;

/// <summary>Defines a constant of a scalar quantity.</summary>
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
    /// <remarks>If set to <see langword="true"/>, <see cref="Multiples"/> is used to determine the name of the property.</remarks>
    public bool GenerateMultiplesProperty { get; init; } = true;

    /// <summary>If <see cref="GenerateMultiplesProperty"/> is set to <see langword="true"/>, this value is used as the name of the property. The name of this
    /// property must differ from the name of the constant.
    /// <para>The default behaviour is prepending "MultiplesOf" to the name of the constant.</para></summary>
    /// <remarks>See <see cref="CommonConstantMultiplesPropertyNotations"/> or <see cref="CommonPluralNotations"/> for some short-hand notations for producing this
    /// name based on the singular name of the constant.</remarks>
    public string Multiples { get; init; } = CommonConstantMultiplesPropertyNotations.PrependMultiplesOf;

    /// <inheritdoc cref="ScalarConstantAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    /// <param name="value"><inheritdoc cref="Value" path="/summary"/><para><inheritdoc cref="Value" path="/remarks"/></para></param>
    public ScalarConstantAttribute(string name, string unit, double value)
    {
        Name = name;
        Unit = unit;
        Value = value;
    }
}
