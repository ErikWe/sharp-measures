namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="DefaultUnitAttribute"/> to be parsed.</summary>
public sealed class DefaultUnitAttributeParser : IConstructiveSyntacticAttributeParser<IRawDefaultUnit>, IConstructiveSemanticAttributeParser<IRawDefaultUnit>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="DefaultUnitAttributeParser"/>, parsing the arguments of a <see cref="DefaultUnitAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public DefaultUnitAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawDefaultUnit? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        DefaultUnitAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        recorder.RecordAttributeNameLocation(attributeSyntax.Name.GetLocation());

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawDefaultUnit? TryParse(AttributeData attributeData)
    {
        DefaultUnitAttributeArgumentRecorder recoder = new();

        if (SemanticParser.TryParse(recoder, attributeData) is false)
        {
            return null;
        }

        return Create(recoder, AttributeParsingMode.Semantically);
    }

    private static IRawDefaultUnit? Create(DefaultUnitAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawDefaultUnit(recorder.Unit, recorder.Symbol, CreateSyntax(recorder, parsingMode));
    }

    private static IDefaultUnitSyntax? CreateSyntax(DefaultUnitAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new DefaultUnitSyntax(recorder.AttributeNameLocation, recorder.UnitLocation, recorder.SymbolLocation);
    }

    private sealed class DefaultUnitAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Unit { get; private set; }
        public string? Symbol { get; private set; }

        public Location AttributeNameLocation { get; private set; } = Location.None;

        public Location UnitLocation { get; private set; } = Location.None;
        public Location SymbolLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Unit", Adapters.ForNullable<string>(RecordUnit));
            yield return ("Symbol", Adapters.ForNullable<string>(RecordSymbol));
        }

        private void RecordUnit(string? unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }

        private void RecordSymbol(string? symbol, Location location)
        {
            Symbol = symbol;
            SymbolLocation = location;
        }

        public void RecordAttributeNameLocation(Location location)
        {
            AttributeNameLocation = location;
        }
    }

    private sealed record class RawDefaultUnit : IRawDefaultUnit
    {
        private string? Unit { get; }
        private string? Symbol { get; }

        private IDefaultUnitSyntax? Syntax { get; }

        public RawDefaultUnit(string? unit, string? symbol, IDefaultUnitSyntax? syntax)
        {
            Unit = unit;
            Symbol = symbol;

            Syntax = syntax;
        }

        string? IRawDefaultUnit.Unit => Unit;
        string? IRawDefaultUnit.Symbol => Symbol;

        IDefaultUnitSyntax? IRawDefaultUnit.Syntax => Syntax;
    }

    private sealed record class DefaultUnitSyntax : IDefaultUnitSyntax
    {
        private Location AttributeName { get; }

        private Location Unit { get; }
        private Location Symbol { get; }

        public DefaultUnitSyntax(Location attributeName, Location unit, Location symbol)
        {
            AttributeName = attributeName;

            Unit = unit;
            Symbol = symbol;
        }

        Location IAttributeSyntax.AttributeName => AttributeName;

        Location IDefaultUnitSyntax.Unit => Unit;
        Location IDefaultUnitSyntax.Symbol => Symbol;
    }
}
