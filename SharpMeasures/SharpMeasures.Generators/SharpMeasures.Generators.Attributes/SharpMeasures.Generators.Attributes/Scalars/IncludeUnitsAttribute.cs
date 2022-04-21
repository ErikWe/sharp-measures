namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Dictates the set of units for which a property representing the magnitude is generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class IncludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which a property representing the magnitude is generated.</summary>
    public IEnumerable<string> IncludedUnits { get; }

    /// <summary>Dictates the set of units for which a property representing the magnitude is generated.</summary>
    /// <param name="includedUnits">Names of the units for which a property representing the magnitude is generated.</param>
    /// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
    public IncludeUnitsAttribute(IEnumerable<string> includedUnits)
    {
        IncludedUnits = includedUnits;
    }
}
