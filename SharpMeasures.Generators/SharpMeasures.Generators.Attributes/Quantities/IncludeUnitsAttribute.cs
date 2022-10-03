namespace SharpMeasures.Generators;

using System;

/// <summary>Applied to quantities, dictating the set of units for which a property representing the magnitude is implemented.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which a property representing the magnitude is implemented.</summary>
    public string[] IncludedUnits { get; }

    /// <summary>If the quantity is a specialized form of another quantity, this property determines how to treath units that were marked as included by the original quantity. The default behaviour is
    /// <see cref="InclusionStackingMode.Intersection"/>.</summary>
    /// <remarks>This does not apply to multiple instances of <see cref="IncludeUnitsAttribute"/> applied to the same quantity. In such cases, all instances will simply be combined.</remarks>
    public InclusionStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="IncludeUnitsAttribute"/>
    /// <param name="includedUnits"><inheritdoc cref="IncludedUnits" path="/summary"/><para><inheritdoc cref="IncludedUnits" path="/remarks"/></para></param>
    public IncludeUnitsAttribute(params string[] includedUnits)
    {
        IncludedUnits = includedUnits;
    }
}
