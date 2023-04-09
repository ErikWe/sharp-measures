namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Marks the type as an auto-generated SharpMeasures unit.</summary>
/// <typeparam name="TScalar">The scalar quantity that the unit describes.
/// <remarks>The following attributes may be used to define instances of the unit:
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
/// </list>
/// The following attribute may be used to add functionality to the unit:
/// <list type="bullet">
/// <item>
/// <term><see cref="DerivableUnitAttribute"/></term>
/// <description>Describes how instances of the unit may be derived from instances of another unit.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class UnitAttribute<TScalar> : Attribute
{
    /// <summary>Dictates whether the unit should include a bias term. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>A bias term allows units to use different definitions of zero. For example; <i>UnitOfTemperature</i> would require a bias term to express <i>Celsius</i> and <i>Fahrenheit</i>.</remarks>
    public bool BiasTerm { get; init; }

    /// <inheritdoc cref="UnitAttribute{TScalar}"/>
    public UnitAttribute() { }
}
