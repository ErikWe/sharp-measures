namespace SharpMeasures.Generators.Parsing.Units.BiasedUnitInstanceAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="BiasedUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class BiasedUnitInstanceAttributeParser : IConstructiveSyntacticAttributeParser<IRawBiasedUnitInstance>, IConstructiveSemanticAttributeParser<IRawBiasedUnitInstance>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="BiasedUnitInstanceAttributeParser"/>, parsing the arguments of a <see cref="BiasedUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public BiasedUnitInstanceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawBiasedUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        BiasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawBiasedUnitInstance? TryParse(AttributeData attributeData)
    {
        BiasedUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawBiasedUnitInstance? Create(BiasedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Bias is null)
        {
            return null;
        }

        return new RawBiasedUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, recorder.Bias.Value, CreateSyntax(recorder, parsingMode));
    }

    private IBiasedUnitInstanceSyntax? CreateSyntax(BiasedUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new BiasedUnitInstanceSyntax(recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation, recorder.BiasLocation);
    }

    private sealed class BiasedUnitInstanceAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }
        public OneOf<double, string?>? Bias { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;
        public Location BiasLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("OriginalUnitInstance", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
            yield return ("Bias", Adapters.For<double>(RecordBias));
            yield return ("BiasExpression", Adapters.ForNullable<string>(RecordBiasExpression));
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

        private void RecordBias(double bias, Location location)
        {
            Bias = bias;
            BiasLocation = location;
        }

        private void RecordBiasExpression(string? expression, Location location)
        {
            Bias = expression;
            BiasLocation = location;
        }
    }
}
