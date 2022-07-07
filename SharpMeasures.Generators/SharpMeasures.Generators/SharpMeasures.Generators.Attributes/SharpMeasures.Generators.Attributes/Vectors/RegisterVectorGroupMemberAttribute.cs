namespace SharpMeasures.Generators.Vectors;

using System;

/// <summary>Registers a vector as member of a vector group.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class RegisterVectorGroupMemberAttribute : Attribute
{
    /// <summary>The vector quantity that is registered as member of the vector group.</summary>
    public Type Vector { get; }

    /// <inheritdoc cref="SharpMeasuresVectorAttribute.Dimension"/>
    public int Dimension { get; init; }

    /// <inheritdoc cref="RegisterVectorGroupMemberAttribute"/>
    /// <param name="vector"><inheritdoc cref="Vector" path="/summary"/><para><inheritdoc cref="Vector" path="/remarks"/></para></param>
    public RegisterVectorGroupMemberAttribute(Type vector)
    {
        Vector = vector;
    }
}
