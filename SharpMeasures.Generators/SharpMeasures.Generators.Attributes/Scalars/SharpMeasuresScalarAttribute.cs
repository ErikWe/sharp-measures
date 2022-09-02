namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System;

/// <summary>Marks the type as a scalar quantity.</summary>
/// <remarks>The following attributes may be used to modify how the scalar is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="DerivedQuantityAttribute"/></term>
/// <description>Describes how the scalar may be derived from other quantities.</description>
/// </item>
/// <item>
/// <term><see cref="ScalarConstantAttribute"/></term>
/// <description>Defines a constant of the scalar.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitBasesAttribute"/></term>
/// <description>Dictates the units for which a static property representing the value { 1 } is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="ExcludeUnitBasesAttribute"/></term>
/// <description>Dictates the units for which a static property representing the value { 1 } is <i>not</i> implemented.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/></term>
/// <description>Dictates the units for which a property representing the magnitude is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the units for which a property representing the magnitude is <i>not</i> implemented.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other scalars that this scalar may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SharpMeasuresScalarAttribute : Attribute
{
    /// <summary>The unit that describes this quantity.</summary>
    public Type Unit { get; }

    /// <summary>Dictates whether bias terms of the unit should be considered when computing the magnitude. This requires the specified unit to include a bias term.
    /// The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>For example; <i>Temperature</i> would require access to a bias term to be able to express both <i>Celsius</i> and <i>Fahrenheit</i>, while
    /// <i>TemperatureDifference</i> would ignore any bias terms.</remarks>
    public bool UseUnitBias { get; init; }

    /// <summary>The vector quantity that is associated with this scalar quantity, if one exists. This is often the vector for which
    /// this scalar represents the magnitude.</summary>
    /// <remarks>For example; <i>Velocity</i> could be considered the vector associated with <i>Speed</i>.</remarks>
    public Type? Vector { get; init; }

    /// <summary>Dictates whether to implement support for computing the sum of two instances of this scalar. The default behaviour is <see langword="true"/>.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether to implement support for computing the difference between two instances of this scalar. The default behaviour is
    /// <see langword="true"/>.</summary>
    /// <remarks>To specify the scalar quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The scalar quantity that is considered the difference between two instances of this scalar. By default, and when <see langword="null"/>, the
    /// quantity itself is used to describe the difference.</summary>
    /// <remarks>To disable support for computing the difference in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary>The name of the default unit instance.</summary>
    public string? DefaultUnitInstanceName { get; init; }

    /// <summary>The symbol of the default unit instance.</summary>
    public string? DefaultUnitInstanceSymbol { get; init; }

    /// <summary>The scalar quantity that is considered the reciprocal, or inverse, of this quantity, if one exists.</summary>
    public Type? Reciprocal { get; init; }
    /// <summary>The scalar quantity that is considered the square of this quantity, if one exists.</summary>
    public Type? Square { get; init; }
    /// <summary>The scalar quantity that is considered the cube of this quantity, if one exists.</summary>
    public Type? Cube { get; init; }
    /// <summary>The scalar quantity that is considered the square root of this quantity, if one exists.</summary>
    public Type? SquareRoot { get; init; }
    /// <summary>The scalar quantity that is considered the cube root of this quantity, if one exists.</summary>
    public Type? CubeRoot { get; init; }

    /// <summary>Dictates whether documentation should be generated for this quantity.</summary>
    /// <remarks>If this property is not explicitly set, the entry [<i>SharpMeasures_GenerateDocumentation</i>] in the global AnalyzerConfig
    /// file is used to determine whether documentation is generated - which in turn is <see langword="true"/> by default.</remarks>
    public bool GenerateDocumentation { get; init; }

    /// <inheritdoc cref="SharpMeasuresScalarAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    public SharpMeasuresScalarAttribute(Type unit)
    {
        Unit = unit;
    }
}
