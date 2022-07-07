namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System.Collections.Generic;

public class ExcludeUnitsProcesser : AUnitListProcesser<RawExcludeUnitsDefinition, ExcludeUnitsLocations, ExcludeUnitsDefinition>
{
    public ExcludeUnitsProcesser(IUnitListProcessingDiagnostics<RawExcludeUnitsDefinition, ExcludeUnitsLocations> diagnostics) : base(diagnostics) { }

    protected override ExcludeUnitsDefinition ConstructProduct(IReadOnlyList<string> items, RawExcludeUnitsDefinition definition) => new(items, definition.Locations);
}
