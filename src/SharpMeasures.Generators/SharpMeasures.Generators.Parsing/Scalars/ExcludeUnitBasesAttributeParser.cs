namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

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
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        ExcludeUnitBasesAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

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

        return new ExcludeUnitBasesSyntax(recorder.AttributeNameLocation, recorder.ExcludedUnitBasesCollectionLocation, recorder.ExcludeUnitBasesElementLocations, recorder.StackingModeLocation);
    }

    private sealed class ExcludeUnitBasesAttributeArgumentRecorder : AArgumentRecorder
    {
        public IReadOnlyList<string?>? ExcludedUnitBases { get; private set; }
        public FilterStackingMode? StackingMode { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

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

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawExcludeUnitBases : IRawExcludeUnitBases
    {
        private IReadOnlyList<string?>? ExcludedUnitBases { get; }
        private FilterStackingMode? StackingMode { get; }

        private IExcludeUnitBasesSyntax? Syntax { get; }

        public RawExcludeUnitBases(IReadOnlyList<string?>? excludedUnitBases, FilterStackingMode? stackingMode, IExcludeUnitBasesSyntax? syntax)
        {
            ExcludedUnitBases = excludedUnitBases;
            StackingMode = stackingMode;

            Syntax = syntax;
        }

        IReadOnlyList<string?>? IRawExcludeUnitBases.ExcludedUnitBases => ExcludedUnitBases;
        FilterStackingMode? IRawExcludeUnitBases.StackingMode => StackingMode;

        IExcludeUnitBasesSyntax? IRawExcludeUnitBases.Syntax => Syntax;
    }

    private sealed record class ExcludeUnitBasesSyntax : IExcludeUnitBasesSyntax
    {
        private Location AttributeName { get; }

        private Location ExcludedUnitBasesCollection { get; }
        private IReadOnlyList<Location> ExcludeUnitBasesElements { get; }
        private Location StackingMode { get; }

        public ExcludeUnitBasesSyntax(Location attributeName, Location excludedUnitBasesCollection, IReadOnlyList<Location> excludeUnitBasesElements, Location stackingMode)
        {
            AttributeName = attributeName;

            ExcludedUnitBasesCollection = excludedUnitBasesCollection;
            ExcludeUnitBasesElements = excludeUnitBasesElements;
            StackingMode = stackingMode;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IExcludeUnitBasesSyntax.ExcludedUnitBasesCollection => ExcludedUnitBasesCollection;
        IReadOnlyList<Location> IExcludeUnitBasesSyntax.ExcludedUnitBasesElements => ExcludeUnitBasesElements;
        Location IExcludeUnitBasesSyntax.StackingMode => StackingMode;
    }
}
