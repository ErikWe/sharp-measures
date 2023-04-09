namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures vector quantity.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
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
public sealed class VectorQuantityAttribute<TUnit> : Attribute
{
    /// <summary>The dimension of the quantity.</summary>
    /// <remarks>This does not have to be explicitly specified if the name of the type ends with the dimension - for example, { <i>Position3</i> }.</remarks>
    public int Dimension { get; init; }

    /// <summary>Dictates whether the quantity should support addition of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify what quantity represents the sum, use <see cref="QuantitySumAttribute{T}"/>.</remarks>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify what quantity represents the difference, use <see cref="QuantityDifferenceAttribute{T}"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute{TUnit}"/>
    public VectorQuantityAttribute() { }
}
