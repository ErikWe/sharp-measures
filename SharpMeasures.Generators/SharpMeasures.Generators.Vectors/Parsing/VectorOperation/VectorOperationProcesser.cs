namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

internal interface IVectorOperationProcessingDiagnostics
{
    public abstract Diagnostic? NullResult(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);
    public abstract Diagnostic? NullOther(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);

    public abstract Diagnostic? UnrecognizedOperatorType(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);
    public abstract Diagnostic? UnrecognizedPosition(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);

    public abstract Diagnostic? MirrorNotSupported(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);

    public abstract Diagnostic? NullName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);
    public abstract Diagnostic? EmptyName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);

    public abstract Diagnostic? NullMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);
    public abstract Diagnostic? EmptyMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);
    public abstract Diagnostic? MirrorDisabledButNameSpecified(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition);

    public abstract Diagnostic? DuplicateName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, string name);
    public abstract Diagnostic? DuplicateMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, string name);
}

internal interface IVectorOperationProcessingContext : IProcessingContext
{
    public abstract HashSet<(string, NamedType)> ReservedMethodSignatures { get; }
}

internal sealed class VectorOperationProcesser : AActionableProcesser<IVectorOperationProcessingContext, RawVectorOperationDefinition, VectorOperationDefinition>
{
    private IVectorOperationProcessingDiagnostics Diagnostics { get; }

    public VectorOperationProcesser(IVectorOperationProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, VectorOperationDefinition product)
    {
        context.ReservedMethodSignatures.Add((product.Name ?? GetDefaultName(product.OperatorType, product.Position), product.Other));

        if (product.Mirror)
        {
            context.ReservedMethodSignatures.Add((product.MirroredName ?? GetDefaultMirroredName(product.OperatorType, product.Position), product.Other));
        }
    }

    public override IOptionalWithDiagnostics<VectorOperationDefinition> Process(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return ValidateResultNotNull(context, definition)
            .Validate(() => ValidateResultNotUndefined(definition))
            .Validate(() => ValidateOtherNotNull(context, definition))
            .Validate(() => ValidateOtherNotUndefined(definition))
            .Validate(() => ValidateOperatorTypeRecognized(context, definition))
            .Validate(() => ValidatePositionRecognized(context, definition))
            .Validate(() => ValidateMirrorNotSetButUnsupported(context, definition))
            .Validate(() => ValidateMethodNameNotNull(context, definition))
            .Validate(() => ValidateMethodNameNotEmpty(context, definition))
            .Validate(() => ValidateMirroredMethodNameNotNull(context, definition))
            .Validate(() => ValidateMirroredMethodNameNotEmpty(context, definition))
            .Validate(() => ValidateMirroredMethodNameNotUnnecessarilySpecified(context, definition))
            .Validate(() => ValidateOperationNotDuplicate(context, definition))
            .Transform(() => ProduceResult(context, definition));
    }

    private static VectorOperationDefinition ProduceResult(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var mirror = ShouldMirror(context.Type.AsNamedType(), definition);
        var methodName = definition.Name ?? GetDefaultName(definition.OperatorType, definition.Position);
        var mirroredMethodName = definition.MirroredName ?? GetDefaultMirroredName(definition.OperatorType, definition.Position);

        return new(definition.Result!.Value, definition.Other!.Value, definition.OperatorType, definition.Position, mirror, methodName, mirroredMethodName, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateResultNotNull(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Result is not null, () => Diagnostics.NullResult(context, definition));
    }

    private static IValidityWithDiagnostics ValidateResultNotUndefined(RawVectorOperationDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Result!.Value != NamedType.Empty);
    }

    private IValidityWithDiagnostics ValidateOtherNotNull(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Other is not null, () => Diagnostics.NullOther(context, definition));
    }

    private static IValidityWithDiagnostics ValidateOtherNotUndefined(RawVectorOperationDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Other!.Value != NamedType.Empty);
    }

    private IValidityWithDiagnostics ValidateOperatorTypeRecognized(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(VectorOperatorType), definition.OperatorType);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedOperatorType(context, definition));
    }

    private IValidityWithDiagnostics ValidatePositionRecognized(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(OperatorPosition), definition.Position);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedPosition(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirrorNotSetButUnsupported(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        if (context.Type.AsNamedType() != definition.Other!.Value)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Locations.ExplicitlySetMirror && definition.Mirror is true || definition.Locations.ExplicitlySetMirroredName)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.MirrorNotSupported(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateMethodNameNotNull(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var methodNameSpecifiedAndNull = definition.Locations.ExplicitlySetName && definition.Name is null;

        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndNull is false, () => Diagnostics.NullName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMethodNameNotEmpty(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var methodNameSpecifiedAndEmpty = definition.Locations.ExplicitlySetName && definition.Name!.Length is 0;
        
        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndEmpty is false, () => Diagnostics.EmptyName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirroredMethodNameNotNull(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var methodNameSpecifiedAndNull = definition.Locations.ExplicitlySetMirroredName && definition.MirroredName is null;

        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndNull is false, () => Diagnostics.NullMirroredName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirroredMethodNameNotEmpty(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var methodNameSpecifiedAndEmpty = definition.Locations.ExplicitlySetMirroredName && definition.MirroredName!.Length is 0;

        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndEmpty is false, () => Diagnostics.EmptyMirroredName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirroredMethodNameNotUnnecessarilySpecified(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMirroredName is false || context.Type.AsNamedType() == definition.Other)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Locations.ExplicitlySetMirror && definition.Mirror is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.MirrorDisabledButNameSpecified(context, definition));
        }

        if (definition.OperatorType is VectorOperatorType.Dot)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.MirrorNotSupported(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateOperationNotDuplicate(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        var name = definition.Name ?? GetDefaultName(definition.OperatorType, definition.Position);

        if (context.ReservedMethodSignatures.Contains((name, definition.Other!.Value)))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateName(context, definition, name));
        }

        var mirroredName = definition.MirroredName ?? GetDefaultMirroredName(definition.OperatorType, definition.Position);

        if (ShouldMirror(context.Type.AsNamedType(), definition) && context.ReservedMethodSignatures.Contains((mirroredName, definition.Other!.Value)))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateMirroredName(context, definition, mirroredName));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static string GetDefaultName(VectorOperatorType operatorType, OperatorPosition position) => (operatorType, position) switch
    {
        (VectorOperatorType.Dot, _) => "Dot",
        (VectorOperatorType.Cross, OperatorPosition.Left) => "Cross",
        (VectorOperatorType.Cross, OperatorPosition.Right) => "CrossInto",
        _ => throw new NotSupportedException($"Unsupported {typeof(OperatorType)}: {operatorType}")
    };

    private static string GetDefaultMirroredName(VectorOperatorType operatorType, OperatorPosition position)
    {
        if (position is OperatorPosition.Left)
        {
            return GetDefaultName(operatorType, OperatorPosition.Right);
        }

        return GetDefaultName(operatorType, OperatorPosition.Left);
    }

    private static bool ShouldMirror(NamedType selfType, RawVectorOperationDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMirror && definition.Mirror is false)
        {
            return false;
        }

        if (selfType == definition.Other!.Value)
        {
            return false;
        }

        if (definition.OperatorType is VectorOperatorType.Dot)
        {
            return false;
        }

        if (definition.Locations.ExplicitlySetMirror is false && definition.Locations.ExplicitlySetMirroredName)
        {
            return true;
        }

        return false;
    }
}
