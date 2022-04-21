namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Dictates the set of units for which a property representing the magnitude is not generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ExcludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which a property representing the magnitude is not generated.</summary>
    public IEnumerable<string> ExcludedUnits { get; }

    /// <summary>Dictates the set of units for which a property representing the magnitude is not generated.</summary>
    /// <param name="excludedUnits">Names of the units for which a property representing the magnitude is not generated.</param>
    /// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
    public ExcludeUnitsAttribute(IEnumerable<string> excludedUnits)
    {
        ExcludedUnits = excludedUnits;
    }
}
