namespace SharpMeasures.Generators.Attributes.Parsing.Extraction;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public abstract class AExtractor<TParameters>
{
    public ReadOnlyCollection<TParameters> ValidDefinitions => new(ValidDefinitionsList);
    public IEnumerable<Diagnostic> Diagnostics { get; protected set; } = Enumerable.Empty<Diagnostic>();

    protected IList<TParameters> ValidDefinitionsList { get; } = new List<TParameters>();
}

public abstract class AExtractor<TParameters, TAttribute> : AExtractor<TParameters>
{
    private AArgumentParser<TParameters> Parser { get; }
    private IValidator<TParameters> Validator { get; }

    protected AExtractor(AArgumentParser<TParameters> parser, IValidator<TParameters> validator)
    {
        if (parser is null)
        {
            throw new ArgumentNullException(nameof(parser));
        }

        if (validator is null)
        {
            throw new ArgumentNullException(nameof(validator));
        }

        Parser = parser;
        Validator = validator;
    }

    protected void Extract(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        TParameters parameters = Parser.Parse(attributeData);

        if (Validator.Check(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalid)
        {
            Diagnostics = Diagnostics.Concat(invalid.Diagnostics);
        }
        else
        {
            ValidDefinitionsList.Add(parameters);
        }
    }

    protected void Extract(IEnumerable<AttributeData> attributeDataIterator)
    {
        if (attributeDataIterator is null)
        {
            throw new ArgumentNullException(nameof(attributeDataIterator));
        }

        foreach (AttributeData attributeData in attributeDataIterator)
        {
            Extract(attributeData);
        }
    }

    protected void ExtractAllFromSymbol(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        Extract(typeSymbol.GetAttributesOfType<TAttribute>());
    }

    protected void ExtractFirstFromSymbol(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            throw new ArgumentNullException(nameof(typeSymbol));
        }

        if (typeSymbol.GetAttributeOfType<TAttribute>() is AttributeData attributeData)
        {
            Extract(attributeData);
        }
    }
}
