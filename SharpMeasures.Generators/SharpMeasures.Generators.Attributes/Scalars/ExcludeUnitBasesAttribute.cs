namespace SharpMeasures.Generators.Scalars;

using System;

/// <summary>Dictates the set of units for which a static property representing the value { 1 } is <i>not</i> generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeUnitBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitBasesAttribute : Attribute
{
    /// <summary>Names of the units for which a static property representing the value { 1 } is <i>not</i> generated.</summary>
    public string[] ExcludedUnitBases { get; }

    /// <inheritdoc cref="ExcludeUnitBasesAttribute"/>
    /// <param name="excludedUnitBases"><inheritdoc cref="ExcludedUnitBases" path="/summary"/><para><inheritdoc cref="ExcludedUnitBases" path="/remarks"/></para></param>
    public ExcludeUnitBasesAttribute(params string[] excludedUnitBases)
    {
        ExcludedUnitBases = excludedUnitBases;
    }
}
