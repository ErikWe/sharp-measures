﻿namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivableUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptyDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? DuplicateDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition);
    public abstract Diagnostic? NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index);
}

internal interface IDerivableUnitProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedIDs { get; }
}

internal class DerivableUnitProcesser : AActionableProcesser<IDerivableUnitProcessingContext, RawDerivableUnitDefinition, UnresolvedDerivableUnitDefinition>
{
    private IDerivableUnitProcessingDiagnostics Diagnostics { get; }

    public DerivableUnitProcesser(IDerivableUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, UnresolvedDerivableUnitDefinition product)
    {
        context.ReservedIDs.Add(product.DerivationID);
    }

    public override IOptionalWithDiagnostics<UnresolvedDerivableUnitDefinition> Process(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivableUnitDefinition>();
        }

        var expressionValidity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDerivationIDValidity, CheckExpressionValidity);
        IEnumerable<Diagnostic> allDiagnostics = expressionValidity.Diagnostics;

        if (expressionValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivableUnitDefinition>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature.Diagnostics);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedDerivableUnitDefinition>(allDiagnostics);
        }

        UnresolvedDerivableUnitDefinition product = new(definition.DerivationID!, definition.Expression!, processedSignature.Result, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckDerivationIDValidity(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        if (definition.DerivationID is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullDerivationID(context, definition));
        }

        if (definition.DerivationID.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyDerivationID(context, definition));
        }

        if (context.ReservedIDs.Contains(definition.DerivationID))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateDerivationID(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        if (definition.Expression is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullExpression(context, definition));
        }

        if (definition.Expression.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyExpression(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<UnresolvedUnitDerivationSignature> ProcessSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition)
    {
        if (definition.Signature is null)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitDerivationSignature>(Diagnostics.NullSignature(context, definition));
        }

        if (definition.Signature.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedUnitDerivationSignature>(Diagnostics.EmptySignature(context, definition));
        }

        NamedType[] definiteSignature = new NamedType[definition.Signature.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is not NamedType signatureComponent)
            {
                return OptionalWithDiagnostics.Empty<UnresolvedUnitDerivationSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }

            definiteSignature[i] = signatureComponent;
        }

        var derivableSignature = new UnresolvedUnitDerivationSignature(definiteSignature);

        return OptionalWithDiagnostics.Result(derivableSignature);
    }

    private static bool VerifyRequiredPropertiesSet(RawDerivableUnitDefinition definition)
    {
        return definition.Locations.ExplicitlySetDerivationID && definition.Locations.ExplicitlySetExpression;
    }
}
