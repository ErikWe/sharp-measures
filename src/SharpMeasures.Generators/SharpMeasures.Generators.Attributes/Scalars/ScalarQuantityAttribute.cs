namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures scalar quantity.</summary>
/// <typeparam name="TUnit">The unit that describes the quantity.</typeparam>
/// <remarks>The following attributes may be used to modify the quantity:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute{TResult, TOther}"/></term>
/// <description>Describes the operations { + , - , ⋅ , ÷ } supported by the quantity.</description>
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
/// <term><see cref="VectorAssociationAttribute{TVector}"/></term>
/// <description>Describes the quantity as associated with a vector quantity - allowing multiplication by pure vectors.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other quantities that the quantity supports conversion to and/or from.</description>
/// </item>
/// <item>
/// <term><see cref="ScalarConstantAttribute"/></term>
/// <description>Defines a constant value of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitBasesAttribute"/> / <see cref="ExcludeUnitBasesAttribute"/></term>
/// <description>Dictates the set of unit instances for which a static property representing the magnitude { 1 } is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/> / <see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the set of unit instances for which a property representing the magnitude is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="DefaultUnitAttribute"/></term>
/// <description>Dictates the default unit of the quantity - used when formatting the quantity.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class ScalarQuantityAttribute<TUnit> : Attribute
{
    /// <summary>Dictates whether the quantity allows negative magnitudes. If <see langword="false"/>, the absolute value of the provided magnitude is used. The default behaviour is <see langword="true"/>.</summary>
    public bool AllowNegative { get; init; }

    /// <summary>Dictates whether quantity should consider the bias term of the unit, if the unit includes one. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>For example; <i>Temperature</i> would need to consider the bias term to be able to express both <i>Celsius</i> and <i>Fahrenheit</i>, while <i>TemperatureDifference</i> would not.</remarks>
    public bool UseUnitBias { get; init; }

    /// <summary>Dictates whether the quantity should support addition of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify what quantity represents the sum, use <see cref="QuantitySumAttribute{T}"/>.</remarks>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify what quantity represents the difference, use <see cref="QuantityDifferenceAttribute{T}"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <inheritdoc cref="ScalarQuantityAttribute{TUnit}"/>
    public ScalarQuantityAttribute() { }
}
