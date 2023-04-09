namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures scalar quantities, dictating the set of units for which a static property representing the magnitude { 1 } is not implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="IncludeUnitBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitBasesAttribute : Attribute
{
    /// <summary>The names of the units for which a static property representing the magnitude { 1 } is not implemented.</summary>
    public string[] ExcludedUnitBases { get; }

    /// <summary>If the quantity is a specialized form of another quantity, dictates how to treat unit filters present in the other quantity. The default behaviour is <see cref="FilterStackingMode.Restore"/>.</summary>
    public FilterStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="ExcludeUnitBasesAttribute"/>
    /// <param name="excludedUnitBases"><inheritdoc cref="ExcludedUnitBases" path="/summary"/></param>
    public ExcludeUnitBasesAttribute(params string[] excludedUnitBases)
    {
        ExcludedUnitBases = excludedUnitBases;
    }
}
