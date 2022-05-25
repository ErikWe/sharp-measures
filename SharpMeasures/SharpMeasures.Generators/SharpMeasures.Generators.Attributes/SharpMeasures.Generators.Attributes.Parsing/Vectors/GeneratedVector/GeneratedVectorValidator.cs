namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;

public interface IGeneratedVectorDiagnostics
{
    public abstract Diagnostic? InvalidUnit(IValidatorContext context, GeneratedVectorDefinition definition);
    public abstract Diagnostic? InvalidScalar(IValidatorContext context, GeneratedVectorDefinition definition);
}

public class GeneratedVectorValidator : AValidator<IValidatorContext, GeneratedVectorDefinition>
{
    private IGeneratedVectorDiagnostics Diagnostics { get; }

    public GeneratedVectorValidator(IGeneratedVectorDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IValidatorContext context, GeneratedVectorDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckScalarValidity);
    }

    private IValidityWithDiagnostics CheckUnitValidity(IValidatorContext context, GeneratedVectorDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Unit.Name))
        {
            return CreateInvalidity(Diagnostics.InvalidUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckScalarValidity(IValidatorContext context, GeneratedVectorDefinition definition)
    {
        if (definition.ParsingData.SpecifiedScalar is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (string.IsNullOrEmpty(definition.Scalar.Name))
        {
            return CreateInvalidity(Diagnostics.InvalidScalar(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
