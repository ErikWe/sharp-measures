namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System.Collections.Generic;

public class IncludeUnitsProcesser : AUnitListProcesser<RawIncludeUnitsDefinition, IncludeUnitsLocations, IncludeUnitsDefinition>
{
    public IncludeUnitsProcesser(IUnitListProcessingDiagnostics<RawIncludeUnitsDefinition, IncludeUnitsLocations> diagnostics) : base(diagnostics) { }

    protected override IncludeUnitsDefinition ConstructProduct(IReadOnlyList<string> items, RawIncludeUnitsDefinition definition) => new(items, definition.Locations);
}
