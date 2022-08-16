namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

internal interface IDependantUnitResolutionDiagnostics<in TDefinition, in TLocations>
    where TDefinition : IRawUnitDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public abstract Diagnostic? UnrecognizedDependency(IDependantUnitResolutionContext context, TDefinition definition);
}

internal interface IDependantUnitResolutionContext : IProcessingContext
{
    public abstract IReadOnlyDictionary<string, IRawUnitInstance> UnitsByName { get; }
}

internal abstract class ADependantUnitResolver<TContext, TDefinition, TLocations, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IDependantUnitResolutionContext
    where TDefinition : IRawDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
    where TProduct : IDependantUnitDefinition<TLocations>
{
    private IDependantUnitResolutionDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected ADependantUnitResolver(IDependantUnitResolutionDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected IOptionalWithDiagnostics<IRawUnitInstance> ProcessDependantOn(TContext context, TDefinition definition)
    {
        var dependantOnCorrectlyResolved = context.UnitsByName.TryGetValue(definition.DependantOn, out var dependantOn);

        return OptionalWithDiagnostics.Conditional(dependantOnCorrectlyResolved, dependantOn, () => Diagnostics.UnrecognizedDependency(context, definition));
    }
}
