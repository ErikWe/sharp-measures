namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System.Collections.Generic;

internal class IncludeBasesProcesser : AUnitListProcesser<RawIncludeBasesDefinition, IncludeBasesLocations, IncludeBasesDefinition>
{
    public IncludeBasesProcesser(IUnitListProcessingDiagnostics<RawIncludeBasesDefinition, IncludeBasesLocations> diagnostics) : base(diagnostics) { }

    protected override IncludeBasesDefinition ConstructProduct(IReadOnlyList<string> items, RawIncludeBasesDefinition definition) => new(items, definition.Locations);
}
