namespace SharpMeasures.Generators.Quantities;

using System;

/// <summary>Dictates the set of units for which a property representing the magnitude is not generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which a property representing the magnitude is not generated.</summary>
    public string[] ExcludedUnits { get; }

    /// <summary>Dictates the set of units for which a property representing the magnitude is not generated.</summary>
    /// <param name="excludedUnits">Names of the units for which a property representing the magnitude is not generated.</param>
    /// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
    public ExcludeUnitsAttribute(params string[] excludedUnits)
    {
        ExcludedUnits = excludedUnits;
    }
}
