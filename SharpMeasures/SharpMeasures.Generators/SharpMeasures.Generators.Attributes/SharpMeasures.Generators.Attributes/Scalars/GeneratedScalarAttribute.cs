﻿namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the type as a scalar quantity, and allows a source generator to implement relevant functionality.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedScalarAttribute : Attribute
{
    /// <summary>The unit that describes the scalar quantity.</summary>
    public Type Unit { get; }

    /// <summary>The quantity that is considered the "vector version" of the scalar, if one exists. This is often the vector for which
    /// this scalar represents the magnitude.</summary>
    /// <remarks>For example, <i>Velocity</i> could be considered the "vector version" of <i>Speed</i>.
    /// <para>There may be multiple such quantities, in which case the most similar or most fundamental quantity should be used.</para>
    /// <para>If the vector quantity is associated with multiple types representing different dimensions, any of the associated types can be specified.</para></remarks>
    public Type? Vector { get; init; }

    /// <summary>Dictates whether this quantity requires a bias term.</summary>
    public bool Biased { get; init; }

    /// <summary>The name of the default unit.</summary>
    /// <remarks>This is used by ToString(), together with <see cref="DefaultUnitSymbol"/>.</remarks>
    public string? DefaultUnitName { get; init; }

    /// <summary>The symbol of the default unit.</summary>
    /// <remarks>This is used by ToString(), together with <see cref="DefaultUnitName"/>.</remarks>
    public string? DefaultUnitSymbol { get; init; }

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

    /// <summary>Marks the type as a scalar quantity, and allows a source generator to implement relevant functionality.</summary>
    /// <param name="unit">The unit that describes the scalar quantity.</param>
    public GeneratedScalarAttribute(Type unit)
    {
        Unit = unit;
    }
}