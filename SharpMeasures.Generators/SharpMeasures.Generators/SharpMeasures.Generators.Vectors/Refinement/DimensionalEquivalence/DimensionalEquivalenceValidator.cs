namespace SharpMeasures.Generators.Vectors.Refinement.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal interface IDimensionalEquivalenceValidationDiagnostics
{
    public abstract Diagnostic? TypeNotVector(IDimensionalEquivalenceValidationContext context, DimensionalEquivalenceDefinition definition, int index);
    public abstract Diagnostic? VectorGroupAlreadySpecified(IDimensionalEquivalenceValidationContext context, DimensionalEquivalenceDefinition definition, int index);
}

internal interface IDimensionalEquivalenceValidationContext : IValidationContext
{
    public abstract HashSet<NamedType> ExcessiveQuantities { get; }

    public abstract VectorPopulation VectorPopulation { get; }
}

internal class DimensionalEquivalenceValidator : IValidator<IDimensionalEquivalenceValidationContext, DimensionalEquivalenceDefinition>
{
    private IDimensionalEquivalenceValidationDiagnostics Diagnostics { get; }

    public DimensionalEquivalenceValidator(IDimensionalEquivalenceValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IValidityWithDiagnostics CheckValidity(IDimensionalEquivalenceValidationContext context, DimensionalEquivalenceDefinition definition)
    {
        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.Quantities.Count; i++)
        {
            if (context.ExcessiveQuantities.Contains(definition.Quantities[i]))
            {
                if (Diagnostics.VectorGroupAlreadySpecified(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.VectorPopulation.AllVectors.ContainsKey(definition.Quantities[i]) is false)
            {
                if (Diagnostics.TypeNotVector(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }
        }

        return ValidityWithDiagnostics.ValidWithDiagnostics(allDiagnostics);
    }
}
