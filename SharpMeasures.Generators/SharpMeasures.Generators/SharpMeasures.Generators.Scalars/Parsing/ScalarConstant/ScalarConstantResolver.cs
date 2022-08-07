namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal class ScalarConstantResolver : AQuantityConstantResolver<IQuantityConstantResolutionContext, UnresolvedScalarConstantDefinition, ScalarConstantLocations, ScalarConstantDefinition>
{
    public ScalarConstantResolver(IQuantityConstantResolutionDiagnostics<UnresolvedScalarConstantDefinition, ScalarConstantLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScalarConstantDefinition> Process(IQuantityConstantResolutionContext context, UnresolvedScalarConstantDefinition definition)
    {
        var processedUnit = ResolveUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ScalarConstantDefinition>(allDiagnostics);
        }

        ScalarConstantDefinition product = new(definition.Name, processedUnit.Result, definition.Value, definition.GenerateMultiplesProperty, definition.Multiples,
            definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
