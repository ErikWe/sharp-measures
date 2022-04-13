namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Dictates for what units magnitude-getters are included.</summary>
/// <remarks>If this attribute is absent, all available units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class IncludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which magnitude-getters are included.</summary>
    public IEnumerable<string> IncludedUnits { get; }

    /// <summary>Dictates for what units magnitude-getters are included.</summary>
    /// <param name="includedUnits">Names of the units for which magnitude-getters are included.</param>
    /// <remarks>If this attribute is absent, all available units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
    public IncludeUnitsAttribute(IEnumerable<string> includedUnits)
    {
        IncludedUnits = includedUnits;
    }
}
