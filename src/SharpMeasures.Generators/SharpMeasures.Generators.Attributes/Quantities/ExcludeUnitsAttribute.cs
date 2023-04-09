namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, dictating the set of units for which a property representing the magnitude is not implemented.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitsAttribute : Attribute
{
    /// <summary>The names of the units for which a property representing the magnitude is not implemented.</summary>
    public string[] ExcludedUnits { get; }

    /// <summary>If the quantity is a specialized form of another quantity, dictates how to treat unit filters present in the other quantity. The default behaviour is <see cref="FilterStackingMode.Restore"/>.</summary>
    public FilterStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="ExcludeUnitsAttribute"/>
    /// <param name="excludedUnits"><inheritdoc cref="ExcludedUnits" path="/summary"/></param>
    public ExcludeUnitsAttribute(params string[] excludedUnits)
    {
        ExcludedUnits = excludedUnits;
    }
}
