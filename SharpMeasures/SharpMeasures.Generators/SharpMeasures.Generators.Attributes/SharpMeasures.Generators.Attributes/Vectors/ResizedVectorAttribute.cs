namespace SharpMeasures.Generators.Vectors;

using System;

/// <summary>Marks the type as a vector quantity associated with another vector quantity, but of another dimension.
/// <para> If SharpMeasures source generators are used, this attribute is also used to identify targets for generation.</para></summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ResizedVectorAttribute : Attribute
{
    /// <summary>The vector quantity that this quantity is associated with.</summary>
    public Type AssociatedVector { get; }

    /// <summary>The dimension of the vector quantity that this type represents.
    /// <para>If the name of the type ends with the dimension, this property can be ignored - such as for <i>Position3</i>.</para></summary>
    public int Dimension { get; init; }

    /// <summary>Dictates whether documentation should be generated for this quantity.</summary>
    /// <remarks>If this property is not explicitly set, the entry [<i>SharpMeasures_GenerateDocumentation</i>] in the global AnalyzerConfig
    /// file is used to determine whether documentation is generated - which in turn is <see langword="true"/> by default.</remarks>
    public bool GenerateDocumentation { get; init; }

    /// <summary>Marks the type as a vector quantity associated with another vector quantity, but of another dimension.
    /// <para> If SharpMeasures source generators are used, this attribute is also used to identify targets for generation.</para></summary>
    /// <param name="associatedVector">The vector quantity that this quantity is associated with.</param>
    public ResizedVectorAttribute(Type associatedVector)
    {
        AssociatedVector = associatedVector;
    }
}
