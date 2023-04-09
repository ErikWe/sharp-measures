namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, dictating the set of units for which a property representing the magnitude is implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitsAttribute : Attribute
{
    /// <summary>The names of the units for which a property representing the magnitude is implemented.</summary>
    public string[] IncludedUnits { get; }

    /// <summary>If the quantity is a specialized form of another quantity, dictates how to treat unit filters present in the other quantity. The default behaviour is <see cref="FilterStackingMode.Restore"/>.</summary>
    public FilterStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="IncludeUnitsAttribute"/>
    /// <param name="includedUnits"><inheritdoc cref="IncludedUnits" path="/summary"/></param>
    public IncludeUnitsAttribute(params string[] includedUnits)
    {
        IncludedUnits = includedUnits;
    }
}
