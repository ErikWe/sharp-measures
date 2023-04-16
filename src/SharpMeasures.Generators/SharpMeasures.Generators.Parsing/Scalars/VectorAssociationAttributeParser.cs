namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorAssociationAttribute{TVector}"/> to be parsed.</summary>
public sealed class VectorAssociationAttributeParser : IConstructiveSyntacticAttributeParser<IRawVectorAssociation>, IConstructiveSemanticAttributeParser<IRawVectorAssociation>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorAssociationAttributeParser"/>, parsing the arguments of a <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorAssociationAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawVectorAssociation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        VectorAssociationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawVectorAssociation? TryParse(AttributeData attributeData)
    {
        VectorAssociationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawVectorAssociation? Create(VectorAssociationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Vector is null)
        {
            return null;
        }

        return new RawVectorAssociation(recorder.Vector, CreateSyntax(recorder, parsingMode));
    }

    private static IVectorAssociationSyntax? CreateSyntax(VectorAssociationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new VectorAssociationSyntax(recorder.VectorLocation);
    }

    private sealed class VectorAssociationAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Vector { get; private set; }

        public Location VectorLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TVector", Adapters.For(RecordVector));
        }

        private void RecordVector(ITypeSymbol vector, Location location)
        {
            Vector = vector;
            VectorLocation = location;
        }
    }

    private sealed record class RawVectorAssociation : IRawVectorAssociation
    {
        private ITypeSymbol Vector { get; }

        private IVectorAssociationSyntax? Syntax { get; }

        public RawVectorAssociation(ITypeSymbol vector, IVectorAssociationSyntax? syntax)
        {
            Vector = vector;

            Syntax = syntax;
        }

        ITypeSymbol IRawVectorAssociation.Vector => Vector;

        IVectorAssociationSyntax? IRawVectorAssociation.Syntax => Syntax;
    }

    private sealed record class VectorAssociationSyntax : IVectorAssociationSyntax
    {
        private Location Vector { get; }

        public VectorAssociationSyntax(Location vector)
        {
            Vector = vector;
        }

        Location IVectorAssociationSyntax.Vector => Vector;
    }
}
