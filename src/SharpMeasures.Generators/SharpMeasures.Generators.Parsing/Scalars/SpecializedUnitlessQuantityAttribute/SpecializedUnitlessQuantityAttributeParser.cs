namespace SharpMeasures.Generators.Parsing.Scalars.SpecializedUnitlessQuantityAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/> to be parsed.</summary>
public sealed class SpecializedUnitlessQuantityAttributeParser : IConstructiveSyntacticAttributeParser<IRawSpecializedUnitlessQuantity>, IConstructiveSemanticAttributeParser<IRawSpecializedUnitlessQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="SpecializedUnitlessQuantityAttributeParser"/>, parsing the arguments of a <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public SpecializedUnitlessQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawSpecializedUnitlessQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        SpecializedUnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawSpecializedUnitlessQuantity? TryParse(AttributeData attributeData)
    {
        SpecializedUnitlessQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawSpecializedUnitlessQuantity? Create(SpecializedUnitlessQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Original is null)
        {
            return null;
        }

        return new RawSpecializedUnitlessQuantity(recorder.Original, recorder.AllowNegative, recorder.InheritOperations, recorder.InheritProcesses, recorder.InheritProperties, recorder.InheritConversions,
            recorder.ForwardsCastOperatorBehaviour, recorder.BackwardsCastOperatorBehaviour, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static ISpecializedUnitlessQuantitySyntax? CreateSyntax(SpecializedUnitlessQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new SpecializedUnitlessQuantitySyntax(recorder.OriginalLocation, recorder.AllowNegativeLocation, recorder.InheritOperationsLocation, recorder.InheritProcessesLocation, recorder.InheritPropertiesLocation,
            recorder.InheritConversionsLocation, recorder.ForwardsCastOperatorBehaviourLocation, recorder.BackwardsCastOperatorBehaviourLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class SpecializedUnitlessQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Original { get; private set; }
        public bool? AllowNegative { get; private set; }
        public bool? InheritOperations { get; private set; }
        public bool? InheritProcesses { get; private set; }
        public bool? InheritProperties { get; private set; }
        public bool? InheritConversions { get; private set; }
        public ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; private set; }
        public ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location OriginalLocation { get; private set; } = Location.None;
        public Location AllowNegativeLocation { get; private set; } = Location.None;
        public Location InheritOperationsLocation { get; private set; } = Location.None;
        public Location InheritProcessesLocation { get; private set; } = Location.None;
        public Location InheritPropertiesLocation { get; private set; } = Location.None;
        public Location InheritConstantsLocation { get; private set; } = Location.None;
        public Location InheritConversionsLocation { get; private set; } = Location.None;
        public Location InheritBasesLocation { get; private set; } = Location.None;
        public Location InheritUnitsLocation { get; private set; } = Location.None;
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
}
