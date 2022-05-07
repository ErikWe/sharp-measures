namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the type as a scalar quantity, and enables source generation.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GeneratedScalarQuantityAttribute : Attribute
{
    /// <summary>The unit that describes the scalar quantity.</summary>
    public Type Unit { get; }

    /// <summary>Dictates whether this quantity requires a bias term.</summary>
    public bool Biased { get; init; }

    /// <summary>Dictates whether documentation should be generated for this quantity.</summary>
    /// <remarks>If this property is not explicitly set, the global AnalyzerConfig file is used to determine whether documentation is generated -
    /// which in turn is <see langword="true"/> by default.</remarks>
    public bool GenerateDocumentation { get; init; }

    /// <summary>Marks the type as a scalar quantity, and enables source generation.</summary>
    /// <param name="unit">The unit that describes the scalar quantity.</param>
    public GeneratedScalarQuantityAttribute(Type unit)
    {
        Unit = unit;
    }
}
