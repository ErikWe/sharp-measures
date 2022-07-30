namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitResolutionDiagnostics
{
    public abstract Diagnostic? UnitNotDerivable(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition);
    public abstract Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedSignatureID(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition);
    public abstract Diagnostic? InvalidUnitListLength(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition,
        UnresolvedUnitDerivationSignature signature);
    public abstract Diagnostic? UnrecognizedUnit(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition, int index, IUnresolvedUnitType unitType);
}

internal interface IDerivedUnitResolutionContext : IProcessingContext
{
    public abstract IReadOnlyDictionary<string, IUnresolvedDerivableUnit> DerivationsByID { get; }
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
}

internal class DerivedUnitResolver : AProcesser<IDerivedUnitResolutionContext, UnresolvedDerivedUnitDefinition, DerivedUnitDefinition>
{
    private IDerivedUnitResolutionDiagnostics Diagnostics { get; }

    public DerivedUnitResolver(IDerivedUnitResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedUnitDefinition> Process(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition)
    {
        var signatureIDValidity = CheckSignatureIDValidity(context, definition);
        var allDiagnostics = signatureIDValidity.Diagnostics;

        if (signatureIDValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature.Diagnostics);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        var processedUnits = ProcessUnits(context, definition, processedSignature.Result);
        allDiagnostics = allDiagnostics.Concat(processedUnits.Diagnostics);

        if (processedUnits.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivedUnitDefinition>(allDiagnostics);
        }

        DerivedUnitDefinition product = new(definition.Name, definition.Plural, processedSignature.Result, processedUnits.Result, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckSignatureIDValidity(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition)
    {
        if (context.DerivationsByID.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnitNotDerivable(context, definition));
        }

        if (definition.DerivationID is null || definition.DerivationID.Length is 0)
        {
            if (context.DerivationsByID.Count is not 1)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.AmbiguousSignatureNotSpecified(context, definition));
            }

            return ValidityWithDiagnostics.Valid;
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<UnresolvedUnitDerivationSignature> ProcessSignature(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition)
    {
        if (definition.DerivationID is null || definition.DerivationID.Length is 0)
        {
            return OptionalWithDiagnostics.Result(context.DerivationsByID.Values.First().Signature);
        }

        if (context.DerivationsByID.TryGetValue(definition.DerivationID, out var derivation) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitDerivationSignature>(Diagnostics.UnrecognizedSignatureID(context, definition));
        }

        return OptionalWithDiagnostics.Result(derivation.Signature);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<IUnresolvedUnitInstance>> ProcessUnits(IDerivedUnitResolutionContext context, UnresolvedDerivedUnitDefinition definition,
        UnresolvedUnitDerivationSignature signature)
    {
        if (definition.Units.Count != signature.Count)
        {
            return OptionalWithDiagnostics.Empty<IReadOnlyList<IUnresolvedUnitInstance>>(Diagnostics.InvalidUnitListLength(context, definition, signature));
        }

        var units = new IUnresolvedUnitInstance[definition.Units.Count];

        for (int i = 0; i < definition.Units.Count; i++)
        {
            if (context.UnitPopulation.Units.TryGetValue(signature[i], out var signatureUnit) is false)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<IUnresolvedUnitInstance>>();
            }

            if (signatureUnit.UnitsByName.TryGetValue(definition.Units[i], out var signatureUnitInstance) is false)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<IUnresolvedUnitInstance>>(Diagnostics.UnrecognizedUnit(context, definition, i, signatureUnit));
            }

            units[i] = signatureUnitInstance;
        }

        return OptionalWithDiagnostics.Result(units as IReadOnlyList<IUnresolvedUnitInstance>);
    }
}
