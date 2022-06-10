namespace SharpMeasures.Generators.Vectors.Refinement;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using System;

internal interface IResizedVectorRefinementDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(IResizedVectorRefinementContext context, ResizedVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(IResizedVectorRefinementContext context, ResizedVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(IResizedVectorRefinementContext context, ResizedVectorDefinition definition);
    public abstract Diagnostic? TypeNotVector(IResizedVectorRefinementContext context, ResizedVectorDefinition definition);
    public abstract Diagnostic? DuplicateDimension(IResizedVectorRefinementContext context, ResizedVectorDefinition definition);
    public abstract Diagnostic? UnresolvedVectorGroup(IResizedVectorRefinementContext context, ResizedVectorDefinition definition);
}

internal interface IResizedVectorRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract ScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }
}

internal class ResizedVectorRefiner : IProcesser<IResizedVectorRefinementContext, ResizedVectorDefinition, RefinedResizedVectorDefinition>
{
    private IResizedVectorRefinementDiagnostics Diagnostics { get; }

    public ResizedVectorRefiner(IResizedVectorRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedResizedVectorDefinition> Process(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(context.Type.AsNamedType(), out var vectorGroup) is false)
        {
            if (context.VectorPopulation.UnresolvedVectors.ContainsKey(context.Type.AsNamedType()))
            {
                return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>(Diagnostics.UnresolvedVectorGroup(context, definition));
            }

            if (context.VectorPopulation.DuplicateDimensionVectors.ContainsKey(context.Type.AsNamedType()))
            {
                return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>(Diagnostics.DuplicateDimension(context, definition));
            }
            
            return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>(Diagnostics.TypeNotVector(context, definition));
        }

        if (context.UnitPopulation.TryGetValue(vectorGroup.Root.UnitType, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>();
        }

        ScalarInterface? scalar = null;

        if (vectorGroup.Root.ScalarType is not null && context.ScalarPopulation.TryGetValue(vectorGroup.Root.ScalarType.Value, out scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedVectorDefinition>();
        }

        RefinedResizedVectorDefinition product = new(vectorGroup.Root, unit, scalar, definition.Dimension, definition.GenerateDocumentation);
        return OptionalWithDiagnostics.Result(product);
    }
}
