﻿namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

public sealed class QuantityConstantProcessingDiagnostics<TDefinition, TLocations> : IQuantityConstantProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : ARawQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    private NamedType? Unit { get; }

    public QuantityConstantProcessingDiagnostics(NamedType unit)
    {
        Unit = unit;
    }

    public QuantityConstantProcessingDiagnostics()
    {
        Unit = null;
    }

    public Diagnostic NullName(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.NullConstantName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IQuantityConstantProcessingContext context, TDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NameReservedByMultiples(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantNameReservedByConstantMultiples(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnitInstanceName(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        if (Unit is null)
        {
            return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.Locations.UnitInstanceName?.AsRoslynLocation());
        }

        return DiagnosticConstruction.NullUnrecognizedUnitInstanceName(definition.Locations.UnitInstanceName?.AsRoslynLocation(), Unit.Value.Name);
    }

    public Diagnostic EmptyUnitInstanceName(IQuantityConstantProcessingContext context, TDefinition definition) => NullUnitInstanceName(context, definition);

    public Diagnostic NullMultiples(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.NullConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiples(IQuantityConstantProcessingContext context, TDefinition definition) => NullMultiples(context, definition);

    public Diagnostic InvalidMultiples(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!, definition.Name!);
    }

    public Diagnostic DuplicateMultiples(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), interpretedMultiples, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.AttributeName.AsRoslynLocation(), interpretedMultiples, context.Type.Name);
    }

    public Diagnostic MultiplesReservedByName(IQuantityConstantProcessingContext context, TDefinition definition, string interpretedMultiples)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantMultiplesNameReversedByConstantName(definition.Locations.Multiples?.AsRoslynLocation(), interpretedMultiples, context.Type.Name);
        }

        return DiagnosticConstruction.ConstantMultiplesNameReversedByConstantName(definition.Locations.AttributeName.AsRoslynLocation(), interpretedMultiples, context.Type.Name);
    }

    public Diagnostic NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantIdenticalNameAndMultiples(definition.Locations.AttributeName.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.ConstantMultiplesDisabledButNameSpecified(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.SetRegexSubstitutionButNotPattern(definition.Locations.MultiplesRegexSubstitution?.AsRoslynLocation());
    }
}
