namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorComponentNamesAttribute"/> to be parsed.</summary>
public sealed class VectorComponentNamesAttributeParser : IConstructiveSemanticAttributeParser<IRawVectorComponentNames>, IConstructiveSyntacticAttributeParser<IRawVectorComponentNames>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorComponentNamesAttributeParser"/>, parsing the arguments of a <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorComponentNamesAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawVectorComponentNames? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        VectorComponentNamesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawVectorComponentNames? TryParse(AttributeData attributeData)
    {
        VectorComponentNamesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawVectorComponentNames? Create(VectorComponentNamesAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawVectorComponentNames(recorder.Names, CreateSyntax(recorder, parsingMode));
    }

    private static IVectorComponentNamesSyntax? CreateSyntax(VectorComponentNamesAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new VectorComponentNamesSyntax(recorder.NamesCollectionLocation, recorder.NamesElementLocations);
    }

    private sealed class VectorComponentNamesAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<string?>? Names { get; private set; }

        public Location NamesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> NamesElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Names", Adapters.ForNullable<string>(RecordNames));
        }

        private void RecordNames(IReadOnlyList<string?>? names, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Names = names;

            NamesCollectionLocation = collectionLocation;
            NamesElementLocations = elementLocations;
        }
    }

    private sealed record class RawVectorComponentNames : IRawVectorComponentNames
    {
        private IReadOnlyList<string?>? Names { get; }

        private IVectorComponentNamesSyntax? Syntax { get; }

        public RawVectorComponentNames(IReadOnlyList<string?>? names, IVectorComponentNamesSyntax? syntax)
        {
            Names = names;

            Syntax = syntax;
        }

        IReadOnlyList<string?>? IRawVectorComponentNames.Names => Names;

        IVectorComponentNamesSyntax? IRawVectorComponentNames.Syntax => Syntax;
    }

    private sealed record class VectorComponentNamesSyntax : IVectorComponentNamesSyntax
    {
        private Location NamesCollection { get; }
        private IReadOnlyList<Location> NamesElements { get; }

        public VectorComponentNamesSyntax(Location namesCollection, IReadOnlyList<Location> namesElements)
        {
            NamesCollection = namesCollection;
            NamesElements = namesElements;
        }

        Location IVectorComponentNamesSyntax.NamesCollection => NamesCollection;
        IReadOnlyList<Location> IVectorComponentNamesSyntax.NamesElements => NamesElements;
    }
}
