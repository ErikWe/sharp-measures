namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitDerivationAttribute"/> to be parsed.</summary>
public sealed class UnitDerivationAttributeParser : IConstructiveSyntacticAttributeParser<IRawUnitDerivation>, IConstructiveSemanticAttributeParser<IRawUnitDerivation>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="UnitDerivationAttributeParser"/>, parsing the arguments of a <see cref="UnitDerivationAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public UnitDerivationAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawUnitDerivation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        UnitDerivationAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawUnitDerivation? TryParse(AttributeData attributeData)
    {
        UnitDerivationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawUnitDerivation? Create(UnitDerivationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawUnitDerivation(recorder.DerivationID, recorder.Expression, recorder.Signature, CreateSyntax(recorder, parsingMode));
    }

    private IUnitDerivationSyntax? CreateSyntax(UnitDerivationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new UnitDerivationSyntax(recorder.AttributeNameLocation, recorder.DerivationIDLocation, recorder.ExpressionLocation, recorder.SignatureCollectionLocation, recorder.SignatureElementLocations);
    }

    private sealed class UnitDerivationAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? DerivationID { get; private set; }
        public string? Expression { get; private set; }
        public IReadOnlyList<ITypeSymbol?>? Signature { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

        public Location DerivationIDLocation { get; private set; } = Location.None;
        public Location ExpressionLocation { get; private set; } = Location.None;
        public Location SignatureCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> SignatureElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("DerivationID", Adapters.ForNullable<string>(RecordDerivationID));
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Signature", Adapters.ForNullable<ITypeSymbol>(RecordSignature));
        }

        private void RecordDerivationID(string? derivationID, Location location)
        {
            if (derivationID is not null)
            {
                DerivationID = derivationID;
            }

            DerivationIDLocation = location;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Expression = expression;
            ExpressionLocation = location;
        }

        private void RecordSignature(IReadOnlyList<ITypeSymbol?>? signature, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Signature = signature;
            SignatureCollectionLocation = collectionLocation;
            SignatureElementLocations = elementLocations;
        }

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawUnitDerivation : IRawUnitDerivation
    {
        private string? DerivationID { get; }
        private string? Expression { get; }
        private IReadOnlyList<ITypeSymbol?>? Signature { get; }

        private IUnitDerivationSyntax? Syntax { get; }

        public RawUnitDerivation(string? derivationID, string? expression, IReadOnlyList<ITypeSymbol?>? signature, IUnitDerivationSyntax? syntax)
        {
            DerivationID = derivationID;
            Expression = expression;
            Signature = signature;

            Syntax = syntax;
        }

        string? IRawUnitDerivation.DerivationID => DerivationID;
        string? IRawUnitDerivation.Expression => Expression;
        IReadOnlyList<ITypeSymbol?>? IRawUnitDerivation.Signature => Signature;

        IUnitDerivationSyntax? IRawUnitDerivation.Syntax => Syntax;
    }

    private sealed record class UnitDerivationSyntax : IUnitDerivationSyntax
    {
        private Location AttributeName { get; }

        private Location DerivationID { get; }
        private Location Expression { get; }
        private Location SignatureCollection { get; }
        private IReadOnlyList<Location> SignatureElements { get; }

        public UnitDerivationSyntax(Location attributeName, Location derivationID, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements)
        {
            AttributeName = attributeName;

            DerivationID = derivationID;
            Expression = expression;
            SignatureCollection = signatureCollection;
            SignatureElements = signatureElements;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IUnitDerivationSyntax.DerivationID => DerivationID;
        Location IUnitDerivationSyntax.Expression => Expression;

        Location IUnitDerivationSyntax.SignatureCollection => SignatureCollection;
        IReadOnlyList<Location> IUnitDerivationSyntax.SignatureElements => SignatureElements;
    }
}
