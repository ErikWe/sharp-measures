namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IScalarConstantDiagnostics
{
    public abstract Diagnostic? NameNullOrEmpty(IScalarConstantValidatorContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? DuplicateName(IScalarConstantValidatorContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? UnitNullOrEmpty(IScalarConstantValidatorContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? InvalidMultiplesName(IScalarConstantValidatorContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? DuplicateMultiplesName(IScalarConstantValidatorContext context, ScalarConstantDefinition definition);
}

public interface IScalarConstantValidatorContext : IValidatorContext
{
    public abstract HashSet<string> ReservedConstants { get; }
    public abstract HashSet<string> ReservedConstantMultiples { get; }
}

public class ScalarConstantValidator : AActionableValidator<IScalarConstantValidatorContext, ScalarConstantDefinition>
{
    private IScalarConstantDiagnostics Diagnostics { get; }

    public ScalarConstantValidator(IScalarConstantDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnValidated(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        context.ReservedConstants.Add(definition.Name);
        context.ReservedConstantMultiples.Add(definition.ParsingData.InterpretedMultiplesName);
    }

    public override IValidityWithDiagnostics CheckValidity(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, CheckNameValidity, CheckUnitValidity, CheckMultiplesNameValidity);
    }

    private IValidityWithDiagnostics CheckNameValidity(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Name))
        {
            return CreateInvalidity(Diagnostics.NameNullOrEmpty(context, definition));
        }

        if (context.ReservedConstants.Contains(definition.Name))
        {
            return CreateInvalidity(Diagnostics.DuplicateName(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckUnitValidity(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Unit))
        {
            return CreateInvalidity(Diagnostics.UnitNullOrEmpty(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckMultiplesNameValidity(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        if (definition.GenerateMultiplesProperty && string.IsNullOrEmpty(definition.ParsingData.InterpretedMultiplesName))
        {
            return CreateInvalidity(Diagnostics.InvalidMultiplesName(context, definition));
        }

        if (context.ReservedConstantMultiples.Contains(definition.ParsingData.InterpretedMultiplesName))
        {
            return CreateInvalidity(Diagnostics.DuplicateMultiplesName(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
