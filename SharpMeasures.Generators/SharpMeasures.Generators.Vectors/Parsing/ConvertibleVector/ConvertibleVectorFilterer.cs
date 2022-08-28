namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IConvertibleVectorFilteringDiagnostics
{
    public abstract Diagnostic? TypeNotVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index);
    public abstract Diagnostic? TypeNotVectorGroup(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index);
    public abstract Diagnostic? DuplicateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index);
}

internal interface IConvertibleVectorFilteringContext : IProcessingContext
{
    public VectorType VectorType { get; }

    public abstract IVectorPopulation VectorPopulation { get; }

    public abstract HashSet<NamedType> InheritedConversions { get; }
}

internal class ConvertibleVectorFilterer : IProcesser<IConvertibleVectorFilteringContext, ConvertibleVectorDefinition, ConvertibleVectorDefinition>
{
    private IConvertibleVectorFilteringDiagnostics Diagnostics { get; }

    public ConvertibleVectorFilterer(IConvertibleVectorFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition)
    {
        List<NamedType> vectors = new(definition.Vectors.Count);
        List<int> locationMap = new(definition.Vectors.Count);

        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Vectors.Count; i++)
        {
            var validity = ValidateVector(context, definition, i);

            if (validity.IsValid)
            {
                vectors.Add(definition.Vectors[i]);
                locationMap.Add(i);
            }

            allDiagnostics.AddRange(validity);
        }

        ConvertibleVectorDefinition product = new(vectors, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations, locationMap);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return ValidateVectorNotDuplicateWithInherited(context, definition, index)
            .Validate(() => ValidateVectorIsOfExpectedVectorType(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateVectorNotDuplicateWithInherited(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        var vectorIsDuplicate = context.InheritedConversions.Contains(definition.Vectors[index]);

        return ValidityWithDiagnostics.Conditional(vectorIsDuplicate is false, () => Diagnostics.DuplicateVector(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateVectorIsOfExpectedVectorType(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        var vectorIsGroup = context.VectorPopulation.Groups.ContainsKey(definition.Vectors[index]);
        
        if (context.VectorType is VectorType.Group)
        {
            return ValidityWithDiagnostics.Conditional(vectorIsGroup, () => Diagnostics.TypeNotVectorGroup(context, definition, index));
        }

        var vectorIsAnyVectorType = vectorIsGroup || context.VectorPopulation.Vectors.ContainsKey(definition.Vectors[index]) || context.VectorPopulation.GroupMembers.ContainsKey(definition.Vectors[index]);

        return ValidityWithDiagnostics.Conditional(vectorIsAnyVectorType, () => Diagnostics.TypeNotVector(context, definition, index));
    }
}
