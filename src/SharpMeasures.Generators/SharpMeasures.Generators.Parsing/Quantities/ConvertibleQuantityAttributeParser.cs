namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

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
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ConvertibleQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

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

        return new ConvertibleQuantitySyntax(recorder.AttributeNameLocation, recorder.QuantitiesCollectionLocation, recorder.QuantitiesElementLocations, recorder.ForwarsBehaviourLocation, recorder.BackwardsBehaviourLocation);
    }

    private sealed class ConvertibleQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<ITypeSymbol?>? Quantities { get; private set; }
        public ConversionOperatorBehaviour? ForwarsBehaviour { get; private set; }
        public ConversionOperatorBehaviour? BackwardsBehaviour { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

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

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawConvertibleQuantity : IRawConvertibleQuantity
    {
        private IReadOnlyList<ITypeSymbol?>? Quantities { get; }

        private ConversionOperatorBehaviour? ForwardsBehaviour { get; }
        private ConversionOperatorBehaviour? BackwardsBehaviour { get; }

        private IConvertibleQuantitySyntax? Syntax { get; }

        public RawConvertibleQuantity(IReadOnlyList<ITypeSymbol?>? quantities, ConversionOperatorBehaviour? forwardsBehaviour, ConversionOperatorBehaviour? backwardsBehaviour, IConvertibleQuantitySyntax? syntax)
        {
            Quantities = quantities;

            ForwardsBehaviour = forwardsBehaviour;
            BackwardsBehaviour = backwardsBehaviour;

            Syntax = syntax;
        }

        IReadOnlyList<ITypeSymbol?>? IRawConvertibleQuantity.Quantities => Quantities;

        ConversionOperatorBehaviour? IRawConvertibleQuantity.ForwardsBehaviour => ForwardsBehaviour;
        ConversionOperatorBehaviour? IRawConvertibleQuantity.BackwardsBehaviour => BackwardsBehaviour;

        IConvertibleQuantitySyntax? IRawConvertibleQuantity.Syntax => Syntax;
    }

    private sealed record class ConvertibleQuantitySyntax : IConvertibleQuantitySyntax
    {
        private Location AttributeName { get; }

        private Location QuantitiesCollection { get; }
        private IReadOnlyList<Location> QuantitiesElements { get; }

        private Location ForwardsBehaviour { get; }
        private Location BackwardsBehaviour { get; }

        public ConvertibleQuantitySyntax(Location attributeName, Location quantitiesCollection, IReadOnlyList<Location> quantitiesElements, Location forwardsBehaviour, Location backwardsBehaviour)
        {
            AttributeName = attributeName;

            QuantitiesCollection = quantitiesCollection;
            QuantitiesElements = quantitiesElements;

            ForwardsBehaviour = forwardsBehaviour;
            BackwardsBehaviour = backwardsBehaviour;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IConvertibleQuantitySyntax.QuantitiesCollection => QuantitiesCollection;
        IReadOnlyList<Location> IConvertibleQuantitySyntax.QuantitiesElements => QuantitiesElements;

        Location IConvertibleQuantitySyntax.ForwardsBehaviour => ForwardsBehaviour;
        Location IConvertibleQuantitySyntax.BackwardsBehaviour => BackwardsBehaviour;
    }
}
