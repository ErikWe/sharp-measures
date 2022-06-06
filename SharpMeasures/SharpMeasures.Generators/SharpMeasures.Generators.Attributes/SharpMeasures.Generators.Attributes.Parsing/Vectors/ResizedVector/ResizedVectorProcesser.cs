namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public interface IResizedVectorDiagnostics
{
    public abstract Diagnostic? NullAssociatedVector(IProcessingContext context, RawResizedVector definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawResizedVector definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawResizedVector definition);
}

public class ResizedVectorProcesser : AProcesser<IProcessingContext, RawResizedVector, ResizedVector>
{
    private IResizedVectorDiagnostics Diagnostics { get; }

    public ResizedVectorProcesser(IResizedVectorDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ResizedVector> Process(IProcessingContext context, RawResizedVector definition)
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
            return OptionalWithDiagnostics.Empty<ResizedVector>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IOptionalWithDiagnostics<ResizedVector> ProcessDefinition(IProcessingContext context, RawResizedVector definition)
    {
        var processedDimensionality = ProcessDimension(context, definition);
        var allDiagnostics = processedDimensionality.Diagnostics;

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ResizedVector>(allDiagnostics);
        }

        ResizedVector product = new(definition.AssociatedVector!.Value, processedDimensionality.Result, definition.GenerateDocumentation, definition.Locations);

        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawResizedVector definition)
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

    private IValidityWithDiagnostics CheckValidity(IProcessingContext context, RawResizedVector definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return CheckVectorValidity(context, definition);
    }

    private IValidityWithDiagnostics CheckVectorValidity(IProcessingContext context, RawResizedVector definition)
    {
        if (definition.AssociatedVector is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullAssociatedVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
