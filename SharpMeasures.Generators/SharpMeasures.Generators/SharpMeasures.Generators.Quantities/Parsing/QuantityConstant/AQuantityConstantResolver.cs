namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

public interface IQuantityConstantResolutionDiagnostics<TDefinition, TLocations>
    where TDefinition : ARawQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public abstract Diagnostic? UnrecognizedUnit(IQuantityConstantResolutionContext context, TDefinition definition);
}

public interface IQuantityConstantResolutionContext : IProcessingContext
{
    public abstract IRawUnitType Unit { get; }
}

public abstract class AQuantityConstantResolver<TContext, TDefinition, TLocations, TProduct> : AProcesser<TContext, TDefinition, TProduct>
    where TContext : IQuantityConstantResolutionContext
    where TDefinition : ARawQuantityConstantDefinition<TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
    where TProduct : AQuantityConstantDefinition<TLocations>
{
    private IQuantityConstantResolutionDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AQuantityConstantResolver(IQuantityConstantResolutionDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected IOptionalWithDiagnostics<IRawUnitInstance> ResolveUnit(TContext context, TDefinition definition)
    {
        var unitCorrectlyResolved = context.Unit.UnitsByName.TryGetValue(definition.Unit, out var unit);

        return OptionalWithDiagnostics.Conditional(unitCorrectlyResolved, unit, () => Diagnostics.UnrecognizedUnit(context, definition));
    }
}
