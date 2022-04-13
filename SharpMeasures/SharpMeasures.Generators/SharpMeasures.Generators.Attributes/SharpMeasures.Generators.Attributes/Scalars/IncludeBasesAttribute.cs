namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Dictates what units are included as pre-defined constants on the quantity.</summary>
/// <remarks>If this attribute is absent, all available units are included - unless <see cref="ExcludeBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class IncludeBasesAttribute : Attribute
{
    /// <summary>Names of the units that are included as pre-defined constants.</summary>
    public IEnumerable<string> IncludedBases { get; }

    /// <summary>Dictates what units are included as pre-defined constants on the quantity.</summary>
    /// <param name="includedBases">Names of the units that are included as pre-defined constants.</param>
    /// <remarks>If this attribute is absent, all available units are included - unless <see cref="ExcludeBasesAttribute"/> is used.</remarks>
    public IncludeBasesAttribute(IEnumerable<string> includedBases)
    {
        IncludedBases = includedBases;
    }
}
