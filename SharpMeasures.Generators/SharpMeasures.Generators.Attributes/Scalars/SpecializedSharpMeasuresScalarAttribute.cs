namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System;

/// <summary>Marks the type as a specialized form of another scalar quantity.
/// For example, <i>Altitude</i> could be defined as a specialized form of <i>Length</i>.</summary>
/// <remarks>The following attributes may be used to modify how the scalar is generated:
/// <list type="bullet">
/// <item>
/// <term><see cref="DerivedQuantityAttribute"/></term>
/// <description>Describes how the scalar may be derived from other quantities.</description>
/// </item>
/// <item>
/// <term><see cref="ScalarConstantAttribute"/></term>
/// <description>Defines a constant of the scalar.</description>
/// </item>
/// <item>
/// <term><see cref="IncludeUnitBasesAttribute"/></term>
/// <description>Dictates the units for which a static property representing the value { 1 } is implemented.</description>
/// </item>
/// <item>
/// <term><see cref="ExcludeUnitBasesAttribute"/></term>
/// <description>Dictates the units for which a static property representing the value { 1 } is <i>not</i> implemented.</description>
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
/// <description>Lists other scalars that this scalar may be converted to.</description>
/// </item>
/// </list></remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SpecializedSharpMeasuresScalarAttribute : Attribute
{
    /// <summary>The original scalar quantity, of which this quantity is a specialized form.</summary>
    public Type OriginalScalar { get; }

    /// <summary>Dictates whether this quantity inherits the derivations defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritDerivations { get; init; }
    /// <summary>Dictates whether this quantity inherits the constants defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConstants { get; init; }
    /// <summary>Dictates whether this quantity inherits the conversions defined by the original quantity. The default behaviour is <see langword="true"/>.</summary>
    public bool InheritConversions { get; init; }
    /// <summary>Dictates whether this quantity inherits the bases of the original quantity. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>The bases are the units for which a static property representing the value { 1 } is implemented.
    /// <para>The attributes <see cref="IncludeUnitBasesAttribute"/> and <see cref="ExcludeUnitBasesAttribute"/> enable more granular control of what bases are included.</para></remarks>
    public bool InheritBases { get; init; }
    /// <summary>Dictates whether this quantity inherits the units of the original quantity. The default behaviour is <see langword="true"/>.</summary>
    /// <remarks>The attributes <see cref="IncludeUnitsAttribute"/> and <see cref="ExcludeUnitsAttribute"/> enable more granular control of what units are included.</remarks>
    public bool InheritUnits { get; init; }

    /// <summary>Determines the behaviour of the operator converting from the original quantity to this quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; }
    /// <summary>Determines the behaviour of the operator converting from this quantity to the original quantity. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresScalarAttribute.Vector" path="/summary"/> By default, the value is inherited from the original quantity.</summary>
    /// <remarks><inheritdoc cref="SharpMeasuresScalarAttribute.Vector" path="/remarks"/></remarks>
    public Type? Vector { get; init; }

    /// <summary>Dictates whether to implement support for computing the sum of two instances of this scalar. By default, the behaviour is inherited from the
    /// original quantity.</summary>
    public bool ImplementSum { get; init; }

    /// <summary>Dictates whether to implement support for computing the difference between two instances of this scalar. By default, the behaviour is
    /// inherited from the original quantity.</summary>
    /// <remarks>To specify the scalar quantity that represents the difference, use <see cref="Difference"/>.</remarks>
    public bool ImplementDifference { get; init; }

    /// <summary>The scalar quantity that is considered the difference between two instances of this scalar. By default, the value is inherited from the
    /// original quantity.</summary>
    /// <remarks>To disable support for computing the difference in the first place, use <see cref="ImplementDifference"/>.</remarks>
    public Type? Difference { get; init; }

    /// <summary>The name of the default unit instance. By default, the value is inherited from the original quantity.</summary>
    public string? DefaultUnitInstanceName { get; init; }

    /// <summary>The symbol of the default unit instance. By default, the value is inherited from the original quantity.</summary>
    public string? DefaultUnitInstanceSymbol { get; init; }

    /// <summary><inheritdoc cref="SharpMeasuresScalarAttribute.GenerateDocumentation" path="/summary"/> By default, the behaviour is inherited from
    /// the original quantity.</summary>
    public bool GenerateDocumentation { get; init; }

    /// <inheritdoc cref="SpecializedSharpMeasuresScalarAttribute"/>
    /// <param name="originalScalar"><inheritdoc cref="OriginalScalar" path="/summary"/><para><inheritdoc cref="OriginalScalar" path="/remarks"/></para></param>
    public SpecializedSharpMeasuresScalarAttribute(Type originalScalar)
    {
        OriginalScalar = originalScalar;
    }
}
