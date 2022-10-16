namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IQuantityOperationValidationDiagnostics
{
    public abstract Diagnostic? ResultNotQuantity(IQuantityOperationValidationContext context, QuantityOperationDefinition definition);
    public abstract Diagnostic? OtherNotQuantity(IQuantityOperationValidationContext context, QuantityOperationDefinition definition);

    public abstract Diagnostic? InvalidOperation(IQuantityOperationValidationContext context, QuantityOperationDefinition definition);
    public abstract Diagnostic? NonOverlappingVectorDimensions(IQuantityOperationValidationContext context, QuantityOperationDefinition definition);

    public abstract Diagnostic? MirrorNotSupported(IQuantityOperationValidationContext context, QuantityOperationDefinition definition);
}

public interface IQuantityOperationValidationContext : IValidationContext
{
    public abstract QuantityType QuantityType { get; }
    public abstract IReadOnlyList<int> VectorDimensions { get; }

    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

public sealed class QuantityOperationValidator : AValidator<IQuantityOperationValidationContext, QuantityOperationDefinition>
{
    private IQuantityOperationValidationDiagnostics Diagnostics { get; }

    public QuantityOperationValidator(IQuantityOperationValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        return ValidateOperationArithmeticallyValid(context, definition);
    }

    private IValidityWithDiagnostics ValidateOperationArithmeticallyValid(IQuantityOperationValidationContext context, QuantityOperationDefinition definition)
    {
        var resultIsScalar = TypeIsScalar(definition.Result, context.ScalarPopulation);
        (var resultIsVector, var resultDimensions) = resultIsScalar ? (false, Array.Empty<int>()) : InterpretVector(definition.Result, context.VectorPopulation);

        if (resultIsScalar is false && resultIsVector is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ResultNotQuantity(context, definition));
        }

        var otherIsScalar = TypeIsScalar(definition.Other, context.ScalarPopulation);
        (var otherIsVector, var otherDimensions) = otherIsScalar ? (false, Array.Empty<int>()) : InterpretVector(definition.Other, context.VectorPopulation);

        if (otherIsScalar is false && otherIsVector is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.OtherNotQuantity(context, definition));
        }

        if (resultIsScalar)
        {
            return ValidityWithDiagnostics.Conditional(context.QuantityType is QuantityType.Scalar && otherIsScalar, () => Diagnostics.InvalidOperation(context, definition));
        }

        if (otherIsVector && context.QuantityType is QuantityType.Scalar)
        {
            if (definition.OperatorType is OperatorType.Addition or OperatorType.Subtraction)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
            }

            if (definition.OperatorType is OperatorType.Division && definition.Position is OperatorPosition.Left)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
            }

            if (definition.OperatorType is OperatorType.Division && definition.Mirror)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.MirrorNotSupported(context, definition));
            }

            var overlappingDimensions = new HashSet<int>(otherDimensions).Intersect(resultDimensions);

            return ValidityWithDiagnostics.Conditional(overlappingDimensions.Any(), Diagnostics.NonOverlappingVectorDimensions(context, definition));
        }

        if (otherIsScalar && context.QuantityType is QuantityType.Vector)
        {
            if (definition.OperatorType is OperatorType.Addition or OperatorType.Subtraction)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
            }

            if (definition.OperatorType is OperatorType.Division && definition.Position is OperatorPosition.Right)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
            }

            if (definition.OperatorType is OperatorType.Division && definition.Mirror)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.MirrorNotSupported(context, definition));
            }

            var overlappingDimensions = new HashSet<int>(context.VectorDimensions).Intersect(resultDimensions);

            return ValidityWithDiagnostics.Conditional(overlappingDimensions.Any(), () => Diagnostics.NonOverlappingVectorDimensions(context, definition));
        }

        if (otherIsVector && resultIsVector && context.QuantityType is QuantityType.Vector)
        {
            if (definition.OperatorType is OperatorType.Multiplication or OperatorType.Division)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
            }

            var overlappingDimensions = new HashSet<int>(context.VectorDimensions).Intersect(resultDimensions).Intersect(otherDimensions);

            return ValidityWithDiagnostics.Conditional(overlappingDimensions.Any(), () => Diagnostics.NonOverlappingVectorDimensions(context, definition));
        }

        return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
    }

    private static bool TypeIsScalar(NamedType type, IScalarPopulation scalarPopulation)
    {
        return scalarPopulation.ScalarBases.ContainsKey(type) || type.FullyQualifiedName is "global::SharpMeasures.Scalar";
    }

    private static (bool TypeIsVector, IReadOnlyList<int> Dimensions) InterpretVector(NamedType type, IVectorPopulation vectorPopulation)
    {
        if (vectorPopulation.VectorBases.TryGetValue(type, out var vector))
        {
            return (true, new[] { vector.Definition.Dimension });
        }

        if (vectorPopulation.GroupMembersByGroup.TryGetValue(type, out var groupMembers))
        {
            List<int> dimensions = new(groupMembers.GroupMembersByDimension.Count);

            foreach (var groupMember in groupMembers.GroupMembersByDimension)
            {
                dimensions.Add(groupMember.Key);
            }

            return (true, dimensions);
        }

        if (vectorPopulation.GroupMembers.TryGetValue(type, out var member))
        {
            return (true, new[] { member.Definition.Dimension });
        }

        if (type.FullyQualifiedName.StartsWith("global::SharpMeasures.Vector", StringComparison.InvariantCulture) && int.TryParse(type.Name.Substring(6), out var dimension))
        {
            return (true, new[] { dimension });
        }

        return (false, Array.Empty<int>());
    }
}
