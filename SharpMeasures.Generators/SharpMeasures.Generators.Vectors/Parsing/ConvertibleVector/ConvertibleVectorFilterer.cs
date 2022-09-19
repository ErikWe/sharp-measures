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
    public abstract Diagnostic? VectorUnexpectedDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index, int dimension);
    public abstract Diagnostic? VectorGroupLacksMemberOfMatchingDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index);
}

internal interface IConvertibleVectorFilteringContext : IProcessingContext
{
    public int Dimension { get; }
    public VectorType VectorType { get; }

    public abstract IVectorPopulation VectorPopulation { get; }

    public abstract HashSet<NamedType> InheritedConversions { get; }
    public abstract HashSet<NamedType> ListedMatchingConversions { get; }
}

internal sealed class ConvertibleVectorFilterer : AActionableProcesser<IConvertibleVectorFilteringContext, ConvertibleVectorDefinition, ConvertibleVectorDefinition>
{
    private IConvertibleVectorFilteringDiagnostics Diagnostics { get; }

    public ConvertibleVectorFilterer(IConvertibleVectorFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnStartProcessing(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition) => SetMatchingConversionsAsListed(context, context.InheritedConversions);
    public override void OnSuccessfulProcess(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, ConvertibleVectorDefinition product) => SetMatchingConversionsAsListed(context, product.Quantities);

    public override IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition)
    {
        List<NamedType> vectors = new(definition.Quantities.Count);
        List<int> locationMap = new(definition.Quantities.Count);

        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Quantities.Count; i++)
        {
            var validity = ValidateVector(context, definition, i);

            if (validity.IsValid)
            {
                vectors.Add(definition.Quantities[i]);
                locationMap.Add(i);
            }

            allDiagnostics.AddRange(validity);
        }

        var productDelegate = () => new ConvertibleVectorDefinition(vectors, definition.ConversionDirection, definition.CastOperatorBehaviour, definition.Locations, locationMap);

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(vectors.Count > 0, productDelegate, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return ValidateVectorNotDuplicate(context, definition, index)
            .Validate(() => ValidateVectorIsOfExpectedVectorType(context, definition, index))
            .Validate(() => ValidateVectorOfExpectedDimension(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateVectorNotDuplicate(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        var vectorIsExplicitlyDuplicate = context.ListedMatchingConversions.Contains(definition.Quantities[index]);

        if (context.VectorType is VectorType.Group || vectorIsExplicitlyDuplicate)
        {
            return ValidityWithDiagnostics.Conditional(vectorIsExplicitlyDuplicate is false, Diagnostics.DuplicateVector(context, definition, index));
        }

        var vectorIsImplictlyDuplicate = context.VectorPopulation.GroupMembersByGroup.TryGetValue(definition.Quantities[index], out var groupMembers)
            && groupMembers.GroupMembersByDimension.TryGetValue(context.Dimension, out var matchingVector)
            && context.ListedMatchingConversions.Contains(matchingVector.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(vectorIsImplictlyDuplicate is false, Diagnostics.DuplicateVector(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateVectorIsOfExpectedVectorType(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        var vectorIsGroup = context.VectorPopulation.Groups.ContainsKey(definition.Quantities[index]);
        
        if (context.VectorType is VectorType.Group)
        {
            return ValidityWithDiagnostics.Conditional(vectorIsGroup, () => Diagnostics.TypeNotVectorGroup(context, definition, index));
        }

        var vectorIsAnyVectorType = vectorIsGroup || context.VectorPopulation.Vectors.ContainsKey(definition.Quantities[index]) || context.VectorPopulation.GroupMembers.ContainsKey(definition.Quantities[index]);

        return ValidityWithDiagnostics.Conditional(vectorIsAnyVectorType, () => Diagnostics.TypeNotVector(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateVectorOfExpectedDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        if (context.VectorType is VectorType.Vector or VectorType.GroupMember)
        {
            if (context.VectorPopulation.Groups.ContainsKey(definition.Quantities[index]))
            {
                var vectorHasMemberOfMatchingDimension = context.VectorPopulation.GroupMembersByGroup[definition.Quantities[index]].GroupMembersByDimension.ContainsKey(context.Dimension);

                return ValidityWithDiagnostics.Conditional(vectorHasMemberOfMatchingDimension, () => Diagnostics.VectorGroupLacksMemberOfMatchingDimension(context, definition, index));
            }

            if (context.VectorPopulation.VectorBases.TryGetValue(definition.Quantities[index], out var vector))
            {
                return ValidityWithDiagnostics.Conditional(vector.Definition.Dimension == context.Dimension, () => Diagnostics.VectorUnexpectedDimension(context, definition, index, vector.Definition.Dimension));
            }

            var memberDimension = context.VectorPopulation.GroupMembers[definition.Quantities[index]].Definition.Dimension;

            return ValidityWithDiagnostics.Conditional(memberDimension == context.Dimension, () => Diagnostics.VectorUnexpectedDimension(context, definition, index, memberDimension));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static void SetMatchingConversionsAsListed(IConvertibleVectorFilteringContext context, IEnumerable<NamedType> conversions)
    {
        if (context.VectorType is VectorType.Group)
        {
            foreach (var conversion in conversions)
            {
                context.ListedMatchingConversions.Add(conversion);
            }

            return;
        }

        foreach (var conversion in conversions)
        {
            if (context.VectorPopulation.GroupMembersByGroup.TryGetValue(conversion, out var groupMembers))
            {
                if (groupMembers.GroupMembersByDimension.TryGetValue(context.Dimension, out var matchingConversion))
                {
                    context.ListedMatchingConversions.Add(matchingConversion.Type.AsNamedType());
                }

                continue;
            }

            context.ListedMatchingConversions.Add(conversion);
        }
    }
}
