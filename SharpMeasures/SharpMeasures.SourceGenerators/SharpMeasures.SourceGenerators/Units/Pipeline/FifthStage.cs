namespace SharpMeasures.SourceGenerators.Units.Pipeline;

using SharpMeasures.Attributes;
using SharpMeasures.SourceGenerators.Documentation;
using SharpMeasures.SourceGenerators.Providers;
using SharpMeasures.SourceGenerators.Units.Attributes;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Threading;

internal static class FifthStage
{
    public readonly record struct UnitInstances(IEnumerable<FixedUnitInstanceAttributeParameters> Fixed, IEnumerable<AliasUnitInstanceAttributeParameters> Alias,
        IEnumerable<DerivedUnitInstanceAttributeParameters> Derived, IEnumerable<ScaledUnitInstanceAttributeParameters> Scaled,
        IEnumerable<PrefixedUnitInstanceAttributeParameters> Prefixed, IEnumerable<OffsetUnitInstanceAttributeParameters> Offset);

    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, UnitAttributeParameters Parameters, IEnumerable<DerivedUnitAttributeParameters> Derivations, UnitInstances Instances);

    private readonly record struct IntermediateResult(MarkedDeclarationSyntaxProvider.OutputData Declaration, IEnumerable<DocumentationFile> Documentation,
        INamedTypeSymbol TypeSymbol, UnitAttributeParameters Parameters, IEnumerable<AttributeData> DerivationData,
        IEnumerable<DerivedUnitAttributeParameters> Derivations, IEnumerable<AttributeData> FixedInstanceData,
        IEnumerable<FixedUnitInstanceAttributeParameters> FixedInstances, IEnumerable<AttributeData> AliasInstanceData,
        IEnumerable<AliasUnitInstanceAttributeParameters> AliasInstances, IEnumerable<AttributeData> DerivedInstanceData,
        IEnumerable<DerivedUnitInstanceAttributeParameters> DerivedInstances, IEnumerable<AttributeData> ScaledInstanceData,
        IEnumerable<ScaledUnitInstanceAttributeParameters> ScaledInstances, IEnumerable<AttributeData> PrefixedInstanceData,
        IEnumerable<PrefixedUnitInstanceAttributeParameters> PrefixedInstances, IEnumerable<AttributeData> OffsetInstanceData,
        IEnumerable<OffsetUnitInstanceAttributeParameters> OffsetInstances);
    
    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<FourthStage.Result> provider)
    {
        IncrementalValuesProvider<IntermediateResult> result = PerformIntermediate(provider);
        result = ParseParameters(result, DerivationTransforms);
        result = ParseParameters(result, FixedInstanceTransforms);
        result = ParseParameters(result, AliasInstanceTransforms);
        result = ParseParameters(result, DerivedInstanceTransforms);
        result = ParseParameters(result, ScaledInstanceTransforms);
        result = ParseParameters(result, PrefixedInstanceTransforms);
        result = ParseParameters(result, OffsetInstanceTransforms);
        return result.Select(outputTransform);

        static Result outputTransform(IntermediateResult input, CancellationToken token) => OutputTransform(input);
    }

    private static IncrementalValuesProvider<IntermediateResult> PerformIntermediate(IncrementalValuesProvider<FourthStage.Result> provider)
    {
        IncrementalValuesProvider<IntermediateResult> result = provider.Select(inputTransform);
        result = AddAttributeData(result, DerivationTransforms);
        result = AddAttributeData(result, FixedInstanceTransforms);
        result = AddAttributeData(result, AliasInstanceTransforms);
        result = AddAttributeData(result, DerivedInstanceTransforms);
        result = AddAttributeData(result, ScaledInstanceTransforms);
        result = AddAttributeData(result, PrefixedInstanceTransforms);
        result = AddAttributeData(result, OffsetInstanceTransforms);
        return result;

        static IntermediateResult inputTransform(FourthStage.Result input, CancellationToken token) => InputTransform(input);
    }

    private static IncrementalValuesProvider<IntermediateResult> AddAttributeData<TParameters, TParsedParameters>(IncrementalValuesProvider<IntermediateResult> provider,
        AttributeTransforms<TParameters, TParsedParameters> transforms)
        => MatchingAttributeDataProvider.Attach(provider, InputTransform, transforms.AttributeAppender, transforms.AttributeType.FullName);

    private static IncrementalValuesProvider<IntermediateResult> ParseParameters<TParameters, TParsedParameters>(IncrementalValuesProvider<IntermediateResult> provider,
        AttributeTransforms<TParameters, TParsedParameters> transforms)
        => AttributeParametersProvider.Attach(provider, transforms.AttributeRetriever, transforms.ParameterAppender, transforms.ParameterParser);

    private static IntermediateResult InputTransform(FourthStage.Result input) => new(input.Declaration, input.Documentation, input.TypeSymbol, input.Parameters,
        Array.Empty<AttributeData>(), Array.Empty<DerivedUnitAttributeParameters>(), Array.Empty<AttributeData>(), Array.Empty<FixedUnitInstanceAttributeParameters>(),
        Array.Empty<AttributeData>(), Array.Empty<AliasUnitInstanceAttributeParameters>(), Array.Empty<AttributeData>(), Array.Empty<DerivedUnitInstanceAttributeParameters>(),
        Array.Empty<AttributeData>(), Array.Empty<ScaledUnitInstanceAttributeParameters>(), Array.Empty<AttributeData>(),
        Array.Empty<PrefixedUnitInstanceAttributeParameters>(), Array.Empty<AttributeData>(), Array.Empty<OffsetUnitInstanceAttributeParameters>());
    private static INamedTypeSymbol InputTransform(IntermediateResult input) => input.TypeSymbol;

    private static Result OutputTransform(IntermediateResult input)
        => new(input.Declaration, input.Documentation, input.TypeSymbol, input.Parameters, input.Derivations, new UnitInstances(input.FixedInstances, input.AliasInstances,
            input.DerivedInstances, input.ScaledInstances, input.PrefixedInstances, input.OffsetInstances));

    private readonly record struct AttributeTransforms<TParameters, TParsedParameters>(
        MatchingAttributeDataProvider.DOutputTransformMultiple<IntermediateResult, IntermediateResult> AttributeAppender,
        AttributeParametersProvider.DInputTransformMultiple<IntermediateResult> AttributeRetriever,
        AttributeParametersProvider.DOutputTransformMultiple<TParameters, IntermediateResult, IntermediateResult> ParameterAppender,
        AttributeParametersProvider.DParameterTransform<TParsedParameters> ParameterParser,
        Type AttributeType);

    private static AttributeTransforms<DerivedUnitAttributeParameters, DerivedUnitAttributeParameters?> DerivationTransforms { get; } = new(
        static (result, data) => result with { DerivationData = data },
        static (result) => result.DerivationData,
        static (result, parameters) => result with { Derivations = parameters },
        DerivedUnitAttributeParameters.Parse,
        typeof(DerivedUnitAttribute)
    );

    private static AttributeTransforms<FixedUnitInstanceAttributeParameters, FixedUnitInstanceAttributeParameters?> FixedInstanceTransforms { get; } = new(
        static (result, data) => result with { FixedInstanceData = data },
        static (result) => result.FixedInstanceData,
        static (result, parameters) => result with { FixedInstances = parameters },
        FixedUnitInstanceAttributeParameters.Parse,
        typeof(FixedUnitInstanceAttribute)
    );

    private static AttributeTransforms<AliasUnitInstanceAttributeParameters, AliasUnitInstanceAttributeParameters?> AliasInstanceTransforms { get; } = new(
        static (result, data) => result with { AliasInstanceData = data },
        static (result) => result.AliasInstanceData,
        static (result, parameters) => result with { AliasInstances = parameters },
        AliasUnitInstanceAttributeParameters.Parse,
        typeof(AliasUnitInstanceAttribute)
    );

    private static AttributeTransforms<DerivedUnitInstanceAttributeParameters, DerivedUnitInstanceAttributeParameters?> DerivedInstanceTransforms { get; } = new(
        static (result, data) => result with { DerivedInstanceData = data },
        static (result) => result.DerivedInstanceData,
        static (result, parameters) => result with { DerivedInstances = parameters },
        DerivedUnitInstanceAttributeParameters.Parse,
        typeof(DerivedUnitInstanceAttribute)
    );

    private static AttributeTransforms<ScaledUnitInstanceAttributeParameters, ScaledUnitInstanceAttributeParameters?> ScaledInstanceTransforms { get; } = new(
        static (result, data) => result with { ScaledInstanceData = data },
        static (result) => result.ScaledInstanceData,
        static (result, parameters) => result with { ScaledInstances = parameters },
        ScaledUnitInstanceAttributeParameters.Parse,
        typeof(ScaledUnitInstanceAttribute)
    );

    private static AttributeTransforms<PrefixedUnitInstanceAttributeParameters, PrefixedUnitInstanceAttributeParameters?> PrefixedInstanceTransforms { get; } = new(
        static (result, data) => result with { PrefixedInstanceData = data },
        static (result) => result.PrefixedInstanceData,
        static (result, parameters) => result with { PrefixedInstances = parameters },
        PrefixedUnitInstanceAttributeParameters.Parse,
        typeof(PrefixedUnitInstanceAttribute)
    );

    private static AttributeTransforms<OffsetUnitInstanceAttributeParameters, OffsetUnitInstanceAttributeParameters?> OffsetInstanceTransforms { get; } = new(
        static (result, data) => result with { OffsetInstanceData = data },
        static (result) => result.OffsetInstanceData,
        static (result, parameters) => result with { OffsetInstances = parameters },
        OffsetUnitInstanceAttributeParameters.Parse,
        typeof(OffsetUnitInstanceAttribute)
    );
}
