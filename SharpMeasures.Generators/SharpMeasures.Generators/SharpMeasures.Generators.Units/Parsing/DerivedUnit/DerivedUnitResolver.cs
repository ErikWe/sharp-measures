namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitResolutionDiagnostics
{
    public abstract Diagnostic? UnitNotDerivable(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedDerivationID(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition);
    public abstract Diagnostic? InvalidUnitListLength(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition, RawUnitDerivationSignature signature);
    public abstract Diagnostic? UnrecognizedUnit(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition, int index, IRawUnitType unitType);
}

internal interface IDerivedUnitResolutionContext : IProcessingContext
{
    public abstract IRawDerivableUnit? UnnamedDerivation { get; }
    public abstract IReadOnlyDictionary<string, IRawDerivableUnit> DerivationsByID { get; }
    public abstract IRawUnitPopulation UnitPopulation { get; }
}

internal class DerivedUnitResolver : AProcesser<IDerivedUnitResolutionContext, RawDerivedUnitDefinition, DerivedUnitDefinition>
{
    private IDerivedUnitResolutionDiagnostics Diagnostics { get; }

    public DerivedUnitResolver(IDerivedUnitResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedUnitDefinition> Process(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition)
    {
        var signature = CheckSignatureIDValidity(context, definition)
            .Merge(() => ProcessSignature(context, definition));

        return signature.Reduce().Merge(() => ProcessUnits(context, definition, signature.Result))
            .Transform((units) => ProduceResult(definition, signature.Result, units));
    }

    private static DerivedUnitDefinition ProduceResult(RawDerivedUnitDefinition definition, RawUnitDerivationSignature signature, IReadOnlyList<IRawUnitInstance> units)
    {
        return new(definition.Name, definition.Plural, signature, units, definition.Locations);
    }

    private IValidityWithDiagnostics CheckSignatureIDValidity(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition)
    {
        if (context.DerivationsByID.Count is 0 && context.UnnamedDerivation is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnitNotDerivable(context, definition));
        }

        if (definition.DerivationID is null || definition.DerivationID.Length is 0)
        {
            if (context.DerivationsByID.Count is not 1 && context.UnnamedDerivation is null)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.AmbiguousSignatureNotSpecified(context, definition));
            }

            return ValidityWithDiagnostics.Valid;
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<RawUnitDerivationSignature> ProcessSignature(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition)
    {
        if (definition.DerivationID is null || definition.DerivationID.Length is 0)
        {
            if (context.UnnamedDerivation is not null)
            {
                return OptionalWithDiagnostics.Result(context.UnnamedDerivation.Signature);
            }

            return OptionalWithDiagnostics.Result(context.DerivationsByID.Values.First().Signature);
        }

        if (context.DerivationsByID.TryGetValue(definition.DerivationID, out var derivation) is false)
        {
            return OptionalWithDiagnostics.Empty<RawUnitDerivationSignature>(Diagnostics.UnrecognizedDerivationID(context, definition));
        }

        return OptionalWithDiagnostics.Result(derivation.Signature);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<IRawUnitInstance>> ProcessUnits(IDerivedUnitResolutionContext context, RawDerivedUnitDefinition definition,
        RawUnitDerivationSignature signature)
    {
        if (definition.Units.Count != signature.Count)
        {
            return OptionalWithDiagnostics.Empty<IReadOnlyList<IRawUnitInstance>>(Diagnostics.InvalidUnitListLength(context, definition, signature));
        }

        var units = new IRawUnitInstance[definition.Units.Count];

        for (int i = 0; i < definition.Units.Count; i++)
        {
            if (context.UnitPopulation.Units.TryGetValue(signature[i], out var signatureUnit) is false)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<IRawUnitInstance>>();
            }

            if (signatureUnit.UnitsByName.TryGetValue(definition.Units[i], out var signatureUnitInstance) is false)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<IRawUnitInstance>>(Diagnostics.UnrecognizedUnit(context, definition, i, signatureUnit));
            }

            units[i] = signatureUnitInstance;
        }

        return OptionalWithDiagnostics.Result(units as IReadOnlyList<IRawUnitInstance>);
    }
}
