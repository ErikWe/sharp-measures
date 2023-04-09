namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as the auto-generated root of a group of SharpMeasures vectors that represent the same quantity, but of varying dimension.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
/// <remarks>The following attributes may be used to modify how the group members are generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute{TResult, TOther}"/></term>
/// <description>Describes the operations { + , - , ⋅ , ÷ } supported by the quantity.</description>
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
public sealed class VectorGroupAttribute<TUnit> : Attribute
{
    /// <inheritdoc cref="VectorQuantityAttribute{TUnit}.ImplementSum"/>
    public bool ImplementSum { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute{TUnit}.ImplementDifference"/>
    public bool ImplementDifference { get; init; }

    /// <inheritdoc cref="VectorGroupAttribute{TUnit}"/>
    public VectorGroupAttribute() { }
}
