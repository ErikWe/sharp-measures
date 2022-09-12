namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System;
using System.Collections.Generic;

internal interface IScalarConstantValidationDiagnostics : IQuantityConstantValidationDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>
{
    public abstract Diagnostic? NameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? MultiplesNameReservedByUnitInstanceName(IScalarConstantValidationContext context, ScalarConstantDefinition definition);
}

internal interface IScalarConstantValidationContext : IQuantityConstantValidationContext
{
    public abstract HashSet<string> IncludedUnitInstanceNames { get; }
}

internal class ScalarConstantValidator : AQuantityConstantValidator<IScalarConstantValidationContext, ScalarConstantDefinition, ScalarConstantLocations>
{
    private IScalarConstantValidationDiagnostics Diagnostics { get; }

    public ScalarConstantValidator(IScalarConstantValidationDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override ScalarConstantDefinition ProduceResult(ScalarConstantDefinition definition, bool generateMultiples)
    {
        return new(definition.Name, definition.UnitInstanceName, definition.Value, generateMultiples, definition.Multiples, definition.Locations);
    }

    protected override IValidityWithDiagnostics ValidateConstant(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        return base.ValidateConstant(context, definition)
            .Validate(() => ValidateNameNotReservedByUnit(context, definition));
    }

    protected override IValidityWithDiagnostics ValidateMultiples(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        return base.ValidateMultiples(context, definition)
            .Validate(() => ValidateMultiplesNotReservedByUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotReservedByUnit(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        var nameReservedByUnitInstanceName = definition.Name.StartsWith("One", StringComparison.InvariantCulture) && context.IncludedUnitInstanceNames.Contains(definition.Name.Substring(3));

        return ValidityWithDiagnostics.Conditional(nameReservedByUnitInstanceName is false, () => Diagnostics.NameReservedByUnitInstanceName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMultiplesNotReservedByUnit(IScalarConstantValidationContext context, ScalarConstantDefinition definition)
    {
        var multiplesReservedByUnitInstanceName = definition.GenerateMultiplesProperty && definition.Multiples!.StartsWith("One", StringComparison.InvariantCulture)
            && context.IncludedUnitInstanceNames.Contains(definition.Multiples.Substring(3));

        return ValidityWithDiagnostics.Conditional(multiplesReservedByUnitInstanceName is false, () => Diagnostics.MultiplesNameReservedByUnitInstanceName(context, definition));
    }
}
