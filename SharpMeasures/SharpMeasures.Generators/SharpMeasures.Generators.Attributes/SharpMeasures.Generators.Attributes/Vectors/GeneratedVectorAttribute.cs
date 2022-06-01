namespace SharpMeasures.Generators.Vectors;

using System;

/// <summary>Marks the type as a vector quantity, and allows a source generator to implement relevant functionality.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedVectorAttribute : Attribute
{
    /// <summary>The unit that describes the scalar quantity.</summary>
    public Type Unit { get; }

    /// <summary>The quantity that is considered the "scalar version" of the vector, if one exists. This is often the scalar that describes the
    /// magnitude of the vector.</summary>
    /// <remarks>For example, <i>Speed</i> is considered the scalar for <i>Velocity</i>.
    /// <para>There may be multiple such quantities, in which case the most similar or most fundamental quantity should be used.</para></remarks>
    public Type? Scalar { get; init; }

    /// <summary>The dimension of the vector quantity that this type represents.
    /// <para>If the name of the type ends with the dimension this property can be ignored - such as for <i>Position3</i>.</para></summary>
    public int Dimension { get; init; }

    /// <summary>The name of the default unit.</summary>
    /// <remarks>This is used by ToString(), together with <see cref="DefaultUnitSymbol"/>.</remarks>
    public string? DefaultUnitName { get; init; }

    /// <summary>The symbol of the default unit.</summary>
    /// <remarks>This is used by ToString(), together with <see cref="DefaultUnitName"/>.</remarks>
    public string? DefaultUnitSymbol { get; init; }

    /// <summary>Dictates whether documentation should be generated for this quantity.</summary>
    /// <remarks>If this property is not explicitly set, the entry [<i>SharpMeasures_GenerateDocumentation</i>] in the global AnalyzerConfig
    /// file is used to determine whether documentation is generated - which in turn is <see langword="true"/> by default.</remarks>
    public bool GenerateDocumentation { get; init; }

    /// <summary>Marks the type as a vector quantity, and allows a source generator to implement relevant functionality.</summary>
    /// <param name="unit">The unit that describes the vector quantity.</param>
    public GeneratedVectorAttribute(Type unit)
    {
        Unit = unit;
    }
}
