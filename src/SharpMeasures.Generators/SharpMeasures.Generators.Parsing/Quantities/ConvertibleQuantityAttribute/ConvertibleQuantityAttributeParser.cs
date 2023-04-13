namespace SharpMeasures.Generators.Parsing.Quantities.ConvertibleQuantityAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ConvertibleQuantityAttribute"/> to be parsed.</summary>
public sealed class ConvertibleQuantityAttributeParser : IConstructiveSyntacticAttributeParser<IRawConvertibleQuantity>, IConstructiveSemanticAttributeParser<IRawConvertibleQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ConvertibleQuantityAttributeParser"/>, parsing the arguments of a <see cref="ConvertibleQuantityAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ConvertibleQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawConvertibleQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        ConvertibleQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawConvertibleQuantity? TryParse(AttributeData attributeData)
    {
        ConvertibleQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawConvertibleQuantity? Create(ConvertibleQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawConvertibleQuantity(recorder.Quantities, recorder.ForwarsBehaviour, recorder.BackwardsBehaviour, CreateSyntax(recorder, parsingMode));
    }

    private static IConvertibleQuantitySyntax? CreateSyntax(ConvertibleQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ConvertibleQuantitySyntax(recorder.QuantitiesCollectionLocation, recorder.QuantitiesElementLocations, recorder.ForwarsBehaviourLocation, recorder.BackwardsBehaviourLocation);
    }

    private sealed class ConvertibleQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<ITypeSymbol?>? Quantities { get; private set; }
        public ConversionOperatorBehaviour? ForwarsBehaviour { get; private set; }
        public ConversionOperatorBehaviour? BackwardsBehaviour { get; private set; }

        public Location QuantitiesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> QuantitiesElementLocations { get; private set; } = Array.Empty<Location>();
        public Location ForwarsBehaviourLocation { get; private set; } = Location.None;
        public Location BackwardsBehaviourLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("ForwardsBehaviour", Adapters.For<ConversionOperatorBehaviour>(RecordForwardsBehaviour));
            yield return ("BackwardsBehaviour", Adapters.For<ConversionOperatorBehaviour>(RecordBackwardsBehaviour));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Quantities", Adapters.ForNullable<ITypeSymbol>(RecordQuantities));
        }

        private void RecordQuantities(IReadOnlyList<ITypeSymbol?>? quantities, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Quantities = quantities;
            QuantitiesCollectionLocation = collectionLocation;
            QuantitiesElementLocations = elementLocations;
        }

        private void RecordForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour, Location location)
        {
            ForwarsBehaviour = forwardsBehaviour;
            ForwarsBehaviourLocation = location;
        }

        private void RecordBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour, Location location)
        {
            BackwardsBehaviour = backwardsBehaviour;
            BackwardsBehaviourLocation = location;
        }
    }
}
