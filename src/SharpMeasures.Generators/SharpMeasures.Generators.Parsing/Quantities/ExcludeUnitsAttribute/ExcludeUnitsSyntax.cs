namespace SharpMeasures.Generators.Parsing.Quantities.ExcludeUnitsAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IExcludeUnitsSyntax"/>
internal sealed record class ExcludeUnitsSyntax : IExcludeUnitsSyntax
{
    private Location ExcludedUnitsCollection { get; }
    private IReadOnlyList<Location> ExcludeUnitsElements { get; }
    private Location StackingMode { get; }

    /// <summary>Instantiates a <see cref="ExcludeUnitsSyntax"/>, representing syntactical information about a parsed <see cref="ExcludeUnitsAttribute"/>.</summary>
    /// <param name="excludedUnitsCollection"><inheritdoc cref="IExcludeUnitsSyntax.ExcludedUnitsCollection" path="/summary"/></param>
    /// <param name="excludeUnitsElements"><inheritdoc cref="IExcludeUnitsSyntax.ExcludedUnitsElements" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IExcludeUnitsSyntax.StackingMode" path="/summary"/></param>
    public ExcludeUnitsSyntax(Location excludedUnitsCollection, IReadOnlyList<Location> excludeUnitsElements, Location stackingMode)
    {
        ExcludedUnitsCollection = excludedUnitsCollection;
        ExcludeUnitsElements = excludeUnitsElements;
        StackingMode = stackingMode;
    }

    Location IExcludeUnitsSyntax.ExcludedUnitsCollection => ExcludedUnitsCollection;
    IReadOnlyList<Location> IExcludeUnitsSyntax.ExcludedUnitsElements => ExcludeUnitsElements;
    Location IExcludeUnitsSyntax.StackingMode => StackingMode;
}
