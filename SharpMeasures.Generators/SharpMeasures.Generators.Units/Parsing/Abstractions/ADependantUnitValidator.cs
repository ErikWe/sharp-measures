namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

internal interface IDependantUnitValidationDiagnostics<in TDefinition, in TLocations>
    where TDefinition : IDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract Diagnostic? UnrecognizedDependency(IDependantUnitValidationContext context, TDefinition definition);
    public abstract Diagnostic? CyclicDependency(IDependantUnitValidationContext context, TDefinition definition);
}

internal interface IDependantUnitValidationContext : IValidationContext
{
    public abstract IReadOnlyDictionary<string, IUnitInstance> UnitsByName { get; }

    public abstract HashSet<IDependantUnitInstance> CyclicDependantUnits { get; } 
}

internal abstract class ADependantUnitValidator<TContext, TDefinition, TLocations> : AActionableValidator<TContext, TDefinition>
    where TContext : IDependantUnitValidationContext
    where TDefinition : IDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    private IDependantUnitValidationDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected ADependantUnitValidator(IDependantUnitValidationDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(TContext context, TDefinition definition)
    {
        return ValidateDependantOnIsUnit(context, definition)
            .Validate(() => ValidateNotCyclicDependency(context, definition));
    }

    private IValidityWithDiagnostics ValidateDependantOnIsUnit(TContext context, TDefinition definition)
    {
        var dependantOnCorrectlyResolved = context.UnitsByName.ContainsKey(definition.DependantOn);

        return ValidityWithDiagnostics.Conditional(dependantOnCorrectlyResolved, () => Diagnostics.UnrecognizedDependency(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotCyclicDependency(TContext context, TDefinition definition)
    {
        var dependantUnitHasCyclicDependency = context.CyclicDependantUnits.Contains(definition);

        return ValidityWithDiagnostics.Conditional(dependantUnitHasCyclicDependency is false, () => Diagnostics.CyclicDependency(context, definition));
    }
}
