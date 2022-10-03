namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IQuantityOperationProcessingDiagnostics
{
    public abstract Diagnostic? NullResult(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? NullOther(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);

    public abstract Diagnostic? UnrecognizedOperatorType(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? UnrecognizedPosition(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? UnrecognizedImplementation(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);

    public abstract Diagnostic? MirrorNotSupported(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);

    public abstract Diagnostic? NullMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? EmptyMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? MethodDisabledButNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);

    public abstract Diagnostic? NullMirroredMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? EmptyMirroredMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? MirrorDisabledButMethodNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? MethodDisabledButMirroredNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? MirroredMethodNotSupportedButNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);

    public abstract Diagnostic? DuplicateOperator(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? DuplicateMirroredOperator(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition);
    public abstract Diagnostic? DuplicateMethod(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, string name);
    public abstract Diagnostic? DuplicateMirroredMethod(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, string name);
}

public interface IQuantityOperationProcessingContext : IProcessingContext
{
    public abstract HashSet<(OperatorType, NamedType)> ReservedLHSOperators { get; }
    public abstract HashSet<(OperatorType, NamedType)> ReservedRHSOperators { get; }
    public abstract HashSet<(string, NamedType)> ReservedMethodSignatures { get; }
}

public sealed class QuantityOperationProcesser : AActionableProcesser<IQuantityOperationProcessingContext, RawQuantityOperationDefinition, QuantityOperationDefinition>
{
    private IQuantityOperationProcessingDiagnostics Diagnostics { get; }

    public QuantityOperationProcesser(IQuantityOperationProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, QuantityOperationDefinition product)
    {
        if (product.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Operator)
        {
            if (product.Mirror || product.Position is OperatorPosition.Left)
            {
                context.ReservedLHSOperators.Add((product.OperatorType, product.Other));
            }

            if (product.Mirror || product.Position is OperatorPosition.Right)
            {
                context.ReservedRHSOperators.Add((product.OperatorType, product.Other));
            }
        }

        if (product.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Method)
        {
            context.ReservedMethodSignatures.Add((product.MethodName, product.Other));

            if (product.MirrorMethod)
            {
                context.ReservedMethodSignatures.Add((product.MirroredMethodName, product.Other));
            }
        }
    }

    public override IOptionalWithDiagnostics<QuantityOperationDefinition> Process(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return ValidateResultNotNull(context, definition)
            .Validate(() => ValidateOtherNotNull(context, definition))
            .Validate(() => ValidateOperatorTypeRecognized(context, definition))
            .Validate(() => ValidatePositionRecognized(context, definition))
            .Validate(() => ValidateImplementationRecognized(context, definition))
            .Validate(() => ValidateMirrorNotSetButUnsupported(context, definition))
            .Validate(() => ValidateMethodNameNotNull(context, definition))
            .Validate(() => ValidateMethodNameNotEmpty(context, definition))
            .Validate(() => ValidateMethodNameNotUnnecessarilySpecified(context, definition))
            .Validate(() => ValidateMirroredMethodNameNotNull(context, definition))
            .Validate(() => ValidateMirroredMethodNameNotEmpty(context, definition))
            .Validate(() => ValidateMirroredMethodNameNotUnnecessarilySpecified(context, definition))
            .Validate(() => ValidateOperationNotDuplicate(context, definition))
            .Transform(() => ProduceResult(context, definition));
    }

    private static QuantityOperationDefinition ProduceResult(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var mirrorOperator = ShouldMirrorOperator(context.Type.AsNamedType(), definition);
        var mirrorMethod = ShouldMirrorOperator(context.Type.AsNamedType(), definition);

        var methodName = definition.MethodName ?? GetDefaultMethodName(definition.OperatorType, definition.Position);
        var mirroredMethodName = definition.MirroredMethodName ?? GetDefaultMirroredMethodName(definition.OperatorType, definition.Position);

        return new(definition.Result!.Value, definition.Other!.Value, definition.OperatorType, definition.Position, mirrorOperator, mirrorMethod, definition.Implementation, methodName, mirroredMethodName, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateResultNotNull(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Result is not null, () => Diagnostics.NullResult(context, definition));
    }

    private IValidityWithDiagnostics ValidateOtherNotNull(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Other is not null, () => Diagnostics.NullOther(context, definition));
    }

    private IValidityWithDiagnostics ValidateOperatorTypeRecognized(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(OperatorType), definition.OperatorType);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedOperatorType(context, definition));
    }

    private IValidityWithDiagnostics ValidatePositionRecognized(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(OperatorPosition), definition.Position);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedPosition(context, definition));
    }

    private IValidityWithDiagnostics ValidateImplementationRecognized(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(QuantityOperationImplementation), definition.Implementation);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedImplementation(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirrorNotSetButUnsupported(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        if (context.Type.AsNamedType() != definition.Other!.Value)
        {
            return ValidityWithDiagnostics.Valid;
        }

        var mirrorExplicitlySet = definition.Locations.ExplicitlySetMirror && definition.Mirror is true;

        return ValidityWithDiagnostics.Conditional(mirrorExplicitlySet is false, () => Diagnostics.MirrorNotSupported(context, definition));
    }

    private IValidityWithDiagnostics ValidateMethodNameNotNull(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var methodNameSpecifiedAndNull = definition.Locations.ExplicitlySetMethodName && definition.MethodName is null;

        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndNull is false, () => Diagnostics.NullMethodName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMethodNameNotEmpty(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var methodNameSpecifiedAndEmpty = definition.Locations.ExplicitlySetMethodName && definition.MethodName!.Length is 0;
        
        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndEmpty is false, () => Diagnostics.EmptyMethodName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMethodNameNotUnnecessarilySpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var methodNameSpecifiedButDisabled = definition.Locations.ExplicitlySetMethodName && definition.Implementation is QuantityOperationImplementation.None or QuantityOperationImplementation.Operator;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(methodNameSpecifiedButDisabled, () => Diagnostics.MethodDisabledButNameSpecified(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirroredMethodNameNotNull(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var methodNameSpecifiedAndNull = definition.Locations.ExplicitlySetMirroredMethodName && definition.MirroredMethodName is null;

        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndNull is false, () => Diagnostics.NullMirroredMethodName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirroredMethodNameNotEmpty(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        var methodNameSpecifiedAndEmpty = definition.Locations.ExplicitlySetMirroredMethodName && definition.MirroredMethodName!.Length is 0;

        return ValidityWithDiagnostics.Conditional(methodNameSpecifiedAndEmpty is false, () => Diagnostics.EmptyMirroredMethodName(context, definition));
    }

    private IValidityWithDiagnostics ValidateMirroredMethodNameNotUnnecessarilySpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMirroredMethodName is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Locations.ExplicitlySetMirror && definition.Mirror is false)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.MirrorDisabledButMethodNameSpecified(context, definition));
        }

        if (definition.Implementation is QuantityOperationImplementation.None or QuantityOperationImplementation.Operator)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.MethodDisabledButMirroredNameSpecified(context, definition));
        }

        if (definition.OperatorType is OperatorType.Addition or OperatorType.Multiplication || context.Type.AsNamedType() == definition.Other)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.MirroredMethodNotSupportedButNameSpecified(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateOperationNotDuplicate(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        if (definition.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Operator)
        {
            var mirror = ShouldMirrorOperator(context.Type.AsNamedType(), definition);

            if ((mirror || definition.Position is OperatorPosition.Left) && context.ReservedLHSOperators.Contains((definition.OperatorType, definition.Other!.Value)))
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateOperator(context, definition));
            }

            if ((mirror || definition.Position is OperatorPosition.Right) && context.ReservedRHSOperators.Contains((definition.OperatorType, definition.Other!.Value)))
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateMirroredOperator(context, definition));
            }
        }

        if (definition.Implementation is QuantityOperationImplementation.OperatorAndMethod or QuantityOperationImplementation.Method)
        {
            var methodName = definition.MethodName ?? GetDefaultMethodName(definition.OperatorType, definition.Position);

            if (context.ReservedMethodSignatures.Contains((methodName, definition.Other!.Value)))
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateMethod(context, definition, methodName));
            }

            if (ShouldMirrorMethod(context.Type.AsNamedType(), definition))
            {
                var mirroredMethodName = definition.MirroredMethodName ?? GetDefaultMirroredMethodName(definition.OperatorType, definition.Position);

                if (context.ReservedMethodSignatures.Contains((mirroredMethodName, definition.Other!.Value)))
                {
                    return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateMirroredMethod(context, definition, mirroredMethodName));
                }
            }
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static string GetDefaultMethodName(OperatorType operatorType, OperatorPosition position) => (operatorType, position) switch
    {
        (OperatorType.Addition, _) => "Add",
        (OperatorType.Subtraction, OperatorPosition.Left) => "Subtract",
        (OperatorType.Subtraction, OperatorPosition.Right) => "SubtractFrom",
        (OperatorType.Multiplication, _) => "Multiply",
        (OperatorType.Division, OperatorPosition.Left) => "Divide",
        (OperatorType.Division, OperatorPosition.Right) => "DivideInto",
        _ => throw new NotSupportedException($"Unsupported {typeof(OperatorType)}: {operatorType}")
    };

    private static string GetDefaultMirroredMethodName(OperatorType operatorType, OperatorPosition position)
    {
        if (position is OperatorPosition.Left)
        {
            return GetDefaultMethodName(operatorType, OperatorPosition.Right);
        }

        return GetDefaultMethodName(operatorType, OperatorPosition.Left);
    }

    private static bool ShouldMirrorOperator(NamedType selfType, RawQuantityOperationDefinition definition)
    {
        if (definition.Implementation is not (QuantityOperationImplementation.Operator or QuantityOperationImplementation.OperatorAndMethod))
        {
            return false;
        }

        if (selfType == definition.Other)
        {
            return false;
        }

        if (definition.Locations.ExplicitlySetMirror)
        {
            return definition.Mirror;
        }

        if (definition.Locations.ExplicitlySetMirroredMethodName && definition.Implementation is QuantityOperationImplementation.OperatorAndMethod)
        {
            return true;
        }

        if (definition.OperatorType is OperatorType.Addition or OperatorType.Multiplication)
        {
            return true;
        }

        return false;
    }

    private static bool ShouldMirrorMethod(NamedType selfType, RawQuantityOperationDefinition definition)
    {
        if (definition.Implementation is not (QuantityOperationImplementation.Method or QuantityOperationImplementation.OperatorAndMethod))
        {
            return false;
        }

        if (selfType == definition.Other)
        {
            return false;
        }

        if (definition.OperatorType is OperatorType.Addition or OperatorType.Multiplication)
        {
            return false;
        }

        if (definition.Locations.ExplicitlySetMirror)
        {
            return definition.Mirror;
        }

        if (definition.Locations.ExplicitlySetMirroredMethodName)
        {
            return true;
        }

        return false;
    }
}
