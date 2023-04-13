namespace SharpMeasures.Generators.Parsing.Scalars.ExcludeUnitBasesAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ExcludeUnitBasesAttribute"/> to be parsed.</summary>
public sealed class ExcludeUnitBasesAttributeParser : IConstructiveSyntacticAttributeParser<IRawExcludeUnitBases>, IConstructiveSemanticAttributeParser<IRawExcludeUnitBases>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ExcludeUnitBasesAttributeParser"/>, parsing the arguments of a <see cref="ExcludeUnitBasesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ExcludeUnitBasesAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawExcludeUnitBases? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        ExcludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawExcludeUnitBases? TryParse(AttributeData attributeData)
    {
        ExcludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawExcludeUnitBases? Create(ExcludeUnitBasesAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawExcludeUnitBases(recorder.ExcludedUnitBases, recorder.StackingMode, CreateSyntax(recorder, parsingMode));
    }

    private static IExcludeUnitBasesSyntax? CreateSyntax(ExcludeUnitBasesAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ExcludeUnitBasesSyntax(recorder.ExcludedUnitBasesCollectionLocation, recorder.ExcludeUnitBasesElementLocations, recorder.StackingModeLocation);
    }

    private sealed class ExcludeUnitBasesAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<string?>? ExcludedUnitBases { get; private set; }
        public FilterStackingMode? StackingMode { get; private set; }

        public Location ExcludedUnitBasesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> ExcludeUnitBasesElementLocations { get; private set; } = Array.Empty<Location>();
        public Location StackingModeLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("StackingMode", Adapters.For<FilterStackingMode>(RecordStackingMode));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("ExcludedUnitBases", Adapters.ForNullable<string>(RecordExcludedUnitBases));
        }

        private void RecordExcludedUnitBases(IReadOnlyList<string?>? excludedUnitBases, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            ExcludedUnitBases = excludedUnitBases;
            ExcludedUnitBasesCollectionLocation = collectionLocation;
            ExcludeUnitBasesElementLocations = elementLocations;
        }

        private void RecordStackingMode(FilterStackingMode stackingMode, Location location)
        {
            StackingMode = stackingMode;
            StackingModeLocation = location;
        }
    }
}
