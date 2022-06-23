namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System.Collections.Generic;

internal class ExcludeBasesProcesser : AUnitListProcesser<RawExcludeBasesDefinition, ExcludeBasesDefinition>
{
    public ExcludeBasesProcesser(IUnitListProcessingDiagnostics<RawExcludeBasesDefinition> diagnostics) : base(diagnostics) { }

    protected override ExcludeBasesDefinition ConstructProduct(IReadOnlyList<string> items, RawExcludeBasesDefinition definition) => new(items, definition.Locations);
}
