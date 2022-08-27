namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Utility;

using System;

/// <summary>Dictates the set of units for which a static property representing the value { 1 } is generated.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeBasesAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeBasesAttribute : Attribute
{
    /// <summary>Names of the units for which a static property representing the value { 1 } is generated.</summary>
    public string[] IncludedBases { get; }

    /// <summary>If the quantity is a specialized form of another quantity, this property determines how to treath units that were marked as included by the original quantity. The default behaviour is
    /// <see cref="InclusionStackingMode.Intersection"/>.</summary>
    /// <remarks>This does not apply to multiple instances of <see cref="IncludeBasesAttribute"/> applied to the same quantity. In such cases, all instances will simply be combined.</remarks>
    public InclusionStackingMode StackingMode { get; init; }

    /// <inheritdoc cref="IncludeBasesAttribute"/>
    /// <param name="includedBases"><inheritdoc cref="IncludedBases" path="/summary"/><para><inheritdoc cref="IncludedBases" path="/remarks"/></para></param>
    public IncludeBasesAttribute(params string[] includedBases)
    {
        IncludedBases = includedBases;
    }
}
