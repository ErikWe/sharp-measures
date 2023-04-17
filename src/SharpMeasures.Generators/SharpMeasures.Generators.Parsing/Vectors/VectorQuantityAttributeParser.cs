namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorQuantityAttribute{TUnit}"/> to be parsed.</summary>
public sealed class VectorQuantityAttributeParser : IConstructiveSemanticAttributeParser<IRawVectorQuantity>, IConstructiveSyntacticAttributeParser<IRawVectorQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorQuantityAttributeParser"/>, parsing the arguments of a <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawVectorQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        VectorQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawVectorQuantity? TryParse(AttributeData attributeData)
    {
        VectorQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawVectorQuantity? Create(VectorQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Unit is null)
        {
            return null;
        }

        return new RawVectorQuantity(recorder.Unit, recorder.Dimension, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static IVectorQuantitySyntax? CreateSyntax(VectorQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new VectorQuantitySyntax(recorder.AttributeNameLocation, recorder.UnitLocation, recorder.DimensionLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class VectorQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Unit { get; private set; }
        public int? Dimension { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

        public Location UnitLocation { get; private set; } = Location.None;
        public Location DimensionLocation { get; private set; } = Location.None;
        public Location ImplementSumLocation { get; private set; } = Location.None;
        public Location ImplementDifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TUnit", Adapters.For(RecordUnit));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Dimension", Adapters.For<int>(RecordDimension));
            yield return ("ImplementSum", Adapters.For<bool>(RecordImplementSum));
            yield return ("ImplementDifference", Adapters.For<bool>(RecordImplementDifference));
        }

        private void RecordUnit(ITypeSymbol unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }

        private void RecordDimension(int dimension, Location location)
        {
            Dimension = dimension;
            DimensionLocation = location;
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

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawVectorQuantity : IRawVectorQuantity
    {
        private ITypeSymbol Unit { get; }

        private int? Dimension { get; }

        private bool? ImplementSum { get; }
        private bool? ImplementDifference { get; }

        private IVectorQuantitySyntax? Syntax { get; }

        public RawVectorQuantity(ITypeSymbol unit, int? dimension, bool? implementSum, bool? implementDifference, IVectorQuantitySyntax? syntax)
        {
            Unit = unit;

            Dimension = dimension;

            ImplementSum = implementSum;
            ImplementDifference = implementDifference;

            Syntax = syntax;
        }

        ITypeSymbol IRawVectorQuantity.Unit => Unit;

        int? IRawVectorQuantity.Dimension => Dimension;

        bool? IRawVectorQuantity.ImplementSum => ImplementSum;
        bool? IRawVectorQuantity.ImplementDifference => ImplementDifference;

        IVectorQuantitySyntax? IRawVectorQuantity.Syntax => Syntax;
    }

    private sealed record class VectorQuantitySyntax : IVectorQuantitySyntax
    {
        private Location AttributeName { get; }

        private Location Unit { get; }
        private Location Dimension { get; }
        private Location ImplementSum { get; }
        private Location ImplementDifference { get; }

        public VectorQuantitySyntax(Location attributeName, Location unit, Location dimension, Location implementSum, Location implementDifference)
        {
            AttributeName = attributeName;

            Unit = unit;
            Dimension = dimension;
            ImplementSum = implementSum;
            ImplementDifference = implementDifference;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IVectorQuantitySyntax.Unit => Unit;
        Location IVectorQuantitySyntax.Dimension => Dimension;
        Location IVectorQuantitySyntax.ImplementSum => ImplementSum;
        Location IVectorQuantitySyntax.ImplementDifference => ImplementDifference;
    }
}
