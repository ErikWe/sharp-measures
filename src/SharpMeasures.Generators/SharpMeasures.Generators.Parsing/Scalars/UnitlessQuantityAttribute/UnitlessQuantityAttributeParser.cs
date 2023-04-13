namespace SharpMeasures.Generators.Parsing.Scalars.UnitlessQuantityAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitlessQuantityAttribute"/> to be parsed.</summary>
public sealed class UnitlessQuantityAttributeParser : IConstructiveSyntacticAttributeParser<IRawUnitlessQuantity>, IConstructiveSemanticAttributeParser<IRawUnitlessQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="UnitlessQuantityAttributeParser"/>, parsing the arguments of a <see cref="UnitlessQuantityAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public UnitlessQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawUnitlessQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        UnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawUnitlessQuantity? TryParse(AttributeData attributeData)
    {
        UnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawUnitlessQuantity? Create(UnitlessQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawUnitlessQuantity(recorder.AllowNegative, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static IUnitlessQuantitySyntax? CreateSyntax(UnitlessQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new UnitlessQuantitySyntax(recorder.AllowNegativeLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class UnitlessQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public bool? AllowNegative { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location AllowNegativeLocation { get; private set; } = Location.None;
        public Location ImplementSumLocation { get; private set; } = Location.None;
        public Location ImplementDifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("AllowNegative", Adapters.For<bool>(RecordAllowNegative));
            yield return ("ImplementSum", Adapters.For<bool>(RecordImplementSum));
            yield return ("ImplementDifference", Adapters.For<bool>(RecordImplementDifference));
        }

        private void RecordAllowNegative(bool allowNegative, Location location)
        {
            AllowNegative = allowNegative;
            AllowNegativeLocation = location;
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
