namespace SharpMeasures.Generators.Parsing.Units.UnitAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitAttribute{TScalar}"/> to be parsed.</summary>
public sealed class UnitAttributeParser : IConstructiveSyntacticAttributeParser<IRawUnit>, IConstructiveSemanticAttributeParser<IRawUnit>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="UnitAttributeParser"/>, parsing the arguments of a <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public UnitAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawUnit? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        UnitAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawUnit? TryParse(AttributeData attributeData)
    {
        UnitAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawUnit? Create(UnitAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Scalar is null)
        {
            return null;
        }

        return new RawUnit(recorder.Scalar, recorder.BiasTerm, CreateSyntax(recorder, parsingMode));
    }

    private IUnitSyntax? CreateSyntax(UnitAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new UnitSyntax(recorder.ScalarLocation, recorder.BiasTermLocation);
    }

    private sealed class UnitAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Scalar { get; private set; }
        public bool? BiasTerm { get; private set; }

        public Location ScalarLocation { get; private set; } = Location.None;
        public Location BiasTermLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TScalar", Adapters.For(RecordScalar));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("BiasTerm", Adapters.For<bool>(RecordBiasTerm));
        }

        private void RecordScalar(ITypeSymbol scalar, Location location)
        {
            Scalar = scalar;
            ScalarLocation = location;
        }

        private void RecordBiasTerm(bool biasTerm, Location location)
        {
            BiasTerm = biasTerm;
            BiasTermLocation = location;
        }
    }
}
