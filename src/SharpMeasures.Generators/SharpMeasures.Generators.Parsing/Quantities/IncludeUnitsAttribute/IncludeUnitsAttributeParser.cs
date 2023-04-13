namespace SharpMeasures.Generators.Parsing.Quantities.IncludeUnitsAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="IncludeUnitsAttribute"/> to be parsed.</summary>
public sealed class IncludeUnitsAttributeParser : IConstructiveSyntacticAttributeParser<IRawIncludeUnits>, IConstructiveSemanticAttributeParser<IRawIncludeUnits>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="IncludeUnitsAttributeParser"/>, parsing the arguments of a <see cref="IncludeUnitsAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public IncludeUnitsAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawIncludeUnits? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        IncludeUnitsAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawIncludeUnits? TryParse(AttributeData attributeData)
    {
        IncludeUnitsAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawIncludeUnits? Create(IncludeUnitsAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawIncludeUnits(recorder.IncludedUnits, recorder.StackingMode, CreateSyntax(recorder, parsingMode));
    }

    private static IIncludeUnitsSyntax? CreateSyntax(IncludeUnitsAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new IncludeUnitsSyntax(recorder.IncludedUnitsCollectionLocation, recorder.IncludedUnitsElementLocations, recorder.StackingModeLocation);
    }

    private sealed class IncludeUnitsAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<string?>? IncludedUnits { get; private set; }
        public FilterStackingMode? StackingMode { get; private set; }

        public Location IncludedUnitsCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> IncludedUnitsElementLocations { get; private set; } = Array.Empty<Location>();
        public Location StackingModeLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("StackingMode", Adapters.For<FilterStackingMode>(RecordStackingMode));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("IncludedUnits", Adapters.ForNullable<string>(RecordIncludedUnits));
        }

        private void RecordIncludedUnits(IReadOnlyList<string?>? includedUnits, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            IncludedUnits = includedUnits;
            IncludedUnitsCollectionLocation = collectionLocation;
            IncludedUnitsElementLocations = elementLocations;
        }

        private void RecordStackingMode(FilterStackingMode stackingMode, Location location)
        {
            StackingMode = stackingMode;
            StackingModeLocation = location;
        }
    }
}
