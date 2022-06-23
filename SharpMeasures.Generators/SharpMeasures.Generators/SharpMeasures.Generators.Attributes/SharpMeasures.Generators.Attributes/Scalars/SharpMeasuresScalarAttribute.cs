namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Vectors;

using System;

/// <summary>Marks the type as a scalar quantity, and allows a source generator to implement relevant functionality.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SharpMeasuresScalarAttribute : Attribute
{
    /// <summary>The unit that describes this quantity.</summary>
    public Type Unit { get; }

    /// <summary>The quantity that is considered the "vector version" of this scalar quantity, if one exists. This is often the vector for which
    /// this scalar represents the magnitude. For example, <i>Velocity</i> could be considered the "vector version" of <i>Speed</i>.</summary>
    /// <remarks>There may be multiple such quantities, in which case the most similar or most fundamental quantity should be used.
    /// <para>If the vector quantity is associated with multiple types representing different dimensions, using <see cref="ResizedSharpMeasuresVectorAttribute"/>,
    /// any of the associated types can be specified.</para></remarks>
    public Type? Vector { get; init; }

    /// <summary>Dictates whether unit biases should be considered when computing the magnitude. This requires the specified unit to include a bias term.
    /// The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>For example; <i>Temperature</i> would require access to a bias term to be able to express both <i>Celsius</i> and <i>Fahrenheit</i>, while
    /// <i>TemperatureDifference</i> would explicitly deny access to the bias term.</remarks>
    public bool UseUnitBias { get; init; }

    /// <summary>Dictates whether to implement support for computing the sum of two instances of this scalar. The default behaviour is <see langword="true"/>.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether to implement support for computing the difference between two instances of this scalar. The default behaviour is
    /// <see langword="true"/>.</summary>
    /// <remarks>To specify the scalar quantity that represents the difference, use <see cref="Difference"/>. By default, the same quantity is used as the
    /// difference between two instance.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The scalar quantity that is considered the difference between two instances of this scalar. By default, and when set to <see langword="null"/>, the
    /// same quantity is used.</summary>
    /// <remarks>To disable support for computing the difference in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary>The name of the default unit.</summary>
    /// <remarks>This is used by ToString(), together with <see cref="DefaultUnitSymbol"/>.</remarks>
    public string? DefaultUnitName { get; init; }

    /// <summary>The symbol of the default unit.</summary>
    /// <remarks>This is used by ToString(), together with <see cref="DefaultUnitName"/>.</remarks>
    public string? DefaultUnitSymbol { get; init; }

    /// <summary>The scalar quantity that is considered the reciprocal, or inverse, of this quantity, if one exists. The specified type should be decorated
    /// with <see cref="SharpMeasuresScalarAttribute"/>.</summary>
    public Type? Reciprocal { get; init; }
    /// <summary>The scalar quantity that is considered the square of this quantity, if one exists. The specified type should be decorated
    /// with <see cref="SharpMeasuresScalarAttribute"/>.</summary>
    public Type? Square { get; init; }
    /// <summary>The scalar quantity that is considered the cube of this quantity, if one exists. The specified type should be decorated
    /// with <see cref="SharpMeasuresScalarAttribute"/>.</summary>
    public Type? Cube { get; init; }
    /// <summary>The scalar quantity that is considered the square root of this quantity, if one exists. The specified type should be decorated
    /// with <see cref="SharpMeasuresScalarAttribute"/>.</summary>
    public Type? SquareRoot { get; init; }
    /// <summary>The scalar quantity that is considered the cube root of this quantity, if one exists. The specified type should be decorated
    /// with <see cref="SharpMeasuresScalarAttribute"/>.</summary>
    public Type? CubeRoot { get; init; }

    /// <summary>Dictates whether documentation should be generated for this quantity.</summary>
    /// <remarks>If this property is not explicitly set, the entry [<i>SharpMeasures_GenerateDocumentation</i>] in the global AnalyzerConfig
    /// file is used to determine whether documentation is generated - which in turn is <see langword="true"/> by default.</remarks>
    public bool GenerateDocumentation { get; init; }

    /// <summary>Marks the type as a scalar quantity, and allows a source generator to implement relevant functionality.</summary>
    /// <param name="unit">The unit that describes the scalar quantity.</param>
    public SharpMeasuresScalarAttribute(Type unit)
    {
        Unit = unit;
    }
}
