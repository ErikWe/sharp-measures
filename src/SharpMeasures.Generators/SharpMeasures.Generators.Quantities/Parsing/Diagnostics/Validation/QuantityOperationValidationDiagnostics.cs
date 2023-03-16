namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

public sealed class QuantityOperationValidationDiagnostics : IQuantityOperationValidationDiagnostics
{
    public static QuantityOperationValidationDiagnostics Instance { get; } = new();

    private QuantityOperationValidationDiagnostics() { }

    public Diagnostic ResultNotQuantity(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.TypeNotQuantity(definition.Locations.Result?.AsRoslynLocation(), definition.Result.Name);
    }

    public Diagnostic OtherNotQuantity(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.TypeNotQuantity(definition.Locations.Other?.AsRoslynLocation(), definition.Other.Name);
    }

    public Diagnostic InvalidOperation(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.InvalidQuantityOperation(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic NonOverlappingVectorDimensions(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        return DiagnosticConstruction.NonOverlappingVectorDimensions(definition.Locations.AttributeName.AsRoslynLocation(), definition.Result.Name, context.QuantityType is QuantityType.Vector ? context.Type.Name : definition.Other.Name);
    }

    public Diagnostic MirrorNotSupported(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMirror)
        {
            return DiagnosticConstruction.QuantityOperationNotMirrorable(definition.Locations.Mirror?.AsRoslynLocation());
        }

        return DiagnosticConstruction.QuantityOperationNotMirrorable(definition.Locations.MirroredMethodName?.AsRoslynLocation());
    }
}
