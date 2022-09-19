namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

using System;

public interface IDerivedQuantityValidationDiagnostics : IDerivedQuantityExpandingDiagnostics
{
    public abstract Diagnostic? TypeNotQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index);
}

public interface IDerivedQuantityValidationContext : IProcessingContext
{
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

public sealed class DerivedQuantityValidator : AProcesser<IDerivedQuantityValidationContext, DerivedQuantityDefinition, DerivedQuantityDefinition>
{
    private IDerivedQuantityValidationDiagnostics Diagnostics { get; }

    public DerivedQuantityValidator(IDerivedQuantityValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivedQuantityDefinition> Process(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition)
    {
        var validity = ValidateSignatureElements(context, definition);

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<DerivedQuantityDefinition>();
        }

        return DerivedQuantityExpander.Expand(context.Type.AsNamedType(), definition, context.VectorPopulation, Diagnostics);
    }

    private IValidityWithDiagnostics ValidateSignatureElements(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition)
    {
        var validity = ValidityWithDiagnostics.Valid;

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            validity = validity.Validate(() => ValidateSignatureElementIsQuantity(context, definition, i));
        }

        return validity;
    }

    private IValidityWithDiagnostics ValidateSignatureElementIsQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index)
    {
        var signatureElementIsScalar = context.ScalarPopulation.Scalars.ContainsKey(definition.Signature[index]);
        var signatureElementIsVector = context.VectorPopulation.Vectors.ContainsKey(definition.Signature[index]);
        var signatureElementIsGroup = context.VectorPopulation.Groups.ContainsKey(definition.Signature[index]);
        var signatureElementIsGroupMember = context.VectorPopulation.GroupMembers.ContainsKey(definition.Signature[index]);

        var signatureElementIsPureScalar = definition.Signature[index].FullyQualifiedName is "global::SharpMeasures.Scalar";
        var signatureElementIsPureVector = definition.Signature[index].FullyQualifiedName.StartsWith("global::Sharpmeasures.Vector", StringComparison.InvariantCulture);

        var signatureElementIsQuantity = signatureElementIsScalar || signatureElementIsVector || signatureElementIsGroup || signatureElementIsGroupMember || signatureElementIsPureScalar || signatureElementIsPureVector;

        return ValidityWithDiagnostics.Conditional(signatureElementIsQuantity, () => Diagnostics.TypeNotQuantity(context, definition, index));
    }
}
