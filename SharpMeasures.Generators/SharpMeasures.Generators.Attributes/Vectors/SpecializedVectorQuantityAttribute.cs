namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as an auto-generated vector quantity, behaving as a specialized form of another quantity.</summary>
/// <remarks><inheritdoc cref="VectorQuantityAttribute" path="/remarks"/></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedVectorQuantityAttribute : Attribute
{
    /// <summary>The original vector quantity, of which this quantity is a specialized form.</summary>
    public Type OriginalVector { get; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.InheritOperations"/>
    public bool InheritOperations { get; init; }
    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.InheritProcesses"/>
    public bool InheritProcesses { get; init; }
    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.InheritConstants"/>
    public bool InheritConstants { get; init; }
    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.InheritConversions"/>
    public bool InheritConversions { get; init; }
    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.InheritUnits"/>
    public bool InheritUnits { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.ForwardsCastOperatorBehaviour"/>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }
    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.BackwardsCastOperatorBehaviour"/>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <summary><inheritdoc cref="VectorQuantityAttribute.Scalar" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks>For example; <i>Speed</i> could be considered the scalar associated with <i>Velocity</i>.</remarks>
    public Type? Scalar { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.ImplementSum"/>
    public bool ImplementSum { get; init; }

    /// <summary><inheritdoc cref="SpecializedScalarQuantityAttribute.ImplementDifference" path="/summary"/></summary>
    /// <remarks>To specify the quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary><inheritdoc cref="SpecializedScalarQuantityAttribute.Difference" path="/summary"/></summary>
    /// <remarks>To disable support for computing the difference, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.DefaultUnit"/>
    public string? DefaultUnit { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute.DefaultSymbol"/>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute"/>
    /// <param name="originalVector"><inheritdoc cref="OriginalVector" path="/summary"/><para><inheritdoc cref="OriginalVector" path="/remarks"/></para></param>
    public SpecializedVectorQuantityAttribute(Type originalVector)
    {
        OriginalVector = originalVector;
    }
}
