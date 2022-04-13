namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

/// <summary>Dictates what units are not included as pre-defined constants on the quantity.</summary>
/// <remarks>If this attribute is absent, all units are included - unless <see cref="IncludeBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ExcludeBasesAttribute : Attribute
{
    /// <summary>Names of the units that are not included as pre-defined constants.</summary>
    public IEnumerable<string> ExcludedBases { get; }

    /// <summary>Names of the units that are not included as pre-defined constants.</summary>
    /// <param name="excludedBases">Names of the units that are not included as pre-defined constants.</param>
    /// <remarks>If this attribute is absent, all units are included as pre-defined constants - unless <see cref="IncludeBasesAttribute"/> is used.</remarks>
    public ExcludeBasesAttribute(IEnumerable<string> excludedBases)
    {
        ExcludedBases = excludedBases;
    }
}
