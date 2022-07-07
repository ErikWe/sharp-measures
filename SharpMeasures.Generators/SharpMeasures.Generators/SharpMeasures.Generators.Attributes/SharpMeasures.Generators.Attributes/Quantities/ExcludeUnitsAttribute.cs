namespace SharpMeasures.Generators.Quantities;

using System;

/// <summary>Applied to quantities, dictating the set of units for which a property representing the magnitude is <i>not</i> implemented.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="IncludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ExcludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which a property representing the magnitude is <i>not</i> implemented.</summary>
    public string[] ExcludedUnits { get; }

    /// <inheritdoc cref="ExcludeUnitsAttribute"/>
    /// <param name="excludedUnits"><inheritdoc cref="ExcludedUnits" path="/summary"/><para><inheritdoc cref="ExcludedUnits" path="/remarks"/></para></param>
    public ExcludeUnitsAttribute(params string[] excludedUnits)
    {
        ExcludedUnits = excludedUnits;
    }
}
