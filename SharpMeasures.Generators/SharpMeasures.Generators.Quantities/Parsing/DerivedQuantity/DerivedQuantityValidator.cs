namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors;

public interface IDerivedQuantityValidationDiagnostics
{
    public abstract Diagnostic? TypeNotQuantity(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition, int index);
}

public interface IDerivedQuantityValidationContext : IValidationContext
{
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

public class DerivedQuantityValidator : AValidator<IDerivedQuantityValidationContext, DerivedQuantityDefinition>
{
    private IDerivedQuantityValidationDiagnostics Diagnostics { get; }

    public DerivedQuantityValidator(IDerivedQuantityValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IDerivedQuantityValidationContext context, DerivedQuantityDefinition definition)
    {
        return ValidateSignatureElements(context, definition);
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
        var signatureElementIsVectorGroup = context.VectorPopulation.Groups.ContainsKey(definition.Signature[index]);
        var signatureElementIsVectorGroupMember = context.VectorPopulation.GroupMembers.ContainsKey(definition.Signature[index]);

        var signatureElementIsQuantity = signatureElementIsScalar || signatureElementIsVector || signatureElementIsVectorGroup || signatureElementIsVectorGroupMember;

        return ValidityWithDiagnostics.Conditional(signatureElementIsQuantity, () => Diagnostics.TypeNotQuantity(context, definition, index));
    }
}
