namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedScalarQuantityAttributeParser : IConstructiveSyntacticAttributeParser<IRawSpecializedScalarQuantity>, IConstructiveSemanticAttributeParser<IRawSpecializedScalarQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedScalarQuantityAttributeParser"/>, parsing the arguments of a <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedScalarQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawSpecializedScalarQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SpecializedScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawSpecializedScalarQuantity? TryParse(AttributeData attributeData)
    {
        SpecializedScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawSpecializedScalarQuantity? Create(SpecializedScalarQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new RawSpecializedScalarQuantity(recorder.Original, recorder.AllowNegative, recorder.InheritOperations, recorder.InheritProcesses, recorder.InheritProperties, recorder.InheritConstants,
            recorder.InheritConversions, recorder.ForwardsCastOperatorBehaviour, recorder.BackwardsCastOperatorBehaviour, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static ISpecializedScalarQuantitySyntax? CreateSyntax(SpecializedScalarQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new SpecializedScalarQuantitySyntax(recorder.AttributeNameLocation, recorder.OriginalLocation, recorder.AllowNegativeLocation, recorder.InheritOperationsLocation, recorder.InheritProcessesLocation, recorder.InheritPropertiesLocation, recorder.InheritConstantsLocation,
            recorder.InheritConversionsLocation, recorder.ForwardsCastOperatorBehaviourLocation, recorder.BackwardsCastOperatorBehaviourLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class SpecializedScalarQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Original { get; private set; }
        public bool? AllowNegative { get; private set; }
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
        public Location AllowNegativeLocation { get; private set; } = Location.None;
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
            yield return ("AllowNegative", Adapters.For<bool>(RecordAllowNegative));
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

        private void RecordAllowNegative(bool allowNegative, Location location)
        {
            AllowNegative = allowNegative;
            AllowNegativeLocation = location;
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

    private sealed record class RawSpecializedScalarQuantity : IRawSpecializedScalarQuantity
    {
        private ITypeSymbol Original { get; }
        private bool? AllowNegative { get; }

        private bool? InheritOperations { get; }
        private bool? InheritProcesses { get; }
        private bool? InheritProperties { get; }
        private bool? InheritConstants { get; }
        private bool? InheritConversions { get; }

        private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
        private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

        private bool? ImplementSum { get; }
        private bool? ImplementDifference { get; }

        private ISpecializedScalarQuantitySyntax? Syntax { get; }

        public RawSpecializedScalarQuantity(ITypeSymbol original, bool? allowNegative, bool? inheritOperations, bool? inheritProcesses, bool? inheritProperties, bool? inheritConstants, bool? inheritConversions,
            ConversionOperatorBehaviour? forwardsCastOperatorBehaviour, ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedScalarQuantitySyntax? syntax)
        {
            Original = original;
            AllowNegative = allowNegative;

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

        ITypeSymbol IRawSpecializedScalarQuantity.Original => Original;
        bool? IRawSpecializedScalarQuantity.AllowNegative => AllowNegative;

        bool? IRawSpecializedScalarQuantity.InheritOperations => InheritOperations;
        bool? IRawSpecializedScalarQuantity.InheritProcesses => InheritProcesses;
        bool? IRawSpecializedScalarQuantity.InheritProperties => InheritProperties;
        bool? IRawSpecializedScalarQuantity.InheritConstants => InheritConstants;
        bool? IRawSpecializedScalarQuantity.InheritConversions => InheritConversions;

        ConversionOperatorBehaviour? IRawSpecializedScalarQuantity.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
        ConversionOperatorBehaviour? IRawSpecializedScalarQuantity.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

        bool? IRawSpecializedScalarQuantity.ImplementSum => ImplementSum;
        bool? IRawSpecializedScalarQuantity.ImplementDifference => ImplementDifference;

        ISpecializedScalarQuantitySyntax? IRawSpecializedScalarQuantity.Syntax => Syntax;
    }

    private sealed record class SpecializedScalarQuantitySyntax : ISpecializedScalarQuantitySyntax
    {
        private Location AttributeName { get; }

        private Location Original { get; }
        private Location AllowNegative { get; }
        private Location InheritOperations { get; }
        private Location InheritProcesses { get; }
        private Location InheritProperties { get; }
        private Location InheritConstants { get; }
        private Location InheritConversions { get; }
        private Location ForwardsCastOperatorBehaviour { get; }
        private Location BackwardsCastOperatorBehaviour { get; }
        private Location ImplementSum { get; }
        private Location ImplementDifference { get; }

        public SpecializedScalarQuantitySyntax(Location attributeName, Location original, Location allowNegative, Location inheritOperations, Location inheritProcesses, Location inheritProperties, Location inheritConstants,
            Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour, Location implementSum, Location implementDifference)
        {
            AttributeName = attributeName;

            Original = original;
            AllowNegative = allowNegative;

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

        Location ISpecializedScalarQuantitySyntax.Original => Original;
        Location ISpecializedScalarQuantitySyntax.AllowNegative => AllowNegative;
        Location ISpecializedScalarQuantitySyntax.InheritOperations => InheritOperations;
        Location ISpecializedScalarQuantitySyntax.InheritProcesses => InheritProcesses;
        Location ISpecializedScalarQuantitySyntax.InheritProperties => InheritProperties;
        Location ISpecializedScalarQuantitySyntax.InheritConstants => InheritConstants;
        Location ISpecializedScalarQuantitySyntax.InheritConversions => InheritConversions;
        Location ISpecializedScalarQuantitySyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
        Location ISpecializedScalarQuantitySyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
        Location ISpecializedScalarQuantitySyntax.ImplementSum => ImplementSum;
        Location ISpecializedScalarQuantitySyntax.ImplementDifference => ImplementDifference;
    }
}
