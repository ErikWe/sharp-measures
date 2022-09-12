namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Dictates the set of units for which a static property representing the value { 1 } is generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeUnitBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitBasesAttribute : Attribute
{
    /// <summary>Names of the units for which a static property representing the value { 1 } is generated.</summary>
    public string[] IncludedUnitBases { get; }

    /// <summary>If the quantity is a specialized form of another quantity, this property determines how to treath units that were marked as included by the original quantity. The default behaviour is
    /// <see cref="InclusionStackingMode.Intersection"/>.</summary>
    /// <remarks>This does not apply to multiple instances of <see cref="IncludeUnitBasesAttribute"/> applied to the same quantity. In such cases, all instances will simply be combined.</remarks>
    public InclusionStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="IncludeUnitBasesAttribute"/>
    /// <param name="includedUnitBases"><inheritdoc cref="IncludedUnitBases" path="/summary"/><para><inheritdoc cref="IncludedUnitBases" path="/remarks"/></para></param>
    public IncludeUnitBasesAttribute(params string[] includedUnitBases)
    {
        IncludedUnitBases = includedUnitBases;
    }
}
