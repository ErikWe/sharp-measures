namespace SharpMeasures.Generators.Quantities;

using System;

/// <summary>Applied to quantities, dictating the set of units for which a property representing the magnitude is implemented.</summary>
/// <remarks>If this attribute is absent, all recognized units are included - unless <see cref="ExcludeUnitsAttribute"/> is used.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class IncludeUnitsAttribute : Attribute
{
    /// <summary>Names of the units for which a property representing the magnitude is implemented.</summary>
    public string[] IncludedUnits { get; }

    /// <inheritdoc cref="IncludeUnitsAttribute"/>
    /// <param name="includedUnits"><inheritdoc cref="IncludedUnits" path="/summary"/><para><inheritdoc cref="IncludedUnits" path="/remarks"/></para></param>
    public IncludeUnitsAttribute(params string[] includedUnits)
    {
        IncludedUnits = includedUnits;
    }
}
