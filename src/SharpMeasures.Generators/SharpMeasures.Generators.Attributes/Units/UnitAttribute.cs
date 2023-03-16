namespace SharpMeasures.Generators;

using System;

/// <summary>Marks the type as a unit.</summary>
/// <remarks>The following accompanying attributes may be used to enhance the unit:
/// <list type="bullet">
/// <item>
/// <term><see cref="FixedUnitInstanceAttribute"/></term>
/// <description>Defines an instance of the unit.</description>
/// </item>
/// <item>
/// <term><see cref="ScaledUnitInstanceAttribute"/></term>
/// <description>Defines an instance of the unit by scaling another instance.</description>
/// </item>
/// <item>
/// <term><see cref="PrefixedUnitInstanceAttribute"/></term>
/// <description>Defines an instance of the unit by applying a prefix to another instance.</description>
/// </item>
/// <item>
/// <term><see cref="BiasedUnitInstanceAttribute"/></term>
/// <description>Defines an instance of the unit according to a bias relative to another instance.</description>
/// </item>
/// <item>
/// <term><see cref="UnitInstanceAliasAttribute"/></term>
/// <description>Defines an instance of the unit as an alias of another instance.</description>
/// </item>
/// <item>
/// <term><see cref="DerivedUnitInstanceAttribute"/></term>
/// <description>Defines an instance of the unit using instances of another unit.</description>
/// </item>
/// <item>
/// <term><see cref="DerivableUnitAttribute"/></term>
/// <description>Describes how instances of the unit may be derived from instances of another unit.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class UnitAttribute : Attribute
{
    /// <summary>The scalar quantity that this unit describes.</summary>
    /// <remarks>For units that include a bias term, this should represent an associated unbiased quantity. For example; <i>UnitOfTemperature</i>,
    /// should be described by <i>TemperatureDifference</i> rather than <i>Temperature</i>.</remarks>
    public Type Quantity { get; }

    /// <summary>Dictates whether this unit should include a bias term. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>This allows units to use different definitions for zero. For example; <i>UnitOfTemperature</i> would require a bias term
    /// to be able to express both <i>Celsius</i> and <i>Fahrenheit</i>.</remarks>
    public bool BiasTerm { get; init; }

    /// <inheritdoc cref="UnitAttribute"/>
    /// <param name="quantity"><inheritdoc cref="Quantity" path="/summary"/><para><inheritdoc cref="Quantity" path="/remarks"/></para></param>
    public UnitAttribute(Type quantity)
    {
        Quantity = quantity;
    }
}
