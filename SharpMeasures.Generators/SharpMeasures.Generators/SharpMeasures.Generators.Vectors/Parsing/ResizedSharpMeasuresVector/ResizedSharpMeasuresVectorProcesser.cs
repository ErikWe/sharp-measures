namespace SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

internal interface IResizedSharpMeasuresVectorProcessingDiagnostics
{
    public abstract Diagnostic? NullAssociatedVector(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition);
}

internal class ResizedSharpMeasuresVectorProcesser : AProcesser<IProcessingContext, RawResizedSharpMeasuresVectorDefinition, ResizedSharpMeasuresVectorDefinition>
{
    private IResizedSharpMeasuresVectorProcessingDiagnostics Diagnostics { get; }

    public ResizedSharpMeasuresVectorProcesser(IResizedSharpMeasuresVectorProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ResizedSharpMeasuresVectorDefinition> Process(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ResizedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IOptionalWithDiagnostics<ResizedSharpMeasuresVectorDefinition> ProcessDefinition(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        var processedDimensionality = ProcessDimension(context, definition);
        var allDiagnostics = processedDimensionality.Diagnostics;

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ResizedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        ResizedSharpMeasuresVectorDefinition product = new(definition.AssociatedVector!.Value, processedDimensionality.Result, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
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
                return OptionalWithDiagnostics.Result(result);
            }

            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<int>();
        }

        return OptionalWithDiagnostics.Empty<int>(Diagnostics.MissingDimension(context, definition));
    }

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        return CheckVectorValidity(context, definition);
    }

    private IValidityWithDiagnostics CheckVectorValidity(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        if (definition.AssociatedVector is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullAssociatedVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
