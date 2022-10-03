namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

public sealed class QuantityOperationProcessingDiagnostics : IQuantityOperationProcessingDiagnostics
{
    public static QuantityOperationProcessingDiagnostics Instance { get; } = new();

    private QuantityOperationProcessingDiagnostics() { }

    public Diagnostic NullResult(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotQuantity(definition.Locations.Result?.AsRoslynLocation());
    }

    public Diagnostic NullOther(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotQuantity(definition.Locations.Other?.AsRoslynLocation());
    }

    public Diagnostic UnrecognizedOperatorType(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.OperatorType?.AsRoslynLocation(), definition.OperatorType);
    }

    public Diagnostic UnrecognizedPosition(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.Position?.AsRoslynLocation(), definition.Position);
    }

    public Diagnostic UnrecognizedImplementation(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.Implementation?.AsRoslynLocation(), definition.Implementation);
    }

    public Diagnostic MirrorNotSupported(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMirror)
        {
            return DiagnosticConstruction.QuantityOperationNotMirrorable(definition.Locations.Mirror?.AsRoslynLocation());
        }

        return DiagnosticConstruction.QuantityOperationNotMirrorable(definition.Locations.MirroredMethodName?.AsRoslynLocation());
    }

    public Diagnostic NullMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.InvalidQuantityOperationName(definition.Locations.MethodName?.AsRoslynLocation());
    }

    public Diagnostic EmptyMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => NullMethodName(context, definition);

    public Diagnostic MethodDisabledButNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.QuantityOperationMethodDisabledButNameSpecified(definition.Locations.MethodName?.AsRoslynLocation());
    }

    public Diagnostic NullMirroredMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.InvalidQuantityOperationName(definition.Locations.MirroredMethodName?.AsRoslynLocation());
    }

    public Diagnostic EmptyMirroredMethodName(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => NullMirroredMethodName(context, definition);

    public Diagnostic MirrorDisabledButMethodNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.QuantityOperationMirrorDisabledButNameSpecified(definition.Locations.MirroredMethodName?.AsRoslynLocation());
    }

    public Diagnostic MethodDisabledButMirroredNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.QuantityOperationMethodDisabledButNameSpecified(definition.Locations.MirroredMethodName?.AsRoslynLocation());
    }

    public Diagnostic MirroredMethodNotSupportedButNameSpecified(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.QuantityOperationMethodNotMirrorable(definition.Locations.MirroredMethodName?.AsRoslynLocation());
    }

    public Diagnostic DuplicateOperator(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.DuplicateQuantityOperationOperator(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic DuplicateMirroredOperator(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition) => DuplicateOperator(context, definition);

    public Diagnostic DuplicateMethod(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, string name)
    {
        if (definition.Locations.ExplicitlySetMethodName)
        {
            return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.MethodName?.AsRoslynLocation(), context.Type.Name, name);
        }

        return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, name);
    }

    public Diagnostic DuplicateMirroredMethod(IQuantityOperationProcessingContext context, RawQuantityOperationDefinition definition, string name)
    {
        if (definition.Locations.ExplicitlySetMirroredMethodName)
        {
            return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.MirroredMethodName?.AsRoslynLocation(), context.Type.Name, name);
        }

        return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, name);
    }
}
