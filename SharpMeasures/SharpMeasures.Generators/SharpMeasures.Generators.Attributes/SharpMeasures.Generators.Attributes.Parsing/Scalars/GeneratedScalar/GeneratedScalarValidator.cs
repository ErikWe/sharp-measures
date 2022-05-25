namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;

public interface IGeneratedScalarDiagnostics
{
    public abstract Diagnostic? InvalidUnit(IValidatorContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? InvalidVector(IValidatorContext context, GeneratedScalarDefinition definition);
}

public class GeneratedScalarValidator : AValidator<IValidatorContext, GeneratedScalarDefinition>
{
    private IGeneratedScalarDiagnostics Diagnostics { get; }

    public GeneratedScalarValidator(IGeneratedScalarDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IValidatorContext context, GeneratedScalarDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckVectorValidity);
    }

    private IValidityWithDiagnostics CheckUnitValidity(IValidatorContext context, GeneratedScalarDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Unit.Name))
        {
            return CreateInvalidity((Diagnostics.InvalidUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckVectorValidity(IValidatorContext context, GeneratedScalarDefinition definition)
    {
        if (definition.ParsingData.SpecifiedVector is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (string.IsNullOrEmpty(definition.Vector.Name))
        {
            return CreateInvalidity(Diagnostics.InvalidVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
