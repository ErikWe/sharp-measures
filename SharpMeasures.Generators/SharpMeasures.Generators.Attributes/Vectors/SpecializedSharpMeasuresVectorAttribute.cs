namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System;

/// <summary>Marks the type as a specialized form of another vector quantity.
/// For example, <i>GravitationalAcceleration</i> could be defined as a specialized form of <i>Acceleration</i>.</summary>
/// <remarks>The following attributes may be used to modify how the vector is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="DerivedQuantityAttribute"/></term>
/// <description>Describes how the vector may be derived from other quantities.</description>
/// </item>
/// <item>
/// <term><see cref="VectorConstantAttribute"/></term>
/// <description>Defines a constant of the vector.</description>
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
/// <description>Lists other vectors that this vector may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedSharpMeasuresVectorAttribute : Attribute
{
    /// <summary>The original vector quantity, of which this is a specialized form.</summary>
    public Type OriginalVector { get; }

    /// <inheritdoc cref="SpecializedSharpMeasuresScalarAttribute.InheritDerivations"/>
    public bool InheritDerivations { get; init; }
    /// <inheritdoc cref="SpecializedSharpMeasuresScalarAttribute.InheritProcesses"/>
    public bool InheritProcesses { get; init; }
    /// <inheritdoc cref="SpecializedSharpMeasuresScalarAttribute.InheritConstants"/>
    public bool InheritConstants { get; init; }
    /// <inheritdoc cref="SpecializedSharpMeasuresScalarAttribute.InheritConversions"/>
    public bool InheritConversions { get; init; }
    /// <inheritdoc cref="SpecializedSharpMeasuresScalarAttribute.InheritUnits"/>
    public bool InheritUnits { get; init; }

    /// <summary>Determines the behaviour of the operator converting from the original quantity to this quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }
    /// <summary>Determines the behaviour of the operator converting from this quantity to the original quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorAttribute.Scalar" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks>For example; <i>Speed</i> could be considered the scalar associated with <i>Velocity</i>.</remarks>
    public Type? Scalar { get; init; }

    /// <summary>Dictates whether to implement support for computing the sum of two instances of this vector. By default, the behaviour is inherited from the original
    /// quantity.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether to implement support for computing the difference between two instances of this vector. By default, the behaviour is inherited from
    /// the original quantity.</summary>
    /// <remarks>To specify the vector quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The vector quantity that is considered the difference between two instances of this vector. By default, the value is inherited from the
    /// original quantity.</summary>
    /// <remarks>To disable support for computing the difference in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorAttribute.DefaultUnitInstanceName" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks><inheritdoc cref="SharpMeasuresVectorAttribute.DefaultUnitInstanceName" path="/remarks"/></remarks>
    public string? DefaultUnitInstanceName { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorAttribute.DefaultUnitInstanceSymbol" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks><inheritdoc cref="SharpMeasuresVectorAttribute.DefaultUnitInstanceSymbol" path="/remarks"/></remarks>
    public string? DefaultUnitInstanceSymbol { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresVectorAttribute.GenerateDocumentation" path="/summary"/> By default, the behaviour is inherited from the original
    /// quantity.</summary>
    public bool GenerateDocumentation { get; init; }

    /// <inheritdoc cref="SharpMeasuresVectorAttribute"/>
    /// <param name="originalVector"><inheritdoc cref="OriginalVector" path="/summary"/><para><inheritdoc cref="OriginalVector" path="/remarks"/></para></param>
    public SpecializedSharpMeasuresVectorAttribute(Type originalVector)
    {
        OriginalVector = originalVector;
    }
}
