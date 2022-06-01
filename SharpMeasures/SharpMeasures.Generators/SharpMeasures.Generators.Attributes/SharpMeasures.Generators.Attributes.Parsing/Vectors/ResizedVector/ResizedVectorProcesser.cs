namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public interface IResizedVectorDiagnostics
{
    public abstract Diagnostic? NullAssociatedVector(IProcessingContext context, RawResizedVectorDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawResizedVectorDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawResizedVectorDefinition definition);
}

public class ResizedVectorProcesser : AProcesser<IProcessingContext, RawResizedVectorDefinition, ResizedVectorDefinition>
{
    private IResizedVectorDiagnostics Diagnostics { get; }

    public ResizedVectorProcesser(IResizedVectorDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ResizedVectorDefinition> Process(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ResizedVectorDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IOptionalWithDiagnostics<ResizedVectorDefinition> ProcessDefinition(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        var processedDimensionality = ProcessDimension(context, definition);
        var allDiagnostics = processedDimensionality.Diagnostics;

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ResizedVectorDefinition>(allDiagnostics);
        }

        ResizedVectorDefinition product = new(definition.AssociatedVector!.Value, processedDimensionality.Result, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawResizedVectorDefinition definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return CheckVectorValidity(context, definition);
    }

    private IValidityWithDiagnostics CheckVectorValidity(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        if (definition.AssociatedVector is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullAssociatedVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
