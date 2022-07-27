namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Globalization;
using System.Text.RegularExpressions;

internal interface ISharpMeasuresVectorGroupMemberProcessingDiagnostics
{
    public abstract Diagnostic? NullVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension);
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

        UnresolvedSharpMeasuresVectorGroupMemberDefinition product = new(definition.VectorGroup!.Value, processedDimensionality.Result, definition.GenerateDocumentation,
            definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return definition.Locations.ExplicitlySetVectorGroup;
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDimension)
        {
            if (definition.Dimension < 2)
            {
                return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidDimension(context, definition));
            }

            return OptionalWithDiagnostics.Result(definition.Dimension);
        }

        var trailingNumber = Regex.Match(context.Type.Name, @"\d+$", RegexOptions.RightToLeft);
        if (trailingNumber.Success)
        {
            if (int.TryParse(trailingNumber.Value, NumberStyles.None, CultureInfo.InvariantCulture, out int result))
            {
                if (result < 2)
                {
                    return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidInterpretedDimension(context, definition, result));
                }

                return OptionalWithDiagnostics.Result(result);
            }
        }

        return OptionalWithDiagnostics.Empty<int>(Diagnostics.MissingDimension(context, definition));
    }
}
