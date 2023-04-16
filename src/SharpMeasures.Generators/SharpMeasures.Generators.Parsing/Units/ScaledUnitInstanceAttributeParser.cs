namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures.Generators.Parsing.Units.Common;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScaledUnitInstanceAttribute"/> to be parsed.</summary>
public sealed class ScaledUnitInstanceAttributeParser : IConstructiveSyntacticAttributeParser<IRawScaledUnitInstance>, IConstructiveSemanticAttributeParser<IRawScaledUnitInstance>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScaledUnitInstanceAttributeParser"/>, parsing the arguments of a <see cref="ScaledUnitInstanceAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScaledUnitInstanceAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawScaledUnitInstance? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        ScaledUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawScaledUnitInstance? TryParse(AttributeData attributeData)
    {
        ScaledUnitInstanceAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private IRawScaledUnitInstance? Create(ScaledUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Scale is null)
        {
            return null;
        }

        return new RawScaledUnitInstance(recorder.Name, recorder.PluralForm, recorder.OriginalUnitInstance, recorder.Scale.Value, CreateSyntax(recorder, parsingMode));
    }

    private IScaledUnitInstanceSyntax? CreateSyntax(ScaledUnitInstanceAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ScaledUnitInstanceSyntax(recorder.NameLocation, recorder.PluralFormLocation, recorder.OriginalUnitInstanceLocation, recorder.ScaleLocation);
    }

    private sealed class ScaledUnitInstanceAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? PluralForm { get; private set; }
        public string? OriginalUnitInstance { get; private set; }
        public OneOf<double, string?>? Scale { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location PluralFormLocation { get; private set; } = Location.None;
        public Location OriginalUnitInstanceLocation { get; private set; } = Location.None;
        public Location ScaleLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("PluralForm", Adapters.ForNullable<string>(RecordPluralForm));
            yield return ("OriginalUnitInstance", Adapters.ForNullable<string>(RecordOriginalUnitInstance));
            yield return ("Scale", Adapters.For<double>(RecordScale));
            yield return ("ScaleExpression", Adapters.ForNullable<string>(RecordScaleExpression));
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

        private void RecordScale(double scale, Location location)
        {
            Scale = scale;
            ScaleLocation = location;
        }

        private void RecordScaleExpression(string? expression, Location location)
        {
            Scale = expression;
            ScaleLocation = location;
        }
    }

    private sealed record class RawScaledUnitInstance : ARawModifiedUnitInstance<IScaledUnitInstanceSyntax>, IRawScaledUnitInstance
    {
        private OneOf<double, string?> Scale { get; }

        public RawScaledUnitInstance(string? name, string? pluralForm, string? originalUnitInstance, OneOf<double, string?> scale, IScaledUnitInstanceSyntax? syntax) : base(name, pluralForm, originalUnitInstance, syntax)
        {
            Scale = scale;
        }

        OneOf<double, string?> IRawScaledUnitInstance.Scale => Scale;

        IScaledUnitInstanceSyntax? IRawScaledUnitInstance.Syntax => Syntax;
    }

    internal sealed record class ScaledUnitInstanceSyntax : AModifiedUnitInstanceSyntax, IScaledUnitInstanceSyntax
    {
        private Location Scale { get; }

        public ScaledUnitInstanceSyntax(Location name, Location pluralForm, Location originalUnitInstance, Location scale) : base(name, pluralForm, originalUnitInstance)
        {
            Scale = scale;
        }

        Location IScaledUnitInstanceSyntax.Scale => Scale;
    }
}
