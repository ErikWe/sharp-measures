namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="FixedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class FixedUnitInstanceAttributeParser : IConstructiveSyntacticAttributeParser<IRawFixedUnitInstance>, IConstructiveSemanticAttributeParser<IRawFixedUnitInstance>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="FixedUnitInstanceAttributeParser"/>, parsing the arguments of a <see cref="FixedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public FixedUnitInstanceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawFixedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        FixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawFixedUnitInstance? TryParse(AttributeData attributeData)
    {
        FixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawFixedUnitInstance? Create(FixedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawFixedUnitInstance(recorder.Name, recorder.PluralForm, CreateSyntax(recorder, parsingMode));
    }

    private IFixedUnitInstanceSyntax? CreateSyntax(FixedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new FixedUnitInstanceSyntax(recorder.NameLocation, recorder.PluralFormLocation);
    }

    private sealed class FixedUnitInstanceAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
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
    }

    private sealed record class RawFixedUnitInstance : ARawUnitInstance<IFixedUnitInstanceSyntax>, IRawFixedUnitInstance
    {
        public RawFixedUnitInstance(string? name, string? pluralForm, IFixedUnitInstanceSyntax? syntax) : base(name, pluralForm, syntax) { }

        IFixedUnitInstanceSyntax? IRawFixedUnitInstance.Syntax => Syntax;
    }

    private sealed record class FixedUnitInstanceSyntax : AUnitInstanceSyntax, IFixedUnitInstanceSyntax
    {
        public FixedUnitInstanceSyntax(Location name, Location pluralForm) : base(name, pluralForm) { }
    }
}
