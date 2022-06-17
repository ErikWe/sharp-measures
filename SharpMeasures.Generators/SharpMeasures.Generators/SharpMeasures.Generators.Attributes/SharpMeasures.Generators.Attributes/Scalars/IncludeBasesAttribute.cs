namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Dictates the set of units for which a static property representing the value 1 is generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeBasesAttribute : Attribute
{
    /// <summary>Names of the units for which a static property representing the value 1 is generated.</summary>
    public string[] IncludedBases { get; }

    /// <summary>Dictates the set of units for which a static property representing the value 1 is generated.</summary>
    /// <param name="includedBases">Names of the units for which a static property representing the value 1 is generated.</param>
    /// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeBasesAttribute"/> is used.</remarks>
    public IncludeBasesAttribute(params string[] includedBases)
    {
        IncludedBases = includedBases;
    }
}
