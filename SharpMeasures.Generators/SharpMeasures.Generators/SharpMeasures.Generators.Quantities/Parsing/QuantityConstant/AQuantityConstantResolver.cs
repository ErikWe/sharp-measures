namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

public interface IQuantityConstantResolutionDiagnostics<TDefinition, TLocations>
    where TDefinition : AUnresolvedQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public abstract Diagnostic? UnrecognizedUnit(IQuantityConstantResolutionContext context, TDefinition definition);
}

public interface IQuantityConstantResolutionContext : IProcessingContext
{
    public abstract IUnresolvedUnitType Unit { get; }
}

public abstract class AQuantityConstantResolver<TContext, TDefinition, TLocations, TProduct> : AProcesser<TContext, TDefinition, TProduct>
    where TContext : IQuantityConstantResolutionContext
    where TDefinition : AUnresolvedQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
    where TProduct : AQuantityConstantDefinition<TLocations>
{
    private IQuantityConstantResolutionDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AQuantityConstantResolver(IQuantityConstantResolutionDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected IOptionalWithDiagnostics<IUnresolvedUnitInstance> ResolveUnit(TContext context, TDefinition definition)
    {
        if (context.Unit.UnitsByName.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedUnitInstance>(Diagnostics.UnrecognizedUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }
}
