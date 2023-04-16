namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

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

    private sealed record class RawVectorGroup : IRawVectorGroup
    {
        private ITypeSymbol Unit { get; }

        private bool? ImplementSum { get; }
        private bool? ImplementDifference { get; }

        private IVectorGroupSyntax? Syntax { get; }

        public RawVectorGroup(ITypeSymbol unit, bool? implementSum, bool? implementDifference, IVectorGroupSyntax? syntax)
        {
            Unit = unit;

            ImplementSum = implementSum;
            ImplementDifference = implementDifference;

            Syntax = syntax;
        }

        ITypeSymbol IRawVectorGroup.Unit => Unit;

        bool? IRawVectorGroup.ImplementSum => ImplementSum;
        bool? IRawVectorGroup.ImplementDifference => ImplementDifference;

        IVectorGroupSyntax? IRawVectorGroup.Syntax => Syntax;
    }

    private sealed record class VectorGroupSyntax : IVectorGroupSyntax
    {
        private Location Unit { get; }
        private Location ImplementSum { get; }
        private Location ImplementDifference { get; }

        /// <summary>Instantiates a <see cref="VectorGroupSyntax"/>, representing syntactical information about a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
        /// <param name="unit"><inheritdoc cref="IVectorGroupSyntax.Unit" path="/summary"/></param>
        /// <param name="implementSum"><inheritdoc cref="IVectorGroupSyntax.ImplementSum" path="/summary"/></param>
        /// <param name="implementDifference"><inheritdoc cref="IVectorGroupSyntax.ImplementDifference" path="/summary"/></param>
        public VectorGroupSyntax(Location unit, Location implementSum, Location implementDifference)
        {
            Unit = unit;
            ImplementSum = implementSum;
            ImplementDifference = implementDifference;
        }

        Location IVectorGroupSyntax.Unit => Unit;
        Location IVectorGroupSyntax.ImplementSum => ImplementSum;
        Location IVectorGroupSyntax.ImplementDifference => ImplementDifference;
    }
}
