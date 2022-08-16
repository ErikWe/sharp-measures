namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class VectorParser
{
    public static (IncrementalValueProvider<IRawVectorPopulation>, IVectorResolver) Attach(IncrementalGeneratorInitializationContext context)
    {
        var vectorGroupBaseSymbols = AttachSymbolProvider<SharpMeasuresVectorGroupAttribute>(context, GroupBaseDeclarationFilters);
        var vectorGroupSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresVectorGroupAttribute>(context, GroupSpecializationDeclarationFilters);
        var vectorGroupMemberSymbols = AttachSymbolProvider<SharpMeasuresVectorGroupMemberAttribute>(context, GroupMemberDeclarationFilters);

        var individualVectorBaseSymbols = AttachSymbolProvider<SharpMeasuresVectorAttribute>(context, IndividualBaseDeclarationFilters);
        var individualVectorSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresVectorAttribute>(context, IndividualSpecializationDeclarationFilters);

        VectorGroupBaseParser vectorGroupBaseParser = new();
        VectorGroupSpecializationParser vectorGroupSpecializationParser = new();

        IndividualVectorBaseParser individualVectorBaseParser = new();
        IndividualVectorSpecializationParser individualVectorSpecializationParser = new();

        var parsedVectorGroupBases = vectorGroupBaseSymbols.Select(vectorGroupBaseParser.Parse).ReportDiagnostics(context);
        var parsedVectorGroupSpecializations = vectorGroupSpecializationSymbols.Select(vectorGroupSpecializationParser.Parse).ReportDiagnostics(context);
        var parsedVectorGroupMembers = vectorGroupMemberSymbols.Select(VectorGroupMemberParsing.Parse).ReportDiagnostics(context);

        var parsedIndividualVectorBases = individualVectorBaseSymbols.Select(individualVectorBaseParser.Parse).ReportDiagnostics(context);
        var parsedIndividualVectorSpecializations = individualVectorSpecializationSymbols.Select(individualVectorSpecializationParser.Parse).ReportDiagnostics(context);

        VectorGroupBaseProcesser vectorGroupBaseProcesser = new();
        VectorGroupSpecializationProcesser vectorGroupSpecializationProcesser = new();

        IndividualVectorBaseProcesser individualVectorBaseProcesser = new();
        IndividualVectorSpecializationProcesser individualVectorSpecializationProcesser = new();

        var processedVectorGroupBases = parsedVectorGroupBases.Select(vectorGroupBaseProcesser.Process).ReportDiagnostics(context);
        var processedVectorGroupSpecializations = parsedVectorGroupSpecializations.Select(vectorGroupSpecializationProcesser.Process).ReportDiagnostics(context);
        var processedVectorGroupMembers = parsedVectorGroupMembers.Select(VectorGroupMemberProcessing.Process).ReportDiagnostics(context);

        var processedIndividualVectorBases = parsedIndividualVectorBases.Select(individualVectorBaseProcesser.Process).ReportDiagnostics(context);
        var processedIndividualVectorSpecializations = parsedIndividualVectorSpecializations.Select(individualVectorSpecializationProcesser.Process).ReportDiagnostics(context);

        var vectorGroupBaseInterfaces = processedVectorGroupBases.Select(ExtractInterface).Collect();
        var vectorGroupSpecializationInterfaces = processedVectorGroupSpecializations.Select(ExtractInterface).Collect();
        var vectorGroupMemberInterfaces = processedVectorGroupMembers.Select(ExtractInterface).Collect();

        var individualVectorBaseInterfaces = processedIndividualVectorBases.Select(ExtractInterface).Collect();
        var individualVectorSpecializationInterfaces = processedIndividualVectorSpecializations.Select(ExtractInterface).Collect();

        var population = vectorGroupBaseInterfaces.Combine(vectorGroupSpecializationInterfaces, vectorGroupMemberInterfaces, individualVectorBaseInterfaces,
            individualVectorSpecializationInterfaces).Select(CreatePopulation);

        var reducedPopulation = population.Select(ExtractInterface);

        return (reducedPopulation, new VectorResolver(population, processedVectorGroupBases, processedVectorGroupSpecializations, processedVectorGroupMembers, processedIndividualVectorBases,
            processedIndividualVectorSpecializations));
    }

    private static IncrementalValuesProvider<IntermediateResult> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context,
        IEnumerable<IDeclarationFilter> declarationFilters)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(declarationFilters).AttachAndReport(context, declarations);
        return DeclarationSymbolProvider.ConstructForValueType(IntermediateResult.Construct).Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static IRawVectorGroupBaseType ExtractInterface(IRawVectorGroupBaseType vectorGroupType, CancellationToken _) => vectorGroupType;
    private static IRawVectorGroupSpecializationType ExtractInterface(IRawVectorGroupSpecializationType vectorGroupType, CancellationToken _) => vectorGroupType;
    private static IRawVectorGroupMemberType ExtractInterface(IRawVectorGroupMemberType groupMemberType, CancellationToken _) => groupMemberType;

    private static IRawVectorBaseType ExtractInterface(IRawVectorBaseType vectorType, CancellationToken _) => vectorType;
    private static IRawVectorSpecializationType ExtractInterface(IRawVectorSpecializationType vectorType, CancellationToken _) => vectorType;

    private static IRawVectorPopulation ExtractInterface(IRawVectorPopulation population, CancellationToken _) => population;

    private static IUnresolvedVectorPopulationWithData CreatePopulation
        ((ImmutableArray<IRawVectorGroupBaseType> BaseGroups, ImmutableArray<IRawVectorGroupSpecializationType> SpecializedGroups,
        ImmutableArray<IRawVectorGroupMemberType> GroupMembers, ImmutableArray<IRawVectorBaseType> Bases,
        ImmutableArray<IRawVectorSpecializationType> Specialized) vectors, CancellationToken _)
    {
        return UnresolvedVectorPopulation.Build(vectors.BaseGroups, vectors.SpecializedGroups, vectors.GroupMembers, vectors.Bases, vectors.Specialized);
    }

    private class VectorGroupBaseParser : AVectorGroupParser<RawSharpMeasuresVectorGroupDefinition, RawVectorGroupBaseType>
    {
        protected override RawVectorGroupBaseType FinalizeParse(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorGroupDefinition definition,
            IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions,
            IEnumerable<RawUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions);
        }

        protected override IAttributeParser<RawSharpMeasuresVectorGroupDefinition> Parser => SharpMeasuresVectorGroupParser.Parser;
    }

    private class VectorGroupSpecializationParser : AVectorGroupParser<RawSpecializedSharpMeasuresVectorGroupDefinition, RawVectorGroupSpecializationType>
    {
        protected override RawVectorGroupSpecializationType FinalizeParse(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorGroupDefinition definition,
            IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions,
            IEnumerable<RawUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions);
        }

        protected override IAttributeParser<RawSpecializedSharpMeasuresVectorGroupDefinition> Parser => SpecializedSharpMeasuresVectorGroupParser.Parser;
    }

    private abstract class AVectorGroupParser<TDefinition, TProduct>
        where TProduct : ARawVectorGroupType
    {
        public IOptionalWithDiagnostics<TProduct> Parse(IntermediateResult input, CancellationToken _) => Parse(input);
        public IOptionalWithDiagnostics<TProduct> Parse(IntermediateResult input)
        {
            if (Parser.ParseFirstOccurrence(input.TypeSymbol) is not TDefinition vectorGroup)
            {
                return OptionalWithDiagnostics.EmptyWithoutDiagnostics<TProduct>();
            }

            var type = input.TypeSymbol.AsDefinedType();
            var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

            var derivations = DerivedQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var conversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            var unitInclusions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var unitExclusions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            TProduct product = FinalizeParse(type, typeLocation, vectorGroup, derivations, conversions, unitInclusions, unitExclusions);

            return OptionalWithDiagnostics.Result(product);
        }

        protected abstract TProduct FinalizeParse(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
            IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions);

        protected abstract IAttributeParser<TDefinition> Parser { get; }
    }

    private class VectorGroupMemberParsing
    {
        public static IOptionalWithDiagnostics<RawVectorGroupMemberType> Parse(IntermediateResult input, CancellationToken _) => Parse(input);
        public static IOptionalWithDiagnostics<RawVectorGroupMemberType> Parse(IntermediateResult input)
        {
            if (SharpMeasuresVectorGroupMemberParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawSharpMeasuresVectorGroupMemberDefinition vectorGroup)
            {
                return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawVectorGroupMemberType>();
            }

            var type = input.TypeSymbol.AsDefinedType();
            var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

            var constants = VectorConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            RawVectorGroupMemberType product = new(type, typeLocation, vectorGroup, constants);

            return OptionalWithDiagnostics.Result(product);
        }
    }

    private class IndividualVectorBaseParser : AIndividualVectorParser<RawSharpMeasuresVectorDefinition, RawIndividualVectorBaseType>
    {
        protected override RawIndividualVectorBaseType FinalizeParse(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorDefinition definition,
            IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants,
            IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions);
        }

        protected override IAttributeParser<RawSharpMeasuresVectorDefinition> Parser => SharpMeasuresVectorParser.Parser;
    }

    private class IndividualVectorSpecializationParser : AIndividualVectorParser<RawSpecializedSharpMeasuresVectorDefinition, RawIndividualVectorSpecializationType>
    {
        protected override RawIndividualVectorSpecializationType FinalizeParse(DefinedType type, MinimalLocation typeLocation,
            RawSpecializedSharpMeasuresVectorDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants,
            IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions);
        }

        protected override IAttributeParser<RawSpecializedSharpMeasuresVectorDefinition> Parser => SpecializedSharpMeasuresVectorParser.Parser;
    }

    private abstract class AIndividualVectorParser<TDefinition, TProduct>
        where TProduct : ARawIndividualVectorType
    {
        public IOptionalWithDiagnostics<TProduct> Parse(IntermediateResult input, CancellationToken _) => Parse(input);
        public IOptionalWithDiagnostics<TProduct> Parse(IntermediateResult input)
        {
            if (Parser.ParseFirstOccurrence(input.TypeSymbol) is not TDefinition vectorGroup)
            {
                return OptionalWithDiagnostics.EmptyWithoutDiagnostics<TProduct>();
            }

            var type = input.TypeSymbol.AsDefinedType();
            var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

            var derivations = DerivedQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var constants = VectorConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var conversions = ConvertibleQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            var unitInclusions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
            var unitExclusions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

            TProduct product = FinalizeParse(type, typeLocation, vectorGroup, derivations, constants, conversions, unitInclusions, unitExclusions);

            return OptionalWithDiagnostics.Result(product);
        }

        protected abstract TProduct FinalizeParse(DefinedType type, MinimalLocation typeLocation, TDefinition definition,
            IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
            IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions);

        protected abstract IAttributeParser<TDefinition> Parser { get; }
    }

    private class VectorGroupBaseProcesser : AVectorGroupProcesser<UnresolvedSharpMeasuresVectorGroupDefinition, RawVectorGroupBaseType, UnresolvedVectorGroupBaseType>
    {
        protected override UnresolvedVectorGroupBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresVectorGroupDefinition definition,
            IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions);
        }

        protected override NamedType? GetUnit(UnresolvedSharpMeasuresVectorGroupDefinition scalar) => scalar.Unit;

        protected override IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorGroupDefinition> ProcessVectorGroup(RawVectorGroupBaseType raw)
        {
            IProcessingContext processingContext = new SimpleProcessingContext(raw.Type);

            return Processers.SharpMeasuresVectorGroupProcesser.Process(processingContext, raw.Definition);
        }
    }

    private class VectorGroupSpecializationProcesser : AVectorGroupProcesser<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition, RawVectorGroupSpecializationType,
        UnresolvedVectorGroupSpecializationType>
    {
        protected override UnresolvedVectorGroupSpecializationType FinalizeProcess(DefinedType type, MinimalLocation typeLocation,
            UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
            IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions);
        }

        protected override NamedType? GetUnit(UnresolvedSpecializedSharpMeasuresVectorGroupDefinition scalar) => null;

        protected override IOptionalWithDiagnostics<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition> ProcessVectorGroup(RawVectorGroupSpecializationType raw)
        {
            IProcessingContext processingContext = new SimpleProcessingContext(raw.Type);

            return Processers.SpecializedSharpMeasuresVectorGroupProcesser.Process(processingContext, raw.Definition);
        }
    }

    private abstract class AVectorGroupProcesser<TDefinition, TRaw, TProduct>
        where TDefinition : IRawVectorGroup
        where TRaw : ARawVectorGroupType
        where TProduct : AUnresolvedVectorGroupType<TDefinition>
    {
        public IOptionalWithDiagnostics<TProduct> Process(TRaw raw, CancellationToken _) => Process(raw);
        public IOptionalWithDiagnostics<TProduct> Process(TRaw raw)
        {
            var vectorGroup = ProcessVectorGroup(raw);
            var allDiagnostics = vectorGroup.Diagnostics;

            if (vectorGroup.LacksResult)
            {
                return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
            }

            DerivedQuantityProcessingContext derivedQuantityProcessingContext = new(raw.Type);
            ConvertibleQuantityProcessingContext convertibleQuantityProcessingContext = new(raw.Type);
            UnitListProcessingContext unitListProcessingContext = new(raw.Type);

            var derivations = ProcessingFilter.Create(Processers.DerivedQuantityProcesser).Filter(derivedQuantityProcessingContext, raw.Derivations);
            var convertibleVectors = ProcessingFilter.Create(Processers.ConvertibleVectorProcesser).Filter(convertibleQuantityProcessingContext, raw.Conversions);

            var includeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.UnitInclusions);
            var excludeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.UnitExclusions);

            allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(convertibleVectors.Diagnostics)
                .Concat(includeUnits.Diagnostics).Concat(excludeUnits.Diagnostics);

            if (includeUnits.HasResult && includeUnits.Result.Count > 0 && excludeUnits.HasResult && excludeUnits.Result.Count > 0)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(raw.TypeLocation) });
                excludeUnits = ResultWithDiagnostics.Construct(Array.Empty<UnresolvedUnitListDefinition>() as IReadOnlyList<UnresolvedUnitListDefinition>);
            }

            var product = FinalizeProcess(raw.Type, raw.TypeLocation, vectorGroup.Result, derivations.Result, convertibleVectors.Result, includeUnits.Result, excludeUnits.Result);

            return OptionalWithDiagnostics.Result(product, allDiagnostics);
        }

        protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
            IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions);

        protected abstract NamedType? GetUnit(TDefinition scalar);

        protected abstract IOptionalWithDiagnostics<TDefinition> ProcessVectorGroup(TRaw raw);
    }

    private class VectorGroupMemberProcessing
    {
        public static IOptionalWithDiagnostics<UnresolvedVectorGroupMemberType> Process(RawVectorGroupMemberType raw, CancellationToken _) => Process(raw);
        public static IOptionalWithDiagnostics<UnresolvedVectorGroupMemberType> Process(RawVectorGroupMemberType raw)
        {
            IProcessingContext processingContext = new SimpleProcessingContext(raw.Type);

            var vectorGroupMember = Processers.SharpMeasuresVectorGroupMemberProcesser.Process(processingContext, raw.Definition);
            var allDiagnostics = vectorGroupMember.Diagnostics;

            if (vectorGroupMember.LacksResult)
            {
                return OptionalWithDiagnostics.Empty<UnresolvedVectorGroupMemberType>(allDiagnostics);
            }

            QuantityConstantProcessingContext vectorConstantProcessingContext = new(raw.Type);

            var constants = ProcessingFilter.Create(Processers.VectorConstantProcesserForUnknownUnit).Filter(vectorConstantProcessingContext, raw.Constants);
            allDiagnostics = allDiagnostics.Concat(constants.Diagnostics);

            UnresolvedVectorGroupMemberType product = new(raw.Type, raw.TypeLocation, vectorGroupMember.Result, constants.Result);

            return OptionalWithDiagnostics.Result(product, allDiagnostics);
        }
    }

    private class IndividualVectorBaseProcesser : AIndividualVectorProcesser<UnresolvedSharpMeasuresVectorDefinition, RawIndividualVectorBaseType,
        UnresolvedIndividualVectorBaseType>
    {
        protected override UnresolvedIndividualVectorBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresVectorDefinition definition,
            IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedVectorConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions);
        }

        protected override NamedType? GetUnit(UnresolvedSharpMeasuresVectorDefinition scalar) => scalar.Unit;

        protected override IOptionalWithDiagnostics<UnresolvedSharpMeasuresVectorDefinition> ProcessIndividualVector(RawIndividualVectorBaseType raw)
        {
            IProcessingContext processingContext = new SimpleProcessingContext(raw.Type);

            return Processers.SharpMeasuresVectorProcesser.Process(processingContext, raw.Definition);
        }
    }

    private class IndividualVectorSpecializationProcesser : AIndividualVectorProcesser<UnresolvedSpecializedSharpMeasuresVectorDefinition, RawIndividualVectorSpecializationType,
        UnresolvedIndividualVectorSpecializationType>
    {
        protected override UnresolvedIndividualVectorSpecializationType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, UnresolvedSpecializedSharpMeasuresVectorDefinition definition,
            IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedVectorConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        {
            return new(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions);
        }

        protected override NamedType? GetUnit(UnresolvedSpecializedSharpMeasuresVectorDefinition scalar) => null;

        protected override IOptionalWithDiagnostics<UnresolvedSpecializedSharpMeasuresVectorDefinition> ProcessIndividualVector(RawIndividualVectorSpecializationType raw)
        {
            IProcessingContext processingContext = new SimpleProcessingContext(raw.Type);

            return Processers.SpecializedSharpMeasuresVectorProcesser.Process(processingContext, raw.Definition);
        }
    }

    private abstract class AIndividualVectorProcesser<TDefinition, TRaw, TProduct>
        where TDefinition : IRawVector
        where TRaw : ARawIndividualVectorType
        where TProduct : AUnresolvedIndividualVectorType<TDefinition>
    {
        public IOptionalWithDiagnostics<TProduct> Process(TRaw raw, CancellationToken _) => Process(raw);
        public IOptionalWithDiagnostics<TProduct> Process(TRaw raw)
        {
            var vector = ProcessIndividualVector(raw);
            var allDiagnostics = vector.Diagnostics;

            if (vector.LacksResult)
            {
                return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
            }

            DerivedQuantityProcessingContext derivedQuantityProcessingContext = new(raw.Type);
            QuantityConstantProcessingContext vectorConstantProcessingContext = new(raw.Type);
            ConvertibleQuantityProcessingContext convertibleQuantityProcessingContext = new(raw.Type);
            UnitListProcessingContext unitListProcessingContext = new(raw.Type);

            var unit = GetUnit(vector.Result);

            var vectorConstantProcesser = unit is null
                ? Processers.VectorConstantProcesserForUnknownUnit
                : Processers.VectorConstantProcesser(unit.Value);

            var derivations = ProcessingFilter.Create(Processers.DerivedQuantityProcesser).Filter(derivedQuantityProcessingContext, raw.Derivations);
            var constants = ProcessingFilter.Create(vectorConstantProcesser).Filter(vectorConstantProcessingContext, raw.Constants);
            var convertibleVectors = ProcessingFilter.Create(Processers.ConvertibleVectorProcesser).Filter(convertibleQuantityProcessingContext, raw.Conversions);

            var includeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.UnitInclusions);
            var excludeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, raw.UnitExclusions);

            allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(convertibleVectors.Diagnostics)
                .Concat(includeUnits.Diagnostics).Concat(excludeUnits.Diagnostics);

            if (includeUnits.HasResult && includeUnits.Result.Count > 0 && excludeUnits.HasResult && excludeUnits.Result.Count > 0)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(raw.TypeLocation) });
                excludeUnits = ResultWithDiagnostics.Construct(Array.Empty<UnresolvedUnitListDefinition>() as IReadOnlyList<UnresolvedUnitListDefinition>);
            }

            TProduct product = FinalizeProcess(raw.Type, raw.TypeLocation, vector.Result, derivations.Result, constants.Result, convertibleVectors.Result, includeUnits.Result, excludeUnits.Result);

            return OptionalWithDiagnostics.Result(product, allDiagnostics);
        }

        protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
            IReadOnlyList<UnresolvedVectorConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
            IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions);

        protected abstract NamedType? GetUnit(TDefinition vector);

        protected abstract IOptionalWithDiagnostics<TDefinition> ProcessIndividualVector(TRaw raw);
    }

    private static IEnumerable<IDeclarationFilter> GroupBaseDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(VectorGroupBaseTypeDiagnostics.TypeNotPartial),
        new StaticDeclarationFilter(VectorGroupBaseTypeDiagnostics.TypeNotStatic)
    };

    private static IEnumerable<IDeclarationFilter> GroupSpecializationDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(VectorGroupSpecializationTypeDiagnostics.TypeNotPartial),
        new StaticDeclarationFilter(VectorGroupSpecializationTypeDiagnostics.TypeNotStatic)
    };

    private static IEnumerable<IDeclarationFilter> GroupMemberDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(VectorGroupMemberTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(VectorGroupMemberTypeDiagnostics.TypeStatic)
    };

    private static IEnumerable<IDeclarationFilter> IndividualBaseDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(IndividualVectorBaseTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(IndividualVectorBaseTypeDiagnostics.TypeStatic)
    };

    private static IEnumerable<IDeclarationFilter> IndividualSpecializationDeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(IndividualVectorSpecializationTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(IndividualVectorSpecializationTypeDiagnostics.TypeStatic)
    };

    private static class Processers
    {
        public static SharpMeasuresVectorProcesser SharpMeasuresVectorProcesser { get; } = new(SharpMeasuresVectorProcessingDiagnostics.Instance);
        public static SpecializedSharpMeasuresVectorProcesser SpecializedSharpMeasuresVectorProcesser { get; } = new(SpecializedSharpMeasuresVectorProcessingDiagnostics.Instance);

        public static SharpMeasuresVectorGroupProcesser SharpMeasuresVectorGroupProcesser { get; } = new(SharpMeasuresVectorGroupProcessingDiagnostics.Instance);
        public static SpecializedSharpMeasuresVectorGroupProcesser SpecializedSharpMeasuresVectorGroupProcesser { get; } = new(SpecializedSharpMeasuresVectorGroupProcessingDiagnostics.Instance);

        public static SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser { get; } = new(SharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance);

        public static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
        public static VectorConstantProcesser VectorConstantProcesser(NamedType unit) => new(new QuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>(unit));
        public static VectorConstantProcesser VectorConstantProcesserForUnknownUnit { get; } = new(new QuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>());
        public static ConvertibleVectorProcesser ConvertibleVectorProcesser { get; } = new(ConvertibleVectorProcessingDiagnostics.Instance);

        public static UnitListProcesser UnitListProcesser { get; } = new(UnitListProcessingDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> Construct => (declaration, symbol) => new(declaration, symbol);
    }
}
