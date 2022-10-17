namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as an auto-generated vector quantity.</summary>
/// <remarks>The following attributes may be used to modify how the quantity is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/>, <see cref="VectorOperationAttribute"/></term>
/// <description>Describes operations { + , - , ⋅ , ÷ , ⨯ } supported by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityProcessAttribute"/></term>
/// <description>Describes a custom process involving the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="VectorConstantAttribute"/></term>
/// <description>Defines a constant value of the quantity.</description>
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
public sealed class VectorQuantityAttribute : Attribute
{
    /// <summary>The unit that describes this quantity.</summary>
    public Type Unit { get; }

    /// <summary>The dimension of this quantity.</summary>
    /// <remarks>This does not have to be explicitly specified if the name of the type ends with the dimension - for example, { <i>Position3</i> }.</remarks>
    public int Dimension { get; init; }

    /// <summary>The scalar quantity associated with this quantity, if one exists.</summary>
    /// <remarks>For example; <i>Speed</i> could be considered the scalar associated with <i>Velocity</i>.</remarks>
    public Type? Scalar { get; init; }

    /// <summary>Dictates whether this quantity should support addition of two instances. The default behaviour is <see langword="true"/>.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether this quantity should support subtraction of two instances. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>To specify the quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>Determines the quantity that is considered the difference between two instances of this quantity. By default, the quantity itself is used to describe the difference.</summary>
    /// <remarks>To disable support for computing the difference, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary>The name of the default unit instance.</summary>
    public string? DefaultUnit { get; init; }

    /// <summary>The symbol representing the default unit instance.</summary>
    public string? DefaultSymbol { get; init; }

    /// <inheritdoc cref="VectorQuantityAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    public VectorQuantityAttribute(Type unit)
    {
        Unit = unit;
    }
}
