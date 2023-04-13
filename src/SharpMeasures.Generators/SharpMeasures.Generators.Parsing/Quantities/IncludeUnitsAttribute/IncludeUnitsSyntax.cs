namespace SharpMeasures.Generators.Parsing.Quantities.IncludeUnitsAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IIncludeUnitsSyntax"/>
internal sealed record class IncludeUnitsSyntax : IIncludeUnitsSyntax
{
    private Location IncludedUnitsCollection { get; }
    private IReadOnlyList<Location> IncludedUnitsElements { get; }
    private Location StackingMode { get; }

    /// <summary>Instantiates a <see cref="IncludeUnitsSyntax"/>, representing syntactical information about a parsed <see cref="IncludeUnitsAttribute"/>.</summary>
    /// <param name="includedUnitsCollation"><inheritdoc cref="IIncludeUnitsSyntax.IncludedUnitsCollection" path="/summary"/></param>
    /// <param name="includedUnitsElements"><inheritdoc cref="IIncludeUnitsSyntax.IncludedUnitsElements" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IIncludeUnitsSyntax.StackingMode" path="/summary"/></param>
    public IncludeUnitsSyntax(Location includedUnitsCollation, IReadOnlyList<Location> includedUnitsElements, Location stackingMode)
    {
        IncludedUnitsCollection = includedUnitsCollation;
        IncludedUnitsElements = includedUnitsElements;
        StackingMode = stackingMode;
    }

    Location IIncludeUnitsSyntax.IncludedUnitsCollection => IncludedUnitsCollection;
    IReadOnlyList<Location> IIncludeUnitsSyntax.IncludedUnitsElements => IncludedUnitsElements;
    Location IIncludeUnitsSyntax.StackingMode => StackingMode;
}
