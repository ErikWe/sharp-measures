namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IDerivedUnitDiagnostics : IUnitDiagnostics<RawDerivedUnit>
{
    public abstract Diagnostic? EmptySignature(IDerivedUnitProcessingContext context, RawDerivedUnit definition);
    public abstract Diagnostic? IncompatibleSignatureAndUnitLists(IDerivedUnitProcessingContext context, RawDerivedUnit definition);
    public abstract Diagnostic? UnrecognizedSignature(IDerivedUnitProcessingContext context, RawDerivedUnit definition);
    public abstract Diagnostic? NullSignatureElement(IDerivedUnitProcessingContext context, RawDerivedUnit definition, int index);
    public abstract Diagnostic? NullUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnit definition, int index);
    public abstract Diagnostic? EmptyUnitElement(IDerivedUnitProcessingContext context, RawDerivedUnit definition, int index);
}

public interface IDerivedUnitProcessingContext : IUnitProcessingContext
{
    public abstract HashSet<DerivableSignature> AvailableSignatures { get; }
}

public class DerivedUnitProcesser : AUnitProcesser<IDerivedUnitProcessingContext, RawDerivedUnit, DerivedUnit>
{
    private IDerivedUnitDiagnostics Diagnostics { get; }

    public DerivedUnitProcesser(IDerivedUnitDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedUnit> Process(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckSignatureValidity);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnit>(allDiagnostics);
        }

        var processedUnits = ProcessUnits(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedUnits);

        if (processedUnits.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnit>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnit>(allDiagnostics);
        }

        DerivedUnit product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, processedSignature.Result,
            processedUnits.Result, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<DerivableSignature> ProcessSignature(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
    {
        var signature = DerivableSignature.ConstructFromDefinite(definition.Signature);

        if (context.AvailableSignatures.Contains(signature) is false)
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.UnrecognizedSignature(context, definition));
        }

        return OptionalWithDiagnostics.Result(signature);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessUnits(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
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

    private IValidityWithDiagnostics CheckSignatureValidity(IDerivedUnitProcessingContext context, RawDerivedUnit definition)
    {
        if (definition.Signature.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptySignature(context, definition));
        }

        if (definition.Signature.Count != definition.Units.Count)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.IncompatibleSignatureAndUnitLists(context, definition));
        }

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is null)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.NullSignatureElement(context, definition, i));
            }
        }

        return ValidityWithDiagnostics.Valid;
    }
}
