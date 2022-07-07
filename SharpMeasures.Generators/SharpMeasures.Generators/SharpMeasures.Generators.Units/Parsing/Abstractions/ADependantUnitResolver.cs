namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal interface IDependantUnitResolutionDiagnostics<in TDefinition, in TLocations>
    where TDefinition : IUnresolvedUnitDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public abstract Diagnostic? UnrecognizedDependency(IDependantUnitResolutionContext context, TDefinition definition);
}

internal interface IDependantUnitResolutionContext : IProcessingContext
{
    public abstract IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByName { get; }
}

internal abstract class ADependantUnitResolver<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IDependantUnitResolutionContext
    where TDefinition : IUnresolvedDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
    where TProduct : IDependantUnitDefinition<TLocations>
{
    private IDependantUnitResolutionDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected ADependantUnitResolver(IDependantUnitResolutionDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected IOptionalWithDiagnostics<IUnresolvedUnitInstance> ProcessDependantOn(TContext context, TDefinition definition)
    {
        if (context.UnitsByName.TryGetValue(definition.DependantOn, out var dependantOn) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedUnitInstance>(Diagnostics.UnrecognizedDependency(context, definition));
        }

        return OptionalWithDiagnostics.Result(dependantOn);
    }
}
