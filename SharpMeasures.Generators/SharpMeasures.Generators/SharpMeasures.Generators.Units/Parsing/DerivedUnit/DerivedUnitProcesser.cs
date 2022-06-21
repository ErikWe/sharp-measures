namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<RawDerivedUnitDefinition>
{
    public abstract Diagnostic? UnitNotDerivable(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedSignatureID(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? InvalidUnitListLength(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, DerivableSignature signature);
    public abstract Diagnostic? NullUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index, DerivableSignature signature);
    public abstract Diagnostic? EmptyUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition, int index, DerivableSignature signature);
}

internal interface IDerivedUnitProcessingContext : IUnitProcessingContext
{
    public abstract Dictionary<string, DerivableSignature> AvailableSignatureIDs { get; }
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
        if (context.AvailableSignatureIDs.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(Diagnostics.UnitNotDerivable(context, definition));
        }

        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>();
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckSignatureIDValidity);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        var processedUnits = ProcessUnits(context, definition, processedSignature.Result);
        allDiagnostics = allDiagnostics.Concat(processedUnits);

        if (processedUnits.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        DerivedUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, processedSignature.Result,
            processedUnits.Result, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<DerivableSignature> ProcessSignature(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        if (definition.SignatureID is null || definition.SignatureID.Length is 0)
        {
            return OptionalWithDiagnostics.Result(context.AvailableSignatureIDs.First().Value);
        }

        if (context.AvailableSignatureIDs.TryGetValue(definition.SignatureID, out var signature) is false)
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.UnrecognizedSignatureID(context, definition));
        }

        return OptionalWithDiagnostics.Result(signature);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition,
        DerivableSignature signature)
    {
        if (definition.Units.Count != signature.Count)
        {
            return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.InvalidUnitListLength(context, definition, signature));
        }

        string[] units = new string[definition.Units.Count];

        for (int i = 0; i < definition.Units.Count; i++)
        {
            if (definition.Units[i] is not string unit)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.NullUnitElement(context, definition, i, signature));
            }

            if (unit.Length is 0)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.EmptyUnitElement(context, definition, i, signature));
            }

            units[i] = unit;
        }

        return OptionalWithDiagnostics.Result(units as IReadOnlyList<string>);
    }

    private IValidityWithDiagnostics CheckSignatureIDValidity(IDerivedUnitProcessingContext context, RawDerivedUnitDefinition definition)
    {
        if (definition.SignatureID is null || definition.SignatureID.Length is 0)
        {
            if (context.AvailableSignatureIDs.Count is not 1)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.AmbiguousSignatureNotSpecified(context, definition));
            }

            return ValidityWithDiagnostics.Valid;
        }

        return ValidityWithDiagnostics.Valid;
    }
}
