namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityDifferenceAttribute{TDifference}"/> to be parsed.</summary>
public sealed class QuantityDifferenceAttributeParser : IConstructiveSyntacticAttributeParser<IRawQuantityDifference>, IConstructiveSemanticAttributeParser<IRawQuantityDifference>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityDifferenceAttributeParser"/>, parsing the arguments of a <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityDifferenceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawQuantityDifference? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        QuantityDifferenceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawQuantityDifference? TryParse(AttributeData attributeData)
    {
        QuantityDifferenceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawQuantityDifference? Create(QuantityDifferenceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Difference is null)
        {
            return null;
        }

        return new RawQuantityDifference(recorder.Difference, CreateSyntax(recorder, parsingMode));
    }

    private static IQuantityDifferenceSyntax? CreateSyntax(QuantityDifferenceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new QuantityDifferenceSyntax(recorder.AttributeNameLocation, recorder.DifferenceLocation);
    }

    private sealed class QuantityDifferenceAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Difference { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;
        public Location DifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TDifference", Adapters.For(RecordDifference));
        }

        private void RecordDifference(ITypeSymbol difference, Location location)
        {
            Difference = difference;
            DifferenceLocation = location;
        }

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawQuantityDifference : IRawQuantityDifference
    {
        private ITypeSymbol Difference { get; }

        private IQuantityDifferenceSyntax? Syntax { get; }

        public RawQuantityDifference(ITypeSymbol difference, IQuantityDifferenceSyntax? syntax)
        {
            Difference = difference;

            Syntax = syntax;
        }

        ITypeSymbol IRawQuantityDifference.Difference => Difference;

        IQuantityDifferenceSyntax? IRawQuantityDifference.Syntax => Syntax;
    }

    private sealed record class QuantityDifferenceSyntax : IQuantityDifferenceSyntax
    {
        private Location AttributeName { get; }

        private Location Difference { get; }

        public QuantityDifferenceSyntax(Location attributeName, Location difference)
        {
            AttributeName = attributeName;

            Difference = difference;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IQuantityDifferenceSyntax.Difference => Difference;
    }
}
