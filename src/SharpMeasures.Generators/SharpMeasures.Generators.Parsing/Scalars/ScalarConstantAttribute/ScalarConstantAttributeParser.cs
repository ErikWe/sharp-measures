namespace SharpMeasures.Generators.Parsing.Scalars.ScalarConstantAttribute;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using SharpMeasures;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScalarConstantAttribute"/> to be parsed.</summary>
public sealed class ScalarConstantAttributeParser : IConstructiveSyntacticAttributeParser<IRawScalarConstant>, IConstructiveSemanticAttributeParser<IRawScalarConstant>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScalarConstantAttributeParser"/>, parsing the arguments of a <see cref="ScalarConstantAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScalarConstantAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawScalarConstant? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        ScalarConstantAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawScalarConstant? TryParse(AttributeData attributeData)
    {
        ScalarConstantAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawScalarConstant? Create(ScalarConstantAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Value is null)
        {
            return null;
        }

        return new RawScalarConstant(recorder.Name, recorder.UnitInstanceName, recorder.Value.Value, recorder.GenerateMultipliesProperty, recorder.MultiplesName, CreateSyntax(recorder, parsingMode));
    }

    private static IScalarConstantSyntax? CreateSyntax(ScalarConstantAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ScalarConstantSyntax(recorder.NameLocation, recorder.UnitInstanceNameLocation, recorder.ValueLocation, recorder.GenerateMultiplesPropertyLocation, recorder.MultiplesNameLocation);
    }

    private sealed class ScalarConstantAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? UnitInstanceName { get; private set; }
        public OneOf<double, string?>? Value { get; private set; }
        public bool? GenerateMultipliesProperty { get; private set; }
        public string? MultiplesName { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location UnitInstanceNameLocation { get; private set; } = Location.None;
        public Location ValueLocation { get; private set; } = Location.None;
        public Location GenerateMultiplesPropertyLocation { get; private set; } = Location.None;
        public Location MultiplesNameLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("UnitInstanceName", Adapters.ForNullable<string>(RecordUnitInstance));
            yield return ("Value", Adapters.For<double>(RecordValue));
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
            yield return ("GenerateMultiplesProperty", Adapters.For<bool>(RecordGenerateMultiplesProperty));
            yield return ("MultiplesName", Adapters.ForNullable<string>(RecordMultiplesName));
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordUnitInstance(string? unitInstance, Location location)
        {
            UnitInstanceName = unitInstance;
            UnitInstanceNameLocation = location;
        }

        private void RecordValue(double value, Location location)
        {
            Value = value;
            ValueLocation = location;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Value = expression;
            ValueLocation = location;
        }

        private void RecordGenerateMultiplesProperty(bool generateMultiplesProperty, Location location)
        {
            GenerateMultipliesProperty = generateMultiplesProperty;
            GenerateMultiplesPropertyLocation = location;
        }

        private void RecordMultiplesName(string? multiplesName, Location location)
        {
            if (multiplesName is not null)
            {
                MultiplesName = multiplesName;
            }

            MultiplesNameLocation = location;
        }
    }
}
