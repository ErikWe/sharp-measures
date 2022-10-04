namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class VectorOperationProcessingDiagnostics : IVectorOperationProcessingDiagnostics
{
    public static VectorOperationProcessingDiagnostics Instance { get; } = new();

    private VectorOperationProcessingDiagnostics() { }

    public Diagnostic NullResult(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotQuantity(definition.Locations.Result?.AsRoslynLocation());
    }

    public Diagnostic NullOther(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Other?.AsRoslynLocation());
    }

    public Diagnostic UnrecognizedOperatorType(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.OperatorType?.AsRoslynLocation(), definition.OperatorType);
    }

    public Diagnostic UnrecognizedPosition(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.Position?.AsRoslynLocation(), definition.Position);
    }

    public Diagnostic MirrorNotSupported(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMirror)
        {
            return DiagnosticConstruction.QuantityOperationNotMirrorable(definition.Locations.Mirror?.AsRoslynLocation());
        }

        return DiagnosticConstruction.QuantityOperationNotMirrorable(definition.Locations.MirroredName?.AsRoslynLocation());
    }

    public Diagnostic NullName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.InvalidQuantityOperationName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => NullName(context, definition);

    public Diagnostic NullMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.InvalidQuantityOperationName(definition.Locations.MirroredName?.AsRoslynLocation());
    }

    public Diagnostic EmptyMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition) => NullMirroredName(context, definition);

    public Diagnostic MirrorDisabledButNameSpecified(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition)
    {
        return DiagnosticConstruction.QuantityOperationMirrorDisabledButNameSpecified(definition.Locations.MirroredName?.AsRoslynLocation());
    }

    public Diagnostic DuplicateName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, string name)
    {
        if (definition.Locations.ExplicitlySetName)
        {
            return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.Name?.AsRoslynLocation(), context.Type.Name, name);
        }

        return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, name);
    }

    public Diagnostic DuplicateMirroredName(IVectorOperationProcessingContext context, RawVectorOperationDefinition definition, string name)
    {
        if (definition.Locations.ExplicitlySetMirroredName)
        {
            return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.MirroredName?.AsRoslynLocation(), context.Type.Name, name);
        }

        return DiagnosticConstruction.DuplicateQuantityOperationMethod(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, name);
    }
}
