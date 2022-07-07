namespace SharpMeasures.Generators.Vectors.Refinement.ResizedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Populations;
using SharpMeasures.Generators.Vectors.Populations;

internal interface IResizedSharpMeasuresVectorRefinementDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotVector(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DuplicateDimension(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? UnresolvedVectorGroup(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition);
}

internal interface IResizedSharpMeasuresVectorRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }

    public abstract VectorPopulationErrors VectorPopulationData { get; }
}

internal class ResizedSharpMeasuresVectorRefiner : IProcesser<IResizedSharpMeasuresVectorRefinementContext, ResizedSharpMeasuresVectorDefinition,
    RefinedResizedSharpMeasuresVectorDefinition>
{
    private IResizedSharpMeasuresVectorRefinementDiagnostics Diagnostics { get; }

    public ResizedSharpMeasuresVectorRefiner(IResizedSharpMeasuresVectorRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedResizedSharpMeasuresVectorDefinition> Process(IResizedSharpMeasuresVectorRefinementContext context,
        ResizedSharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.ResizedVectorGroups.TryGetValue(context.Type.AsNamedType(), out var vectorGroup) is false)
        {
            if (context.VectorPopulationData.UnresolvedAssociatedVectors.ContainsKey(context.Type.AsNamedType()))
            {
                return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>(Diagnostics.UnresolvedVectorGroup(context, definition));
            }

            if (context.VectorPopulationData.ResizedVectorsWithDuplicateDimension.ContainsKey(context.Type.AsNamedType()))
            {
                return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>(Diagnostics.DuplicateDimension(context, definition));
            }

            return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>(Diagnostics.TypeNotVector(context, definition));
        }

        if (context.VectorPopulation.AllVectors.TryGetValue(definition.AssociatedVector, out var associatedVector) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>();
        }

        if (context.UnitPopulation.TryGetValue(vectorGroup.Root.UnitType, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>();
        }

        IScalarType? scalar = null;

        if (vectorGroup.Root.ScalarType is not null && context.ScalarPopulation.TryGetValue(vectorGroup.Root.ScalarType.Value, out scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedResizedSharpMeasuresVectorDefinition>();
        }

        RefinedResizedSharpMeasuresVectorDefinition product = new(associatedVector, vectorGroup, unit, scalar, definition.Dimension, vectorGroup.Root.ImplementSum,
            vectorGroup.Root.ImplementDifference, vectorGroup.Root.Difference, vectorGroup.Root.DefaultUnitName, vectorGroup.Root.DefaultUnitSymbol,
            definition.GenerateDocumentation);
        
        return OptionalWithDiagnostics.Result(product);
    }
}
