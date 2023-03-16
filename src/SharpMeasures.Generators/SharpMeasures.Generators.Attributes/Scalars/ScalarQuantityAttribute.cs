namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as an auto-generated scalar quantity.</summary>
/// <remarks>The following attributes may be used to modify how the quantity is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="QuantityOperationAttribute"/></term>
/// <description>Describes operations { + , - , ⋅ , ÷ } supported by the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="QuantityProcessAttribute"/></term>
/// <description>Describes a custom process involving the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="ScalarConstantAttribute"/></term>
/// <description>Defines a constant value of the quantity.</description>
/// </item>
/// <item>
/// <term><see cref="ConvertibleQuantityAttribute"/></term>
/// <description>Lists other quantities that the quantity supports conversion to and/or from.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitBasesAttribute"/>, <see cref="ExcludeUnitBasesAttribute"/></term>
/// <description>Dictates the set of unit instances for which a static property representing the value { 1 } is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitsAttribute"/>, <see cref="ExcludeUnitsAttribute"/></term>
/// <description>Dictates the set of unit instances for which a property representing the magnitude is implemented.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ScalarQuantityAttribute : Attribute
{
    /// <summary>The unit that describes this quantity.</summary>
    public Type Unit { get; }

    /// <summary>Dictates whether the bias term of the unit should be considered, if the unit includes one. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>For example; <i>Temperature</i> would require a bias term to be able to express both <i>Celsius</i> and <i>Fahrenheit</i>, while <i>TemperatureDifference</i> would not.</remarks>
    public bool UseUnitBias { get; init; }

    /// <summary>The vector quantity associated with this quantity, if one exists.</summary>
    /// <remarks>For example, <i>Velocity</i> could be considered the vector associated with <i>Speed</i>.</remarks>
    public Type? Vector { get; init; }

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

    /// <inheritdoc cref="ScalarQuantityAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/><para><inheritdoc cref="Unit" path="/remarks"/></para></param>
    public ScalarQuantityAttribute(Type unit)
    {
        Unit = unit;
    }
}
