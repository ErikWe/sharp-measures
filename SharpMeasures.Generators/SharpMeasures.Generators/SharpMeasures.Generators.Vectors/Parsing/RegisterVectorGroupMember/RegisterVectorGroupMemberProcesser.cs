namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

internal interface IRegisterVectorGroupMemberProcessingDiagnostics
{
    public abstract Diagnostic? NullVector(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition);

    public abstract Diagnostic? MissingDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidInterpretedDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition, int dimension);
    public abstract Diagnostic? DuplicateDimension(IRegisterVectorGroupMemberProcessingContext cotnext, RawRegisterVectorGroupMemberDefinition definition, int dimension);
}

internal interface IRegisterVectorGroupMemberProcessingContext : IProcessingContext
{
    public abstract HashSet<int> ReservedDimensions { get; }
}

internal class RegisterVectorGroupMemberProcesser
    : AActionableProcesser<IRegisterVectorGroupMemberProcessingContext, RawRegisterVectorGroupMemberDefinition,UnresolvedRegisterVectorGroupMemberDefinition>
{
    private IRegisterVectorGroupMemberProcessingDiagnostics Diagnostics { get; }

    public RegisterVectorGroupMemberProcesser(IRegisterVectorGroupMemberProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition,
        UnresolvedRegisterVectorGroupMemberDefinition product)
    {
        context.ReservedDimensions.Add(product.Dimension);
    }

    public override IOptionalWithDiagnostics<UnresolvedRegisterVectorGroupMemberDefinition> Process(IRegisterVectorGroupMemberProcessingContext context,
        RawRegisterVectorGroupMemberDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedRegisterVectorGroupMemberDefinition>();
        }

        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedRegisterVectorGroupMemberDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return OptionalWithDiagnostics.Result(product.Result, allDiagnostics);
    }

    private static bool VerifyRequiredPropertiesSet(RawRegisterVectorGroupMemberDefinition definition)
    {
        return definition.Locations.ExplicitlySetVector;
    }

    private IOptionalWithDiagnostics<UnresolvedRegisterVectorGroupMemberDefinition> ProcessDefinition(IRegisterVectorGroupMemberProcessingContext context,
        RawRegisterVectorGroupMemberDefinition definition)
    {
        var processedDimensionality = ProcessDimension(context, definition);
        var allDiagnostics = processedDimensionality.Diagnostics;

        if (processedDimensionality.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedRegisterVectorGroupMemberDefinition>(allDiagnostics);
        }

        UnresolvedRegisterVectorGroupMemberDefinition product = new(definition.Vector!.Value, processedDimensionality.Result, definition.Locations);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition)
    {
        if (definition.Locations.ExplicitlySetDimension)
        {
            if (definition.Dimension < 2)
            {
                return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidDimension(context, definition));
            }

            if (context.ReservedDimensions.Contains(definition.Dimension))
            {
                return OptionalWithDiagnostics.Empty<int>(Diagnostics.DuplicateDimension(context, definition, definition.Dimension));
            }

            return OptionalWithDiagnostics.Result(definition.Dimension);
        }

        var trailingNumber = Regex.Match(context.Type.Name, @"\d+$", RegexOptions.RightToLeft);
        if (trailingNumber.Success)
        {
            if (int.TryParse(trailingNumber.Value, NumberStyles.None, CultureInfo.InvariantCulture, out int interpretedDimension))
            {
                if (interpretedDimension < 2)
                {
                    return OptionalWithDiagnostics.Empty<int>(Diagnostics.InvalidInterpretedDimension(context, definition, interpretedDimension));
                }

                if (context.ReservedDimensions.Contains(definition.Dimension))
                {
                    return OptionalWithDiagnostics.Empty<int>(Diagnostics.DuplicateDimension(context, definition, interpretedDimension));
                }

                return OptionalWithDiagnostics.Result(interpretedDimension);
            }
        }

        return OptionalWithDiagnostics.Empty<int>(Diagnostics.MissingDimension(context, definition));
    }

    private IValidityWithDiagnostics CheckValidity(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckVectorValidity);
    }

    private IValidityWithDiagnostics CheckVectorValidity(IRegisterVectorGroupMemberProcessingContext context, RawRegisterVectorGroupMemberDefinition definition)
    {
        if (definition.Vector is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullVector(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
