namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as a vector quantity.</summary>
/// <remarks>The following attributes may be used to modify how the vector is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/></term>
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
public sealed class VectorQuantityAttribute : Attribute
{
    /// <summary>The unit that describes this quantity.</summary>
    public Type Unit { get; }

    /// <summary>The dimension of the vector quantity.</summary>
    /// <remarks>This does not have to be explicitly specified if the name of the type ends with the dimension - such as for <i>Position3</i>.</remarks>
    public int Dimension { get; init; }

    /// <summary>The scalar quantity that is associated with this vector, if one exists. This scalar is used to describe the
    /// magnitude and individual components of the vector.</summary>
    /// <remarks>For example; <i>Speed</i> could be considered the scalar associated with <i>Velocity</i>.</remarks>
    public Type? Scalar { get; init; }

    /// <summary>Dictates whether to implement support for computing the sum of two instances of this vector. The default behaviour is <see langword="true"/>.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether to implement support for computing the difference between two instances of this vector. The default behaviour is
    /// <see langword="true"/>.</summary>
    /// <remarks>To specify the vector quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The vector quantity that is considered the difference between two instances of this vector. By default, and when <see langword="null"/>, the
    /// quantity itself is used to also describe the difference.</summary>
    /// <remarks>To disable support for computing the difference in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary>The name of the default unit instance.</summary>
    public string? DefaultUnit { get; init; }

    /// <summary>The symbol of the default unit instance.</summary>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    public VectorQuantityAttribute(Type unit)
    {
        Unit = unit;
    }
}
