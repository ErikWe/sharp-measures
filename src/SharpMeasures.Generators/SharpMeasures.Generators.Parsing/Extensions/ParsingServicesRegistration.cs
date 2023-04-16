namespace SharpMeasures.Generators.Parsing.Extensions;

using Microsoft.Extensions.DependencyInjection;

using SharpAttributeParser.Extensions;

using SharpMeasures.Generators.Parsing.Quantities;
using SharpMeasures.Generators.Parsing.Scalars;
using SharpMeasures.Generators.Parsing.Units;
using SharpMeasures.Generators.Parsing.Vectors;

/// <summary>Allows the services offered by <i>SharpMeasures.Generators.Parsing</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class ParsingServicesRegistration
{
    /// <summary>Registers services offered by <i>SharpMeasures.Generators.Parsing</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> that the services are registered with.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpMeasuresAttributeParsing(this IServiceCollection services)
    {
        services.AddSharpAttributeParser();

        AddSharpMeasuresUnitAttributesParsing(services);
        AddSharpMeasuresQuantityAttributesParsing(services);
        AddSharpMeasuresScalarAttributesParsing(services);
        AddSharpMeasuresVectorAttributesParsing(services);

        return services;
    }

    private static void AddSharpMeasuresUnitAttributesParsing(IServiceCollection services)
    {
        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawUnit>, UnitAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawUnit>, UnitAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawFixedUnitInstance>, FixedUnitInstanceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance>, FixedUnitInstanceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawScaledUnitInstance>, ScaledUnitInstanceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawScaledUnitInstance>, ScaledUnitInstanceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawBiasedUnitInstance>, BiasedUnitInstanceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance>, BiasedUnitInstanceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance>, PrefixedUnitInstanceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance>, PrefixedUnitInstanceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance>, AliasedUnitInstanceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance>, AliasedUnitInstanceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance>, DerivedUnitInstanceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance>, DerivedUnitInstanceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawDerivableUnit>, DerivableUnitAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawDerivableUnit>, DerivableUnitAttributeParser>();
    }

    private static void AddSharpMeasuresQuantityAttributesParsing(IServiceCollection services)
    {
        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawConvertibleQuantity>, ConvertibleQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity>, ConvertibleQuantityAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawDefaultUnit>, DefaultUnitAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawDefaultUnit>, DefaultUnitAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawExcludeUnits>, ExcludeUnitsAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawExcludeUnits>, ExcludeUnitsAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawIncludeUnits>, IncludeUnitsAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawIncludeUnits>, IncludeUnitsAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawQuantityDifference>, QuantityDifferenceAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawQuantityDifference>, QuantityDifferenceAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawQuantityOperation>, QuantityOperationAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawQuantityOperation>, QuantityOperationAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawQuantityProcess>, QuantityProcessAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawQuantityProcess>, QuantityProcessAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawQuantityProperty>, QuantityPropertyAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawQuantityProperty>, QuantityPropertyAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawQuantitySum>, QuantitySumAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawQuantitySum>, QuantitySumAttributeParser>();
    }

    private static void AddSharpMeasuresScalarAttributesParsing(IServiceCollection services)
    {
        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawExcludeUnitBases>, ExcludeUnitBasesAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases>, ExcludeUnitBasesAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawIncludeUnitBases>, IncludeUnitBasesAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases>, IncludeUnitBasesAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawScalarConstant>, ScalarConstantAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawScalarConstant>, ScalarConstantAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawScalarQuantity>, ScalarQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawScalarQuantity>, ScalarQuantityAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity>, SpecializedScalarQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawSpecializedScalarQuantity>, SpecializedScalarQuantityAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawSpecializedUnitlessQuantity>, SpecializedUnitlessQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity>, SpecializedUnitlessQuantityAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawUnitlessQuantity>, UnitlessQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity>, UnitlessQuantityAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawVectorAssociation>, VectorAssociationAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawVectorAssociation>, VectorAssociationAttributeParser>();
    }

    private static void AddSharpMeasuresVectorAttributesParsing(IServiceCollection services)
    {
        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawScalarAssociation>, ScalarAssociationAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawScalarAssociation>, ScalarAssociationAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup>, SpecializedVectorGroupAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup>, SpecializedVectorGroupAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity>, SpecializedVectorQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity>, SpecializedVectorQuantityAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawVectorComponentNames>, VectorComponentNamesAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawVectorComponentNames>, VectorComponentNamesAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawVectorConstant>, VectorConstantAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawVectorConstant>, VectorConstantAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawVectorGroup>, VectorGroupAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawVectorGroup>, VectorGroupAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawVectorGroupMember>, VectorGroupMemberAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawVectorGroupMember>, VectorGroupMemberAttributeParser>();

        services.AddSingleton<IConstructiveSemanticAttributeParser<IRawVectorQuantity>, VectorQuantityAttributeParser>();
        services.AddSingleton<IConstructiveSyntacticAttributeParser<IRawVectorQuantity>, VectorQuantityAttributeParser>();
    }
}
