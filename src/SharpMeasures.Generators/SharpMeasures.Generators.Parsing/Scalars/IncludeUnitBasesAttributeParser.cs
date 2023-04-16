namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="IncludeUnitBasesAttribute"/> to be parsed.</summary>
public sealed class IncludeUnitBasesAttributeParser : IConstructiveSyntacticAttributeParser<IRawIncludeUnitBases>, IConstructiveSemanticAttributeParser<IRawIncludeUnitBases>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="IncludeUnitBasesAttributeParser"/>, parsing the arguments of a <see cref="IncludeUnitBasesAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public IncludeUnitBasesAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawIncludeUnitBases? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        IncludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawIncludeUnitBases? TryParse(AttributeData attributeData)
    {
        IncludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawIncludeUnitBases? Create(IncludeUnitBasesAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawIncludeUnitBases(recorder.IncludedUnitBases, recorder.StackingMode, CreateSyntax(recorder, parsingMode));
    }

    private static IIncludeUnitBasesSyntax? CreateSyntax(IncludeUnitBasesAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new IncludeUnitBasesSyntax(recorder.IncludedUnitBasesCollectionLocation, recorder.IncludedUnitBasesElementLocations, recorder.StackingModeLocation);
    }

    private sealed class IncludeUnitBasesAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<string?>? IncludedUnitBases { get; private set; }
        public FilterStackingMode? StackingMode { get; private set; }

        public Location IncludedUnitBasesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> IncludedUnitBasesElementLocations { get; private set; } = Array.Empty<Location>();
        public Location StackingModeLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("StackingMode", Adapters.For<FilterStackingMode>(RecordStackingMode));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("IncludedUnitBases", Adapters.ForNullable<string>(RecordIncludedUnitBases));
        }

        private void RecordIncludedUnitBases(IReadOnlyList<string?>? includedUnitBases, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            IncludedUnitBases = includedUnitBases;
            IncludedUnitBasesCollectionLocation = collectionLocation;
            IncludedUnitBasesElementLocations = elementLocations;
        }

        private void RecordStackingMode(FilterStackingMode stackingMode, Location location)
        {
            StackingMode = stackingMode;
            StackingModeLocation = location;
        }
    }

    private sealed record class RawIncludeUnitBases : IRawIncludeUnitBases
    {
        private IReadOnlyList<string?>? IncludedUnitBases { get; }
        private FilterStackingMode? StackingMode { get; }

        private IIncludeUnitBasesSyntax? Syntax { get; }

        public RawIncludeUnitBases(IReadOnlyList<string?>? includedUnitBases, FilterStackingMode? stackingMode, IIncludeUnitBasesSyntax? syntax)
        {
            IncludedUnitBases = includedUnitBases;
            StackingMode = stackingMode;

            Syntax = syntax;
        }

        IReadOnlyList<string?>? IRawIncludeUnitBases.IncludedUnitBases => IncludedUnitBases;
        FilterStackingMode? IRawIncludeUnitBases.StackingMode => StackingMode;

        IIncludeUnitBasesSyntax? IRawIncludeUnitBases.Syntax => Syntax;
    }

    private sealed record class IncludeUnitBasesSyntax : IIncludeUnitBasesSyntax
    {
        private Location IncludedUnitBasesCollection { get; }
        private IReadOnlyList<Location> IncludedUnitBasesElements { get; }
        private Location StackingMode { get; }

        public IncludeUnitBasesSyntax(Location includedUnitBasesCollection, IReadOnlyList<Location> includedUnitBasesElements, Location stackingMode)
        {
            IncludedUnitBasesCollection = includedUnitBasesCollection;
            IncludedUnitBasesElements = includedUnitBasesElements;
            StackingMode = stackingMode;
        }

        Location IIncludeUnitBasesSyntax.IncludedUnitBasesCollection => IncludedUnitBasesCollection;
        IReadOnlyList<Location> IIncludeUnitBasesSyntax.IncludedUnitBasesElements => IncludedUnitBasesElements;
        Location IIncludeUnitBasesSyntax.StackingMode => StackingMode;
    }
}
