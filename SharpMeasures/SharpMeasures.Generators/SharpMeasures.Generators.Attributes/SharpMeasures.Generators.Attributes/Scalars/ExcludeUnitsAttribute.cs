namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Dictates for what units magnitude-getters are not included.</summary>
/// <remarks>If this attribute is absent, all available units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ExcludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which magnitude-getters are not included.</summary>
    public IEnumerable<string> ExcludedUnits { get; }

    /// <summary>Dictates for what units magnitude-getters are not included.</summary>
    /// <param name="excludedUnits">Names of the units for which magnitude-getters are not included.</param>
    /// <remarks>If this attribute is absent, all available units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
    public ExcludeUnitsAttribute(IEnumerable<string> excludedUnits)
    {
        ExcludedUnits = excludedUnits;
    }
}
