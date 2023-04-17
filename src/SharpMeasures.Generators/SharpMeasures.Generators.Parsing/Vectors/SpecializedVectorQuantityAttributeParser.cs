namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedVectorQuantityAttributeParser : IConstructiveSyntacticAttributeParser<IRawSpecializedVectorQuantity>, IConstructiveSemanticAttributeParser<IRawSpecializedVectorQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorQuantityAttributeParser"/>, parsing the arguments of a <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedVectorQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawSpecializedVectorQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SpecializedVectorQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawSpecializedVectorQuantity? TryParse(AttributeData attributeData)
    {
        SpecializedVectorQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawSpecializedVectorQuantity? Create(SpecializedVectorQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new RawSpecializedVectorQuantity(recorder.Original, recorder.InheritOperations, recorder.InheritProcesses, recorder.InheritProperties, recorder.InheritConstants,
            recorder.InheritConversions, recorder.ForwardsCastOperatorBehaviour, recorder.BackwardsCastOperatorBehaviour, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static ISpecializedVectorQuantitySyntax? CreateSyntax(SpecializedVectorQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new SpecializedVectorQuantitySyntax(recorder.AttributeNameLocation, recorder.OriginalLocation, recorder.InheritOperationsLocation, recorder.InheritProcessesLocation, recorder.InheritPropertiesLocation, recorder.InheritConstantsLocation,
            recorder.InheritConversionsLocation, recorder.ForwardsCastOperatorBehaviourLocation, recorder.BackwardsCastOperatorBehaviourLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class SpecializedVectorQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Original { get; private set; }
        public bool? InheritOperations { get; private set; }
        public bool? InheritProcesses { get; private set; }
        public bool? InheritProperties { get; private set; }
        public bool? InheritConstants { get; private set; }
        public bool? InheritConversions { get; private set; }
        public ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; private set; }
        public ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

        public Location OriginalLocation { get; private set; } = Location.None;
        public Location InheritOperationsLocation { get; private set; } = Location.None;
        public Location InheritProcessesLocation { get; private set; } = Location.None;
        public Location InheritPropertiesLocation { get; private set; } = Location.None;
        public Location InheritConstantsLocation { get; private set; } = Location.None;
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
            yield return ("InheritProcesses", Adapters.For<bool>(RecordInheritProcesses));
            yield return ("InheritProperties", Adapters.For<bool>(RecordInheritProperties));
            yield return ("InheritConstants", Adapters.For<bool>(RecordInheritConstants));
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

        private void RecordInheritProcesses(bool inheritProcesses, Location location)
        {
            InheritProcesses = inheritProcesses;
            InheritProcessesLocation = location;
        }

        private void RecordInheritProperties(bool inheritProperties, Location location)
        {
            InheritProperties = inheritProperties;
            InheritPropertiesLocation = location;
        }

        private void RecordInheritConstants(bool inheritConstants, Location location)
        {
            InheritConstants = inheritConstants;
            InheritConstantsLocation = location;
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

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawSpecializedVectorQuantity : IRawSpecializedVectorQuantity
    {
        private ITypeSymbol Original { get; }

        private bool? InheritOperations { get; }
        private bool? InheritProcesses { get; }
        private bool? InheritProperties { get; }
        private bool? InheritConstants { get; }
        private bool? InheritConversions { get; }

        private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
        private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

        private bool? ImplementSum { get; }
        private bool? ImplementDifference { get; }

        private ISpecializedVectorQuantitySyntax? Syntax { get; }

        public RawSpecializedVectorQuantity(ITypeSymbol original, bool? inheritOperations, bool? inheritProcesses, bool? inheritProperties, bool? inheritConstants, bool? inheritConversions,
            ConversionOperatorBehaviour? forwardsCastOperatorBehaviour, ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedVectorQuantitySyntax? syntax)
        {
            Original = original;

            InheritOperations = inheritOperations;
            InheritProcesses = inheritProcesses;
            InheritProperties = inheritProperties;
            InheritConstants = inheritConstants;
            InheritConversions = inheritConversions;

            ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
            BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

            ImplementSum = implementSum;
            ImplementDifference = implementDifference;

            Syntax = syntax;
        }

        ITypeSymbol IRawSpecializedVectorQuantity.Original => Original;

        bool? IRawSpecializedVectorQuantity.InheritOperations => InheritOperations;
        bool? IRawSpecializedVectorQuantity.InheritProcesses => InheritProcesses;
        bool? IRawSpecializedVectorQuantity.InheritProperties => InheritProperties;
        bool? IRawSpecializedVectorQuantity.InheritConstants => InheritConstants;
        bool? IRawSpecializedVectorQuantity.InheritConversions => InheritConversions;

        ConversionOperatorBehaviour? IRawSpecializedVectorQuantity.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
        ConversionOperatorBehaviour? IRawSpecializedVectorQuantity.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

        bool? IRawSpecializedVectorQuantity.ImplementSum => ImplementSum;
        bool? IRawSpecializedVectorQuantity.ImplementDifference => ImplementDifference;

        ISpecializedVectorQuantitySyntax? IRawSpecializedVectorQuantity.Syntax => Syntax;
    }

    private sealed record class SpecializedVectorQuantitySyntax : ISpecializedVectorQuantitySyntax
    {
        private Location AttributeName { get; }

        private Location Original { get; }
        private Location InheritOperations { get; }
        private Location InheritProcesses { get; }
        private Location InheritProperties { get; }
        private Location InheritConstants { get; }
        private Location InheritConversions { get; }
        private Location ForwardsCastOperatorBehaviour { get; }
        private Location BackwardsCastOperatorBehaviour { get; }
        private Location ImplementSum { get; }
        private Location ImplementDifference { get; }

        public SpecializedVectorQuantitySyntax(Location attributeName, Location original, Location inheritOperations, Location inheritProcesses, Location inheritProperties, Location inheritConstants,
            Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour, Location implementSum, Location implementDifference)
        {
            AttributeName = attributeName;

            Original = original;

            InheritOperations = inheritOperations;
            InheritProcesses = inheritProcesses;
            InheritProperties = inheritProperties;
            InheritConstants = inheritConstants;
            InheritConversions = inheritConversions;

            ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
            BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

            ImplementSum = implementSum;
            ImplementDifference = implementDifference;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location ISpecializedVectorQuantitySyntax.Original => Original;
        Location ISpecializedVectorQuantitySyntax.InheritOperations => InheritOperations;
        Location ISpecializedVectorQuantitySyntax.InheritProcesses => InheritProcesses;
        Location ISpecializedVectorQuantitySyntax.InheritProperties => InheritProperties;
        Location ISpecializedVectorQuantitySyntax.InheritConstants => InheritConstants;
        Location ISpecializedVectorQuantitySyntax.InheritConversions => InheritConversions;
        Location ISpecializedVectorQuantitySyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
        Location ISpecializedVectorQuantitySyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
        Location ISpecializedVectorQuantitySyntax.ImplementSum => ImplementSum;
        Location ISpecializedVectorQuantitySyntax.ImplementDifference => ImplementDifference;
    }
}
