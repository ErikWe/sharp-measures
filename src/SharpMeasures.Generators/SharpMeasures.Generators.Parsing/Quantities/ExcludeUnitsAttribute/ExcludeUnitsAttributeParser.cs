namespace SharpMeasures.Generators.Parsing.Quantities.ExcludeUnitsAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ExcludeUnitsAttribute"/> to be parsed.</summary>
public sealed class ExcludeUnitsAttributeParser : IConstructiveSyntacticAttributeParser<IRawExcludeUnits>, IConstructiveSemanticAttributeParser<IRawExcludeUnits>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ExcludeUnitsAttributeParser"/>, parsing the arguments of a <see cref="ExcludeUnitsAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ExcludeUnitsAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawExcludeUnits? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        ExcludeUnitsAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawExcludeUnits? TryParse(AttributeData attributeData)
    {
        ExcludeUnitsAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawExcludeUnits? Create(ExcludeUnitsAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawExcludeUnits(recorder.ExcludedUnits, recorder.StackingMode, CreateSyntax(recorder, parsingMode));
    }

    private static IExcludeUnitsSyntax? CreateSyntax(ExcludeUnitsAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ExcludeUnitsSyntax(recorder.ExcludedUnitsCollectionLocation, recorder.ExcludeUnitsElementLocations, recorder.StackingModeLocation);
    }

    private sealed class ExcludeUnitsAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<string?>? ExcludedUnits { get; private set; }
        public FilterStackingMode? StackingMode { get; private set; }

        public Location ExcludedUnitsCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> ExcludeUnitsElementLocations { get; private set; } = Array.Empty<Location>();
        public Location StackingModeLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("StackingMode", Adapters.For<FilterStackingMode>(RecordStackingMode));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("ExcludedUnits", Adapters.ForNullable<string>(RecordExcludedUnits));
        }

        private void RecordExcludedUnits(IReadOnlyList<string?>? excludedUnits, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            ExcludedUnits = excludedUnits;
            ExcludedUnitsCollectionLocation = collectionLocation;
            ExcludeUnitsElementLocations = elementLocations;
        }

        private void RecordStackingMode(FilterStackingMode stackingMode, Location location)
        {
            StackingMode = stackingMode;
            StackingModeLocation = location;
        }
    }
}
