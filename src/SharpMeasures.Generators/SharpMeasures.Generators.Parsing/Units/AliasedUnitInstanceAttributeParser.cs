namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="UnitInstanceAliasAttribute"/> to be parsed.</summary>
public sealed class AliasedUnitInstanceAttributeParser : IConstructiveSyntacticAttributeParser<IRawAliasedUnitInstance>, IConstructiveSemanticAttributeParser<IRawAliasedUnitInstance>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="AliasedUnitInstanceAttributeParser"/>, parsing the arguments of a <see cref="UnitInstanceAliasAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public AliasedUnitInstanceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawAliasedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        AliasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawAliasedUnitInstance? TryParse(AttributeData attributeData)
    {
        AliasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawAliasedUnitInstance? Create(AliasedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawAliasedUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, CreateSyntax(recorder, parsingMode));
    }

    private IAliasedUnitInstanceSyntax? CreateSyntax(AliasedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new AliasedUnitInstanceSyntax(recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation);
    }

    private sealed class AliasedUnitInstanceAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("AliasOf", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
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

        private void RecordOriginalUnitInstance(string? originalUnitInstance, Location location)
        {
            OriginalUnitInstance = originalUnitInstance;
            OriginalUnitInstanceLocation = location;
        }
    }

    private sealed record class RawAliasedUnitInstance : ARawModifiedUnitInstance<IAliasedUnitInstanceSyntax>, IRawAliasedUnitInstance
    {
        public RawAliasedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, IAliasedUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax) { }

        IAliasedUnitInstanceSyntax? IRawAliasedUnitInstance.Syntax => Syntax;
    }

    private sealed record class AliasedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IAliasedUnitInstanceSyntax
    {
        public AliasedUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance) : base(name, pluralForm, originalUnitInstance) { }
    }
}
