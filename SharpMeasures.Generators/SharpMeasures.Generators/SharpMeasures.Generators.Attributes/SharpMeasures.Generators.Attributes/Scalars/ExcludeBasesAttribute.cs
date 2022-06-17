namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Dictates the set of units for which a static property representing the value 1 is not generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeBasesAttribute : Attribute
{
    /// <summary>Names of the units for which a static property representing the value 1 is not generated.</summary>
    public string[] ExcludedBases { get; }

    /// <summary>Dictates the set of units for which a static property representing the value 1 is not generated.</summary>
    /// <param name="excludedBases">Names of the units for which a static property representing the value 1 is not generated.</param>
    /// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeBasesAttribute"/> is used.</remarks>
    public ExcludeBasesAttribute(params string[] excludedBases)
    {
        ExcludedBases = excludedBases;
    }
}
