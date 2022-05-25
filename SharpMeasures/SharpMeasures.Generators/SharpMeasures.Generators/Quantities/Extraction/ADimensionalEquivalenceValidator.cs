namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

internal abstract class ADimensionalEquivalenceValidator : IValidator<DimensionalEquivalenceDefinition>
{
    public IValidityWithDiagnostics Check(AttributeData attributeData, DimensionalEquivalenceDefinition definition)
    {
        HashSet<string> listedQuantities = new();
        IEnumerable<Diagnostic> diagnostics = Array.Empty<Diagnostic>();

        for (int i = 0; i < definition.Quantities.Count; i++)
        {
            IValidityWithDiagnostics quantityValidity = CheckQuantity(definition, i, listedQuantities);

            if (quantityValidity.IsInvalid)
            {
                quantityValidity.AddDiagnostics(diagnostics);
                return quantityValidity;
            }

            diagnostics = diagnostics.Concat(quantityValidity.Diagnostics);
        }

        return ValidityWithDiagnostics.ValidWithDiagnostics(diagnostics);
    }

    protected abstract IValidityWithDiagnostics Check(DimensionalEquivalenceDefinition definition, int index);
    protected abstract Diagnostic CreateDuplicateListingDiagnostics(DimensionalEquivalenceDefinition definition, int index);

    private IValidityWithDiagnostics CheckQuantity(DimensionalEquivalenceDefinition definition, int index, HashSet<string> alreadyListedQuantities)
    {
        if (definition.Quantities[index] is null)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (alreadyListedQuantities.Contains(definition.Quantities[index].Name))
        {
            return ValidityWithDiagnostics.Invalid(CreateDuplicateListingDiagnostics(definition, index));
        }

        return Check(definition, index);
    }
}
