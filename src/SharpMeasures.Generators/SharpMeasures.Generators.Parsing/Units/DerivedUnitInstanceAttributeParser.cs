namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures;
using SharpMeasures.Generators.Parsing;

using SharpMeasures.Generators.Parsing.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="DerivedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class DerivedUnitInstanceAttributeParser : IConstructiveSyntacticAttributeParser<IRawDerivedUnitInstance>, IConstructiveSemanticAttributeParser<IRawDerivedUnitInstance>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DerivedUnitInstanceAttributeParser"/>, parsing the arguments of a <see cref="DerivedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DerivedUnitInstanceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawDerivedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        DerivedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawDerivedUnitInstance? TryParse(AttributeData attributeData)
    {
        DerivedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawDerivedUnitInstance? Create(DerivedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawDerivedUnitInstance(recorder.Name, recorder.PluralForm, recorder.DerivationID, recorder.Units, CreateSyntax(recorder, parsingMode));
    }

    private IDerivedUnitInstanceSyntax? CreateSyntax(DerivedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new DerivedUnitInstanceSyntax(recorder.NameLocation, recorder.PluralFormLocation, recorder.DerivationIDLocation, recorder.UnitsCollectionLocation, recorder.UnitsElementLocations);
    }

    private sealed class DerivedUnitInstanceAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? DerivationID { get; private set; }
        public IReadOnlyList<string?>? Units { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location DerivationIDLocation { get; private set; } = Location.None;
        public Location UnitsCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> UnitsElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("DerivationID", Adapters.ForNullable<string>(RecordDerivationID));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Units", Adapters.ForNullable<string>(RecordUnits));
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordPluralForm(string? pluralForm, Location location)
        {
            if (pluralForm is not null)
            {
                PluralForm = pluralForm;
            }

            PluralFormLocation = location;
        }

        private void RecordDerivationID(string? derivationID, Location location)
        {
            if (derivationID is not null)
            {
                DerivationID = derivationID;
            }

            DerivationIDLocation = location;
        }

        private void RecordUnits(IReadOnlyList<string?>? units, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Units = units;
            UnitsCollectionLocation = collectionLocation;
            UnitsElementLocations = elementLocations;
        }
    }

    private sealed record class RawDerivedUnitInstance : ARawUnitInstance<IDerivedUnitInstanceSyntax>, IRawDerivedUnitInstance
    {
        private string? DerivationID { get; }
        private IReadOnlyList<string?>? Units { get; }

        public RawDerivedUnitInstance(string? name, string? pluralForm, string? derivationID, IReadOnlyList<string?>? units, IDerivedUnitInstanceSyntax? syntax) : base(name, pluralForm, syntax)
        {
            DerivationID = derivationID;
            Units = units;
        }

        string? IRawDerivedUnitInstance.DerivationID => DerivationID;
        IReadOnlyList<string?>? IRawDerivedUnitInstance.Units => Units;

        IDerivedUnitInstanceSyntax? IRawDerivedUnitInstance.Syntax => Syntax;
    }

    private sealed record class DerivedUnitInstanceSyntax : AUnitInstanceSyntax, IDerivedUnitInstanceSyntax
    {
        private Location DerivationID { get; }
        private Location UnitsCollection { get; }
        private IReadOnlyList<Location> UnitsElements { get; }

        public DerivedUnitInstanceSyntax(Location name, Location pluralForm, Location derivationID, Location unitsCollection, IReadOnlyList<Location> unitsElements) : base(name, pluralForm)
        {
            DerivationID = derivationID;

            UnitsCollection = unitsCollection;
            UnitsElements = unitsElements;
        }

        Location IDerivedUnitInstanceSyntax.DerivationID => DerivationID;
        Location IDerivedUnitInstanceSyntax.UnitsCollection => UnitsCollection;
        IReadOnlyList<Location> IDerivedUnitInstanceSyntax.UnitsElements => UnitsElements;
    }
}
