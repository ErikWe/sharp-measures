namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IDerivableUnitDiagnostics
{
    public abstract Diagnostic? NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnit definition);
    public abstract Diagnostic? EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnit definition);
    public abstract Diagnostic? EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnit definition);
    public abstract Diagnostic? NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnit definition, int index);
    public abstract Diagnostic? DuplicateSignature(IDerivableUnitProcessingContext context, RawDerivableUnit definition);
}

public interface IDerivableUnitProcessingContext : IProcessingContext
{
    public abstract HashSet<DerivableSignature> ReservedSignatures { get; }
}

public class DerivableUnitProcesser : AActionableProcesser<IDerivableUnitProcessingContext, RawDerivableUnit, DerivableUnit>
{
    private IDerivableUnitDiagnostics Diagnostics { get; }

    public DerivableUnitProcesser(IDerivableUnitDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDerivableUnitProcessingContext context, RawDerivableUnit definition, DerivableUnit product)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        context.ReservedSignatures.Add(product.Signature);
    }

    public override IOptionalWithDiagnostics<DerivableUnit> Process(IDerivableUnitProcessingContext context, RawDerivableUnit definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var expressionValidity = CheckExpressionValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = expressionValidity.Diagnostics;

        if (expressionValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DerivableUnit>(allDiagnostics);
        }

        var processedSignature = ProcessSignature(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedSignature.Diagnostics);

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivableUnit>(allDiagnostics);
        }

        DerivableUnit product = new(definition.Expression!, processedSignature.Result, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IDerivableUnitProcessingContext context, RawDerivableUnit definition)
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

    private IOptionalWithDiagnostics<DerivableSignature> ProcessSignature(IDerivableUnitProcessingContext context, RawDerivableUnit definition)
    {
        if (definition.Signature.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.EmptySignature(context, definition));
        }

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (definition.Signature[i] is null)
            {
                return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.NullSignatureElement(context, definition, i));
            }
        }

        var derivableSignature = DerivableSignature.ConstructFromDefinite(definition.Signature);

        if (context.ReservedSignatures.Contains(derivableSignature))
        {
            return OptionalWithDiagnostics.Empty<DerivableSignature>(Diagnostics.DuplicateSignature(context, definition));
        }

        return OptionalWithDiagnostics.Result(derivableSignature);
    }
}
