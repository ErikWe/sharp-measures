namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Linq;

internal interface ISharpMeasuresVectorGroupMemberProcessingDiagnostics
{
    public abstract Diagnostic? NullVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension);
    public abstract Diagnostic? VectorNameAndDimensionMismatch(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int interpretedDimension);
}

internal class SharpMeasuresVectorGroupMemberProcesser
    : AProcesser<IProcessingContext, RawSharpMeasuresVectorGroupMemberDefinition, UnresolvedSharpMeasuresVectorGroupMemberDefinition>
{
    private ISharpMeasuresVectorGroupMemberProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupMemberProcesser(ISharpMeasuresVectorGroupMemberProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorGroupMemberDefinition> Process(IProcessingContext context,
        RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorGroupMemberDefinition>();
        }

        var processedDimensionality = ProcessDimension(context, definition);
        var allDiagnostics = processedDimensionality.Diagnostics;

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorGroupMemberDefinition>(allDiagnostics);
        }

        var processedVectorGroup = ProcessVectorGroup(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedVectorGroup.Diagnostics);

        if (processedVectorGroup.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresVectorGroupMemberDefinition>(allDiagnostics);
        }

        UnresolvedSharpMeasuresVectorGroupMemberDefinition product = new(processedVectorGroup.Result, processedDimensionality.Result, definition.GenerateDocumentation,
            definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return definition.Locations.ExplicitlySetVectorGroup;
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDimension && definition.Dimension < 2)
        {
            return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidDimension(context, definition));
        }

        if (Utility.InterpretDimensionFromName(context.Type.Name) is int result)
        {
            if (definition.Locations.ExplicitlySetDimension is false)
            {
                if (result < 2)
                {
                    return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidInterpretedDimension(context, definition, result));
                }

                return OptionalWithDiagnostics.Result(result);
            }

            if (result != definition.Dimension)
            {
                return OptionalWithDiagnostics.Result(definition.Dimension, Diagnostics.VectorNameAndDimensionMismatch(context, definition, result));
            }
        }

        if (definition.Locations.ExplicitlySetDimension)
        {
            return OptionalWithDiagnostics.Result(definition.Dimension);
        }

        return OptionalWithDiagnostics.Empty<int>(Diagnostics.MissingDimension(context, definition));
    }

    private IOptionalWithDiagnostics<NamedType> ProcessVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (definition.VectorGroup is null)
        {
            return OptionalWithDiagnostics.Empty<NamedType>(Diagnostics.NullVectorGroup(context, definition));
        }

        return OptionalWithDiagnostics.Result(definition.VectorGroup.Value);
    }
}
