namespace SharpMeasures.Generators.Parsing.Vectors.VectorGroupAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorGroupAttribute{TUnit}"/> to be parsed.</summary>
public sealed class VectorGroupAttributeParser : IConstructiveSemanticAttributeParser<IRawVectorGroup>, IConstructiveSyntacticAttributeParser<IRawVectorGroup>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorGroupAttributeParser"/>, parsing the arguments of a <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorGroupAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawVectorGroup? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        VectorGroupAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawVectorGroup? TryParse(AttributeData attributeData)
    {
        VectorGroupAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawVectorGroup? Create(VectorGroupAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Unit is null)
        {
            return null;
        }

        return new RawVectorGroup(recorder.Unit, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static IVectorGroupSyntax? CreateSyntax(VectorGroupAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new VectorGroupSyntax(recorder.UnitLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class VectorGroupAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Unit { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location UnitLocation { get; private set; } = Location.None;
        public Location ImplementSumLocation { get; private set; } = Location.None;
        public Location ImplementDifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TUnit", Adapters.For(RecordUnit));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("ImplementSum", Adapters.For<bool>(RecordImplementSum));
            yield return ("ImplementDifference", Adapters.For<bool>(RecordImplementDifference));
        }

        private void RecordUnit(ITypeSymbol unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }

        private void RecordImplementSum(bool implementSum, Location location)
        {
            ImplementSum = implementSum;
            ImplementSumLocation = location;
        }

        private void RecordImplementDifference(bool implementDifference, Location location)
        {
            ImplementDifference = implementDifference;
            ImplementDifferenceLocation = location;
        }
    }
}
