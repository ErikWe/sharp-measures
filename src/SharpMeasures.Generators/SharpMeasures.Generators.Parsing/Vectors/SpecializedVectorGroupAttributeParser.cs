namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedVectorGroupAttributeParser : IConstructiveSyntacticAttributeParser<IRawSpecializedVectorGroup>, IConstructiveSemanticAttributeParser<IRawSpecializedVectorGroup>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorGroupAttributeParser"/>, parsing the arguments of a <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedVectorGroupAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawSpecializedVectorGroup? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        SpecializedVectorGroupAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawSpecializedVectorGroup? TryParse(AttributeData attributeData)
    {
        SpecializedVectorGroupAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawSpecializedVectorGroup? Create(SpecializedVectorGroupAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new RawSpecializedVectorGroup(recorder.Original, recorder.InheritOperations, recorder.InheritConversions, recorder.ForwardsCastOperatorBehaviour, recorder.BackwardsCastOperatorBehaviour,
            recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static ISpecializedVectorGroupSyntax? CreateSyntax(SpecializedVectorGroupAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new SpecializedVectorGroupSyntax(recorder.OriginalLocation, recorder.InheritOperationsLocation, recorder.InheritConversionsLocation, recorder.ForwardsCastOperatorBehaviourLocation,
            recorder.BackwardsCastOperatorBehaviourLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class SpecializedVectorGroupAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Original { get; private set; }
        public bool? InheritOperations { get; private set; }
        public bool? InheritConversions { get; private set; }
        public ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; private set; }
        public ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location OriginalLocation { get; private set; } = Location.None;
        public Location InheritOperationsLocation { get; private set; } = Location.None;
        public Location InheritConversionsLocation { get; private set; } = Location.None;
        public Location ForwardsCastOperatorBehaviourLocation { get; private set; } = Location.None;
        public Location BackwardsCastOperatorBehaviourLocation { get; private set; } = Location.None;
        public Location ImplementSumLocation { get; private set; } = Location.None;
        public Location ImplementDifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TOriginal", Adapters.For(RecordOriginal));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("InheritOperations", Adapters.For<bool>(RecordInheritOperations));
            yield return ("InheritConversions", Adapters.For<bool>(RecordInheritConversions));
            yield return ("ForwardsCastOperatorBehaviour", Adapters.For<ConversionOperatorBehaviour>(RecordForwardsCastOperatorBehaviour));
            yield return ("BackwardsCastOperatorBehaviour", Adapters.For<ConversionOperatorBehaviour>(RecordBackwardsCastOperatorBehaviour));
            yield return ("ImplementSum", Adapters.For<bool>(RecordImplementSum));
            yield return ("ImplementDifference", Adapters.For<bool>(RecordImplementDifference));
        }

        private void RecordOriginal(ITypeSymbol original, Location location)
        {
            Original = original;
            OriginalLocation = location;
        }

        private void RecordInheritOperations(bool inheritOperations, Location location)
        {
            InheritOperations = inheritOperations;
            InheritOperationsLocation = location;
        }

        private void RecordInheritConversions(bool inheritConversions, Location location)
        {
            InheritConversions = inheritConversions;
            InheritConversionsLocation = location;
        }

        private void RecordForwardsCastOperatorBehaviour(ConversionOperatorBehaviour forwardsCastOperatorBehaviour, Location location)
        {
            ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
            ForwardsCastOperatorBehaviourLocation = location;
        }

        private void RecordBackwardsCastOperatorBehaviour(ConversionOperatorBehaviour backwardsCastOperatorBehaviour, Location location)
        {
            BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;
            BackwardsCastOperatorBehaviourLocation = location;
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

    private sealed record class RawSpecializedVectorGroup : IRawSpecializedVectorGroup
    {
        private ITypeSymbol Original { get; }

        private bool? InheritOperations { get; }
        private bool? InheritConversions { get; }

        private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
        private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

        private bool? ImplementSum { get; }
        private bool? ImplementDifference { get; }

        private ISpecializedVectorGroupSyntax? Syntax { get; }

        public RawSpecializedVectorGroup(ITypeSymbol original, bool? inheritOperations, bool? inheritConversions, ConversionOperatorBehaviour? forwardsCastOperatorBehaviour,
            ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedVectorGroupSyntax? syntax)
        {
            Original = original;

            InheritOperations = inheritOperations;
            InheritConversions = inheritConversions;

            ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
            BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

            ImplementSum = implementSum;
            ImplementDifference = implementDifference;

            Syntax = syntax;
        }

        ITypeSymbol IRawSpecializedVectorGroup.Original => Original;

        bool? IRawSpecializedVectorGroup.InheritOperations => InheritOperations;
        bool? IRawSpecializedVectorGroup.InheritConversions => InheritConversions;

        ConversionOperatorBehaviour? IRawSpecializedVectorGroup.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
        ConversionOperatorBehaviour? IRawSpecializedVectorGroup.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

        bool? IRawSpecializedVectorGroup.ImplementSum => ImplementSum;
        bool? IRawSpecializedVectorGroup.ImplementDifference => ImplementDifference;

        ISpecializedVectorGroupSyntax? IRawSpecializedVectorGroup.Syntax => Syntax;
    }

    private sealed record class SpecializedVectorGroupSyntax : ISpecializedVectorGroupSyntax
    {
        private Location Original { get; }
        private Location InheritOperations { get; }
        private Location InheritConversions { get; }
        private Location ForwardsCastOperatorBehaviour { get; }
        private Location BackwardsCastOperatorBehaviour { get; }
        private Location ImplementSum { get; }
        private Location ImplementDifference { get; }

        public SpecializedVectorGroupSyntax(Location original, Location inheritOperations, Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour,
            Location implementSum, Location implementDifference)
        {
            Original = original;

            InheritOperations = inheritOperations;
            InheritConversions = inheritConversions;

            ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
            BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

            ImplementSum = implementSum;
            ImplementDifference = implementDifference;
        }

        Location ISpecializedVectorGroupSyntax.Original => Original;
        Location ISpecializedVectorGroupSyntax.InheritOperations => InheritOperations;
        Location ISpecializedVectorGroupSyntax.InheritConversions => InheritConversions;
        Location ISpecializedVectorGroupSyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
        Location ISpecializedVectorGroupSyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
        Location ISpecializedVectorGroupSyntax.ImplementSum => ImplementSum;
        Location ISpecializedVectorGroupSyntax.ImplementDifference => ImplementDifference;
    }
}
