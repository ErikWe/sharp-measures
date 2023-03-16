namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface IVectorOperationValidationDiagnostics
{
    public abstract Diagnostic? ResultNotQuantity(IVectorOperationValidationContext context, VectorOperationDefinition definition);
    public abstract Diagnostic? OtherNotVector(IVectorOperationValidationContext context, VectorOperationDefinition definition);

    public abstract Diagnostic? InvalidOperation(IVectorOperationValidationContext context, VectorOperationDefinition definition);
    public abstract Diagnostic? NonOverlappingVectorDimensions(IVectorOperationValidationContext context, VectorOperationDefinition definition);

    public abstract Diagnostic? ResultDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition);
    public abstract Diagnostic? OtherDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition);
    public abstract Diagnostic? ThisDoesNotSupportCrossProduct(IVectorOperationValidationContext context, VectorOperationDefinition definition);
}

internal interface IVectorOperationValidationContext : IValidationContext
{
    public abstract IReadOnlyList<int> Dimensions { get; }

    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class VectorOperationValidator : AValidator<IVectorOperationValidationContext, VectorOperationDefinition>
{
    private IVectorOperationValidationDiagnostics Diagnostics { get; }

    public VectorOperationValidator(IVectorOperationValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        return ValidateOperationArithmeticallyValid(context, definition);
    }

    private IValidityWithDiagnostics ValidateOperationArithmeticallyValid(IVectorOperationValidationContext context, VectorOperationDefinition definition)
    {
        var resultIsScalar = TypeIsScalar(definition.Result, context.ScalarPopulation);

        if (resultIsScalar && definition.OperatorType is VectorOperatorType.Cross)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.InvalidOperation(context, definition));
        }

        var otherIsScalar = TypeIsScalar(definition.Other, context.ScalarPopulation);

        if (otherIsScalar)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.OtherNotVector(context, definition));
        }

        (var resultIsVector, var resultDimensions) = resultIsScalar ? (false, Array.Empty<int>()) : InterpretVector(definition.Result, context.VectorPopulation);

        if (resultIsVector is false && resultIsScalar is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ResultNotQuantity(context, definition));
        }

        (var otherIsVector, var otherDimensions) = InterpretVector(definition.Other, context.VectorPopulation);

        if (otherIsVector is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.OtherNotVector(context, definition));
        }

        if (resultIsVector)
        {
            if (context.Dimensions.Contains(3) is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.ThisDoesNotSupportCrossProduct(context, definition));
            }

            if (resultDimensions.Contains(3) is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.ResultDoesNotSupportCrossProduct(context, definition));
            }

            if (otherDimensions.Contains(3) is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.OtherDoesNotSupportCrossProduct(context, definition));
            }

            return ValidityWithDiagnostics.Valid;
        }

        var overlappingDimensions = new HashSet<int>(context.Dimensions).Intersect(otherDimensions);

        return ValidityWithDiagnostics.Conditional(overlappingDimensions.Any(), Diagnostics.NonOverlappingVectorDimensions(context, definition));
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
