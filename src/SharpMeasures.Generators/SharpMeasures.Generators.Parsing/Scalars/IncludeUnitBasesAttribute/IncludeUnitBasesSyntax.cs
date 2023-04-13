namespace SharpMeasures.Generators.Parsing.Scalars.IncludeUnitBasesAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IIncludeUnitBasesSyntax"/>
internal sealed record class IncludeUnitBasesSyntax : IIncludeUnitBasesSyntax
{
    private Location IncludedUnitBasesCollection { get; }
    private IReadOnlyList<Location> IncludedUnitBasesElements { get; }
    private Location StackingMode { get; }

    /// <summary>Instantiates a <see cref="IncludeUnitBasesSyntax"/>, representing syntactical information about a parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
    /// <param name="includedUnitBasesCollection"><inheritdoc cref="IIncludeUnitBasesSyntax.IncludedUnitBasesCollection" path="/summary"/></param>
    /// <param name="includedUnitBasesElements"><inheritdoc cref="IIncludeUnitBasesSyntax.IncludedUnitBasesElements" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IIncludeUnitBasesSyntax.StackingMode" path="/summary"/></param>
    public IncludeUnitBasesSyntax(Location includedUnitBasesCollection, IReadOnlyList<Location> includedUnitBasesElements, Location stackingMode)
    {
        IncludedUnitBasesCollection = includedUnitBasesCollection;
        IncludedUnitBasesElements = includedUnitBasesElements;
        StackingMode = stackingMode;
    }

    Location IIncludeUnitBasesSyntax.IncludedUnitBasesCollection => IncludedUnitBasesCollection;
    IReadOnlyList<Location> IIncludeUnitBasesSyntax.IncludedUnitBasesElements => IncludedUnitBasesElements;
    Location IIncludeUnitBasesSyntax.StackingMode => StackingMode;
}
