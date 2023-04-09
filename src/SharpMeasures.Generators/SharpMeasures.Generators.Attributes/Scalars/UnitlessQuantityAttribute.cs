namespace SharpMeasures;

using System;

/// <summary>Marks the type as an auto-generated SharpMeasures unit-less quantity.</summary>
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
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other quantities that the quantity supports conversion to and/or from.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class UnitlessQuantityAttribute : Attribute
{
    /// <summary>Dictates whether the quantity allows negative magnitudes. If <see langword="false"/>, the absolute value of the provided magnitude is used. The default behaviour is <see langword="true"/>.</summary>
    public bool AllowNegative { get; init; }

    /// <summary>Dictates whether the quantity should support addition of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify what quantity represents the sum, use <see cref="QuantitySumAttribute{T}"/>.</remarks>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify what quantity represents the difference, use <see cref="QuantityDifferenceAttribute{T}"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <inheritdoc cref="UnitlessQuantityAttribute"/>
    public UnitlessQuantityAttribute() { }
}
