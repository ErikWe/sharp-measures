namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures vector quantity, behaving as a specialized form of another quantity, <typeparamref name="TOriginal"/>.</summary>
/// <typeparam name="TOriginal">The original quantity, of which this quantity is a specialized form.</typeparam>
/// <remarks>The following attributes may be used to modify the quantity:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute{TResult, TOther}"/></term>
/// <description>Describes the operations { + , - , ⋅ , ÷, ⨯ } supported by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityProcessAttribute{TResult}"/></term>
/// <description>Describes a custom process implemented by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityPropertyAttribute{TResult}"/></term>
/// <description>Describes a custom readonly property implemented by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantitySumAttribute{TSum}"/></term>
/// <description>Describes the result of addition of two instances of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityDifferenceAttribute{TDifference}"/></term>
/// <description>Describes the result of subtraction of two instances of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="ScalarAssociationAttribute{TScalar}"/></term>
/// <description>Describes the quantity as associated with a scalar quantity.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other quantities that the quantity supports conversion to and/or from.</description>
/// </item>
/// <item>
/// <term><see cref="VectorConstantAttribute"/></term>
/// <description>Defines a constant value of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/> / <see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the set of unit instances for which a property representing the magnitudes of the components is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="DefaultUnitAttribute"/></term>
/// <description>Dictates the default unit of the quantity - used when formatting the quantity.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class SpecializedVectorQuantityAttribute<TOriginal> : Attribute
{
    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.InheritOperations"/>
    public bool InheritOperations { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.InheritProcesses"/>
    public bool InheritProcesses { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.InheritProperties"/>
    public bool InheritProperties { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.InheritConstants"/>
    public bool InheritConstants { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.InheritConversions"/>
    public bool InheritConversions { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.InheritUnits"/>
    public bool InheritUnits { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.ForwardsCastOperatorBehaviour"/>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.BackwardsCastOperatorBehaviour"/>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.ImplementSum"/>
    public bool ImplementSum { get; init; }

    /// <inheritdoc cref="SpecializedScalarQuantityAttribute{TOriginal}.ImplementDifference"/>
    public bool ImplementDifference { get; init; }

    /// <inheritdoc cref="SpecializedVectorQuantityAttribute{TOriginal}"/>
    public SpecializedVectorQuantityAttribute() { }
}
