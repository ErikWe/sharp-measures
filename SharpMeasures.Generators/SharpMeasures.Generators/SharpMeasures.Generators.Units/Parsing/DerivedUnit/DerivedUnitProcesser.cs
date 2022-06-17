namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<RawDerivedUnitDefinition>
{
    public abstract Diagnostic? EmptySignature(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? IncompatibleSignatureAndUnitLists(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedSignature(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
    public abstract Diagnostic? NullUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
    public abstract Diagnostic? EmptyUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index);
}

internal interface IDerivedUnitProcessingContext : IUnitProcessingContext
{
    public abstract HashSet<DerivableSignature> AvailableSignatures { get; }
}

internal class DerivedUnitProcesser : AUnitProcesser<IDerivedUnitProcessingContext, RawDerivedUnitDefinition, DerivedUnitDefinition>
{
    private IDerivedUnitProcessingDiagnostics Diagnostics { get; }

    public DerivedUnitProcesser(IDerivedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedUnitDefinition> Process(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckSignatureValidity);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        var processedUnits = ProcessUnits(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedUnits);

        if (processedUnits.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        DerivedUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, processedSignature.Result,
            processedUnits.Result, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<DerivableSignature> ProcessSignature(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        NamedType[] definiteSignature = new NamedType[definition.Signature.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is not NamedType signatureElement)
            {
                return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }

            definiteSignature[i] = signatureElement;
        }

        DerivableSignature signature = new(definiteSignature);

        if (context.AvailableSignatures.Contains(signature) is false)
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.UnrecognizedSignature(context, definition));
        }

        return OptionalWithDiagnostics.Result(signature);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        string[] units = new string[definition.Units.Count];

        for (int i = 0; i < definition.Units.Count; i++)
        {
            if (definition.Units[i] is not string unit)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.NullUnitElement(context, definition, i));
            }

            if (unit.Length is 0)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.EmptyUnitElement(context, definition, i));
            }

            units[i] = unit;
        }

        return OptionalWithDiagnostics.Result(units as IReadOnlyList<string>);
    }

    private IValidityWithDiagnostics CheckSignatureValidity(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        if (definition.Signature.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptySignature(context, definition));
        }

        if (definition.Signature.Count != definition.Units.Count)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.IncompatibleSignatureAndUnitLists(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
