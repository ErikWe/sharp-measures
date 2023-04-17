namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScalarAssociationAttribute{TScalar}"/> to be parsed.</summary>
public sealed class ScalarAssociationAttributeParser : IConstructiveSemanticAttributeParser<IRawScalarAssociation>, IConstructiveSyntacticAttributeParser<IRawScalarAssociation>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScalarAssociationAttributeParser"/>, parsing the arguments of a <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScalarAssociationAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawScalarAssociation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ScalarAssociationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawScalarAssociation? TryParse(AttributeData attributeData)
    {
        ScalarAssociationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawScalarAssociation? Create(ScalarAssociationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Scalar is null)
        {
            return null;
        }

        return new RawScalarAssociation(recorder.Scalar, recorder.AsComponents, recorder.AsMagnitude, CreateSyntax(recorder, parsingMode));
    }

    private static IScalarAssociationSyntax? CreateSyntax(ScalarAssociationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ScalarAssociationSyntax(recorder.AttributeNameLocation, recorder.ScalarLocation, recorder.AsComponentsLocation, recorder.AsMagnitudeLocation);
    }

    private sealed class ScalarAssociationAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Scalar { get; private set; }

        public bool? AsComponents { get; private set; }
        public bool? AsMagnitude { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

        public Location ScalarLocation { get; private set; } = Location.None;

        public Location AsComponentsLocation { get; private set; } = Location.None;
        public Location AsMagnitudeLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TScalar", Adapters.For(RecordScalar));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("AsComponents", Adapters.For<bool>(RecordAsComponents));
            yield return ("AsMagnitude", Adapters.For<bool>(RecordAsMagnitude));
        }

        private void RecordScalar(ITypeSymbol scalar, Location location)
        {
            Scalar = scalar;
            ScalarLocation = location;
        }

        private void RecordAsComponents(bool asComponents, Location location)
        {
            AsComponents = asComponents;
            AsComponentsLocation = location;
        }

        private void RecordAsMagnitude(bool asMagnitude, Location location)
        {
            AsMagnitude = asMagnitude;
            AsMagnitudeLocation = location;
        }

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawScalarAssociation : IRawScalarAssociation
    {
        private ITypeSymbol Scalar { get; }

        private bool? AsComponents { get; }
        private bool? AsMagnitude { get; }

        private IScalarAssociationSyntax? Syntax { get; }

        public RawScalarAssociation(ITypeSymbol scalar, bool? asComponents, bool? asMagnitude, IScalarAssociationSyntax? syntax)
        {
            Scalar = scalar;

            AsComponents = asComponents;
            AsMagnitude = asMagnitude;

            Syntax = syntax;
        }

        ITypeSymbol IRawScalarAssociation.Scalar => Scalar;

        bool? IRawScalarAssociation.AsComponents => AsComponents;
        bool? IRawScalarAssociation.AsMagnitude => AsMagnitude;

        IScalarAssociationSyntax? IRawScalarAssociation.Syntax => Syntax;
    }

    private sealed record class ScalarAssociationSyntax : IScalarAssociationSyntax
    {
        private Location AttributeName { get; }

        private Location Scalar { get; }

        private Location AsComponents { get; }
        private Location AsMagnitude { get; }

        public ScalarAssociationSyntax(Location attributeName, Location scalar, Location asComponents, Location asMagnitude)
        {
            AttributeName = attributeName;

            Scalar = scalar;

            AsComponents = asComponents;
            AsMagnitude = asMagnitude;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IScalarAssociationSyntax.Scalar => Scalar;

        Location IScalarAssociationSyntax.AsComponents => AsComponents;
        Location IScalarAssociationSyntax.AsMagnitude => AsMagnitude;
    }
}
