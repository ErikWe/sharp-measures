namespace SharpMeasures.Generators.Parsing.Scalars.ExcludeUnitBasesAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IExcludeUnitBasesSyntax"/>
internal sealed record class ExcludeUnitBasesSyntax : IExcludeUnitBasesSyntax
{
    private Location ExcludedUnitBasesCollection { get; }
    private IReadOnlyList<Location> ExcludeUnitBasesElements { get; }
    private Location StackingMode { get; }

    /// <summary>Instantiates a <see cref="ExcludeUnitBasesSyntax"/>, representing syntactical information about a parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
    /// <param name="excludedUnitBasesCollection"><inheritdoc cref="IExcludeUnitBasesSyntax.ExcludedUnitBasesCollection" path="/summary"/></param>
    /// <param name="excludeUnitBasesElements"><inheritdoc cref="IExcludeUnitBasesSyntax.ExcludedUnitBasesElements" path="/summary"/></param>
    /// <param name="stackingMode"><inheritdoc cref="IExcludeUnitBasesSyntax.StackingMode" path="/summary"/></param>
    public ExcludeUnitBasesSyntax(Location excludedUnitBasesCollection, IReadOnlyList<Location> excludeUnitBasesElements, Location stackingMode)
    {
        ExcludedUnitBasesCollection = excludedUnitBasesCollection;
        ExcludeUnitBasesElements = excludeUnitBasesElements;
        StackingMode = stackingMode;
    }

    Location IExcludeUnitBasesSyntax.ExcludedUnitBasesCollection => ExcludedUnitBasesCollection;
    IReadOnlyList<Location> IExcludeUnitBasesSyntax.ExcludedUnitBasesElements => ExcludeUnitBasesElements;
    Location IExcludeUnitBasesSyntax.StackingMode => StackingMode;
}
