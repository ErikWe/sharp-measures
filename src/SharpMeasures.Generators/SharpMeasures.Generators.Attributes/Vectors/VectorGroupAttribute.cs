namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as the auto-generated root of a group of vectors that represent the same quantity, but of varying dimensions.</summary>
/// <remarks>The following attributes may be used to modify how the group members are generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/>, <see cref="VectorOperationAttribute"/></term>
/// <description>Describes operations { + , - , ⋅ , ÷ , ⨯ } supported by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other quantities that the quantity supports conversion to and/or from.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/>, <see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the set of unit instances for which a property representing the components is implemented.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class VectorGroupAttribute : Attribute
{
    /// <inheritdoc cref="VectorQuantityAttribute.Unit"/>
    public Type Unit { get; }

    /// <inheritdoc cref="VectorQuantityAttribute.Scalar"/>
    public Type? Scalar { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute.ImplementSum"/>
    public bool ImplementSum { get; init; }

    /// <summary><inheritdoc cref="VectorQuantityAttribute.ImplementDifference" path="/summary"/></summary>
    /// <remarks>To specify the quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary><inheritdoc cref="VectorQuantityAttribute.Difference" path="/summary"/></summary>
    /// <remarks>To disable support for computing the difference, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute.DefaultUnit"/>
    public string? DefaultUnit { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute.DefaultSymbol"/>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorGroupAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    public VectorGroupAttribute(Type unit)
    {
        Unit = unit;
    }
}
