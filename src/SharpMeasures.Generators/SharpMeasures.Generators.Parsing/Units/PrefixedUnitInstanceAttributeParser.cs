namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="PrefixedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class PrefixedUnitInstanceAttributeParser : IConstructiveSyntacticAttributeParser<IRawPrefixedUnitInstance>, IConstructiveSemanticAttributeParser<IRawPrefixedUnitInstance>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="PrefixedUnitInstanceAttributeParser"/>, parsing the arguments of a <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public PrefixedUnitInstanceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawPrefixedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        PrefixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawPrefixedUnitInstance? TryParse(AttributeData attributeData)
    {
        PrefixedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawPrefixedUnitInstance? Create(PrefixedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Prefix is null)
        {
            return null;
        }

        return new RawPrefixedUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, recorder.Prefix.Value, CreateSyntax(recorder, parsingMode));
    }

    private IPrefixedUnitInstanceSyntax? CreateSyntax(PrefixedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new PrefixedUnitInstanceSyntax(recorder.AttributeNameLocation, recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation, recorder.PrefixLocation);
    }

    private sealed class PrefixedUnitInstanceAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }
        public OneOf<MetricPrefixName, BinaryPrefixName>? Prefix { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;
        public Location PrefixLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("OriginalUnitInstance", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
            yield return ("MetricPrefix", Adapters.For<MetricPrefixName>(RecordMetricPrefix));
            yield return ("BinaryPrefix", Adapters.For<BinaryPrefixName>(RecordBinaryPrefix));
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

        private void RecordMetricPrefix(MetricPrefixName metricPrefix, Location location)
        {
            Prefix = metricPrefix;
            PrefixLocation = location;
        }

        private void RecordBinaryPrefix(BinaryPrefixName binaryPrefix, Location location)
        {
            Prefix = binaryPrefix;
            PrefixLocation = location;
        }

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawPrefixedUnitInstance : ARawModifiedUnitInstance<IPrefixedUnitInstanceSyntax>, IRawPrefixedUnitInstance
    {
        private OneOf<MetricPrefixName, BinaryPrefixName> Prefix { get; }

        public RawPrefixedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<MetricPrefixName, BinaryPrefixName> prefix, IPrefixedUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax)
        {
            Prefix = prefix;
        }

        OneOf<MetricPrefixName, BinaryPrefixName> IRawPrefixedUnitInstance.Prefix => Prefix;

        IPrefixedUnitInstanceSyntax? IRawPrefixedUnitInstance.Syntax => Syntax;
    }

    private sealed record class PrefixedUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IPrefixedUnitInstanceSyntax
    {
        private Location Prefix { get; }

        public PrefixedUnitInstanceSyntax(Location attributeName, Location name, Location pluralForm, Location originalUnitInstance, Location prefix) : base(attributeName, name, pluralForm, originalUnitInstance)
        {
            Prefix = prefix;
        }

        Location IPrefixedUnitInstanceSyntax.Prefix => Prefix;
    }
}
