namespace SharpMeasures.Generators.Vectors.Refinement.UnitInclusion;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Refinement;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface IUnitInclusionRefinementDiagnostics
{
    public abstract Diagnostic? UnitAlreadyIncluded(IUnitInclusionRefinementContext context, string unitName, Location location);
    public abstract Diagnostic? UnitAlreadyExcluded(IUnitInclusionRefinementContext context, string unitName, Location location);
}

internal interface IUnitInclusionRefinementContext : IProcessingContext
{
    public abstract InclusionPopulation<string> InclusionPopulation { get; }
}

internal class UnitInclusionRefiner : IProcesser<IUnitInclusionRefinementContext, IDataModel, RefinedUnitInclusion>
{
    private IUnitInclusionRefinementDiagnostics Diagnostics { get; }

    public UnitInclusionRefiner(IUnitInclusionRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedUnitInclusion> Process(IUnitInclusionRefinementContext context, IDataModel definition)
    {
        List<Diagnostic> allDiagnostics = new();

        if (context.InclusionPopulation.TryGetValue(context.Type.AsNamedType(), out var resolvedInclusions) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedUnitInclusion>();
        }

        if (definition.IncludeUnits.Any())
        {
            AddRedundantDiagnostics(context, allDiagnostics, resolvedInclusions, definition.IncludeUnits.SelectMany(static (x) => x.IncludedUnits),
                definition.IncludeUnits.SelectMany(static (x) => x.Locations.IncludedUnitsElements), Diagnostics.UnitAlreadyIncluded);
        }
        else
        {
            AddRedundantDiagnostics(context, allDiagnostics, resolvedInclusions, definition.ExcludeUnits.SelectMany(static (x) => x.ExcludedUnits),
                definition.ExcludeUnits.SelectMany(static (x) => x.Locations.ExcludedUnitsElements), Diagnostics.UnitAlreadyExcluded);
        }
    }

    private static void AddRedundantDiagnostics(IUnitInclusionRefinementContext context, List<Diagnostic> diagnostics, List<UnitInstance> units,
        ResolvedInclusion<string> inclusion, IEnumerable<string> items, IEnumerable<MinimalLocation> itemLocations,
        Func<IUnitInclusionRefinementContext, string, Location, Diagnostic?> diagnosticsConstructor)
    {
        var redundantIndexEnumerator = inclusion.RedundantIndices.GetEnumerator();
        redundantIndexEnumerator.MoveNext();

        IEnumerator<string> itemEnumerator = items.GetEnumerator();
        IEnumerator<MinimalLocation> locationEnumerator = itemLocations.GetEnumerator();

        int index = 0;
        while (itemEnumerator.MoveNext() && locationEnumerator.MoveNext())
        {
            if (index == redundantIndexEnumerator.Current)
            {
                if (diagnosticsConstructor(context, itemEnumerator.Current, locationEnumerator.Current.AsRoslynLocation()) is Diagnostic diagnostic)
                {
                    diagnostics.Add(diagnostic);
                }
            }
        }
    }
}
