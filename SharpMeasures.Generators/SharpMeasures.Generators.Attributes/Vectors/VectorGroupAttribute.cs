namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as the root of a group of vectors that represent the same quantity, but of different dimension.</summary>
/// <remarks>The following attributes may be used to modify how the vectors of the group are generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/></term>
/// <description>Describes how the vectors of the group may be derived from other quantities.</description>
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
/// <description>Lists other vectors that the vectors in the group may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class VectorGroupAttribute : Attribute
{
    /// <summary>The unit that describes the quantities of this group.</summary>
    public Type Unit { get; }

    /// <summary>The scalar quantity that is associated with the vectors of this group, if one exists. This scalar is used to describe the
    /// magnitude and individual components of the vectors.</summary>
    /// <remarks>For example; <i>Speed</i> could be considered the scalar associated with <i>Velocity</i>.</remarks>
    public Type? Scalar { get; init; }

    /// <summary>Dictates whether the vectors of this group should implement support for computing the sum of two instances of that vector. The default
    /// behaviour is <see langword="true"/>.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether the vectors of this group should implement support for computing the difference between two instances of that vector. The default
    /// behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify the vector group that represents the differences, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The vector group that consists of vectors that are considered the differences between two instances of the vectors of this group. By default, and
    /// when <see langword="null"/>, the group itself is used.</summary>
    /// <remarks>To disable support for computing the differences in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary>The name of the default unit instance of the vectors of this group.</summary>
    public string? DefaultUnit { get; init; }

    /// <summary>The symbol of the default unit instance of the vectors of this group.</summary>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorGroupAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    public VectorGroupAttribute(Type unit)
    {
        Unit = unit;
    }
}
