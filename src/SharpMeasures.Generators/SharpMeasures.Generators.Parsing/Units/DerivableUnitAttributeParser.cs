namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="DerivableUnitAttribute"/> to be parsed.</summary>
public sealed class DerivableUnitAttributeParser : IConstructiveSyntacticAttributeParser<IRawDerivableUnit>, IConstructiveSemanticAttributeParser<IRawDerivableUnit>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DerivableUnitAttributeParser"/>, parsing the arguments of a <see cref="DerivableUnitAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DerivableUnitAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawDerivableUnit? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        DerivableUnitAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawDerivableUnit? TryParse(AttributeData attributeData)
    {
        DerivableUnitAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawDerivableUnit? Create(DerivableUnitAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawDerivableUnit(recorder.DerivationID, recorder.Expression, recorder.Signature, CreateSyntax(recorder, parsingMode));
    }

    private IDerivableUnitSyntax? CreateSyntax(DerivableUnitAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new DerivableUnitSyntax(recorder.DerivationIDLocation, recorder.ExpressionLocation, recorder.SignatureCollectionLocation, recorder.SignatureElementLocations);
    }

    private sealed class DerivableUnitAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? DerivationID { get; private set; }
        public string? Expression { get; private set; }
        public IReadOnlyList<ITypeSymbol?>? Signature { get; private set; }

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
    }

    private sealed record class RawDerivableUnit : IRawDerivableUnit
    {
        private string? DerivationID { get; }
        private string? Expression { get; }
        private IReadOnlyList<ITypeSymbol?>? Signature { get; }

        private IDerivableUnitSyntax? Syntax { get; }

        public RawDerivableUnit(string? derivationID, string? expression, IReadOnlyList<ITypeSymbol?>? signature, IDerivableUnitSyntax? syntax)
        {
            DerivationID = derivationID;
            Expression = expression;
            Signature = signature;

            Syntax = syntax;
        }

        string? IRawDerivableUnit.DerivationID => DerivationID;
        string? IRawDerivableUnit.Expression => Expression;
        IReadOnlyList<ITypeSymbol?>? IRawDerivableUnit.Signature => Signature;

        IDerivableUnitSyntax? IRawDerivableUnit.Syntax => Syntax;
    }

    private sealed record class DerivableUnitSyntax : IDerivableUnitSyntax
    {
        private Location DerivationID { get; }
        private Location Expression { get; }
        private Location SignatureCollection { get; }
        private IReadOnlyList<Location> SignatureElements { get; }

        public DerivableUnitSyntax(Location derivationID, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements)
        {
            DerivationID = derivationID;
            Expression = expression;
            SignatureCollection = signatureCollection;
            SignatureElements = signatureElements;
        }

        Location IDerivableUnitSyntax.DerivationID => DerivationID;
        Location IDerivableUnitSyntax.Expression => Expression;

        Location IDerivableUnitSyntax.SignatureCollection => SignatureCollection;
        IReadOnlyList<Location> IDerivableUnitSyntax.SignatureElements => SignatureElements;
    }
}
