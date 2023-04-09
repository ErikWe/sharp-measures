namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, dictating the set of units for which a static property representing the magnitude { 1 } is implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="ExcludeUnitBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitBasesAttribute : Attribute
{
    /// <summary>The names of the units for which a static property representing the magnitude { 1 } is implemented.</summary>
    public string[] IncludedUnitBases { get; }

    /// <summary>If the quantity is a specialized form of another quantity, dictates how to treat unit filters present in the other quantity. The default behaviour is <see cref="FilterStackingMode.Restore"/>.</summary>
    public FilterStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="IncludeUnitBasesAttribute"/>
    /// <param name="includedUnitBases"><inheritdoc cref="IncludedUnitBases" path="/summary"/></param>
    public IncludeUnitBasesAttribute(params string[] includedUnitBases)
    {
        IncludedUnitBases = includedUnitBases;
    }
}
