namespace SharpMeasures.Generators.Vectors.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Processing.Diagnostics;

using System;

internal class ResizedVectorProcessingContext : IValidatorContext
{
    public DefinedType Type { get; }

    public VectorPopulation VectorPopulation { get; }

    public ResizedVectorProcessingContext(DefinedType type, VectorPopulation vectorPopulation)
    {
        Type = type;

        VectorPopulation = vectorPopulation;
    }
}

internal class ResizedVectorValidator : IValidator<ResizedVectorProcessingContext, ResizedVectorDefinition>
{
    public static ResizedVectorValidator Instance { get; } = new();

    private ResizedVectorValidator() { }

    public IValidityWithDiagnostics CheckValidity(ResizedVectorProcessingContext context, ResizedVectorDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        if (context.VectorPopulation.VectorGroups.Population.ContainsKey(context.Type.AsNamedType()))
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (context.VectorPopulation.UnresolvedVectors.Population.ContainsKey(context.Type.AsNamedType()))
        {
            return ValidityWithDiagnostics.Invalid(ResizedVectorDiagnostics.UnresolvedVectorGroup(definition));
        }

        if (context.VectorPopulation.DuplicateDimensionVectors.Population.ContainsKey(context.Type.AsNamedType()))
        {
            return ValidityWithDiagnostics.Invalid(ResizedVectorDiagnostics.DuplicateDimension(definition));
        }

        return ValidityWithDiagnostics.Invalid(ResizedVectorDiagnostics.TypeNotVector(definition));
    }
}
