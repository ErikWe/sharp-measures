namespace SharpMeasures.Generators.Parsing.Quantities.QuantitySumAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantitySumAttribute{TSum}"/> to be parsed.</summary>
public sealed class QuantitySumAttributeParser : IConstructiveSyntacticAttributeParser<IRawQuantitySum>, IConstructiveSemanticAttributeParser<IRawQuantitySum>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantitySumAttributeParser"/>, parsing the arguments of a <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantitySumAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawQuantitySum? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        QuantitySumAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawQuantitySum? TryParse(AttributeData attributeData)
    {
        QuantitySumAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawQuantitySum? Create(QuantitySumAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Sum is null)
        {
            return null;
        }

        return new RawQuantitySum(recorder.Sum, CreateSyntax(recorder, parsingMode));
    }

    private static IQuantitySumSyntax? CreateSyntax(QuantitySumAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new QuantitySumSyntax(recorder.SumLocation);
    }

    private sealed class QuantitySumAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Sum { get; private set; }

        public Location SumLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TSum", Adapters.For(RecordSum));
        }

        private void RecordSum(ITypeSymbol sum, Location location)
        {
            Sum = sum;
            SumLocation = location;
        }
    }
}
