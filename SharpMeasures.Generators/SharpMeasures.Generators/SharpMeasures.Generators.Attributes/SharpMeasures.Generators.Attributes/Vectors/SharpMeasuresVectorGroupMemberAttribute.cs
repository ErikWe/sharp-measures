namespace SharpMeasures.Generators.Vectors;

using System;

/// <summary>Marks the type as a member of a vector group. The vector group should be defined using <see cref="SharpMeasuresVectorGroupAttribute"/>.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SharpMeasuresVectorGroupMemberAttribute : Attribute
{
    /// <summary>The vector group that this quantity belongs to.</summary>
    public Type VectorGroup { get; }

    /// <inheritdoc cref="SharpMeasuresVectorAttribute.Dimension"/>
    public int Dimension { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorAttribute.GenerateDocumentation" path="/summary"/> By default, the behaviour is inherited from the vector group.</summary>
    public bool GenerateDocumentation { get; init; }

    /// <inheritdoc cref="SharpMeasuresVectorGroupMemberAttribute"/>
    /// <param name="vectorGroup"><inheritdoc cref="VectorGroup" path="/summary"/><para><inheritdoc cref="VectorGroup" path="/remarks"/></para></param>
    public SharpMeasuresVectorGroupMemberAttribute(Type vectorGroup)
    {
        VectorGroup = vectorGroup;
    }
}
