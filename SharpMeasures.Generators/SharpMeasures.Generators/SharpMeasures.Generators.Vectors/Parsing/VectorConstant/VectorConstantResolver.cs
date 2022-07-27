namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Linq;

internal interface IVectorConstantResolutionDiagnostics : IQuantityConstantResolutionDiagnostics<UnresolvedVectorConstantDefinition, VectorConstantLocations>
{
    public abstract Diagnostic? InvalidConstantDimensionality(IVectorConstantResolutionContext context, UnresolvedVectorConstantDefinition definition);
}

internal interface IVectorConstantResolutionContext : IQuantityConstantResolutionContext
{
    public abstract int Dimension { get; }
}

internal class VectorConstantResolver
    : AQuantityConstantResolver<IVectorConstantResolutionContext, UnresolvedVectorConstantDefinition, VectorConstantLocations, VectorConstantDefinition>
{
    private IVectorConstantResolutionDiagnostics Diagnostics { get; }

    public VectorConstantResolver(IVectorConstantResolutionDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<VectorConstantDefinition> Process(IVectorConstantResolutionContext context, UnresolvedVectorConstantDefinition definition)
    {
        var processedUnit = ResolveUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<VectorConstantDefinition>(allDiagnostics);
        }

        var validity = CheckValueValidity(context, definition);
        allDiagnostics = allDiagnostics.Concat(validity.Diagnostics);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<VectorConstantDefinition>(allDiagnostics);
        }

        VectorConstantDefinition product = new(definition.Name, processedUnit.Result, definition.Value, definition.GenerateMultiplesProperty, definition.Multiples,
            definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckValueValidity(IVectorConstantResolutionContext context, UnresolvedVectorConstantDefinition definition)
    {
        if (definition.Value.Count != context.Dimension)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidConstantDimensionality(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
