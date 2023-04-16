namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityPropertyAttribute{TResult}"/> to be parsed.</summary>
public sealed class QuantityPropertyAttributeParser : IConstructiveSyntacticAttributeParser<IRawQuantityProperty>, IConstructiveSemanticAttributeParser<IRawQuantityProperty>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityPropertyAttributeParser"/>, parsing the arguments of a <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityPropertyAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawQuantityProperty? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        QuantityPropertyAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawQuantityProperty? TryParse(AttributeData attributeData)
    {
        QuantityPropertyAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawQuantityProperty? Create(QuantityPropertyAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Result is null)
        {
            return null;
        }

        return new RawQuantityProperty(recorder.Result, recorder.Name, recorder.Expression, recorder.ImplementStatically, CreateSyntax(recorder, parsingMode));
    }

    private static IQuantityPropertySyntax? CreateSyntax(QuantityPropertyAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new QuantityPropertySyntax(recorder.ResultLocation, recorder.NameLocation, recorder.ExpressionLocation, recorder.ImplementStaticallyLocation);
    }

    private sealed class QuantityPropertyAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Result { get; private set; }

        public string? Name { get; private set; }
        public string? Expression { get; private set; }
        public bool? ImplementStatically { get; private set; }

        public Location ResultLocation { get; private set; } = Location.None;

        public Location NameLocation { get; private set; } = Location.None;
        public Location ExpressionLocation { get; private set; } = Location.None;
        public Location ImplementStaticallyLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TResult", Adapters.For(RecordResult));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("Expression", Adapters.ForNullable<string>(RecordExpression));
            yield return ("ImplementStatically", Adapters.For<bool>(RecordImplementStatically));
        }

        private void RecordResult(ITypeSymbol result, Location location)
        {
            Result = result;
            ResultLocation = location;
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordExpression(string? expression, Location location)
        {
            Expression = expression;
            ExpressionLocation = location;
        }

        private void RecordImplementStatically(bool implementStatically, Location location)
        {
            ImplementStatically = implementStatically;
            ImplementStaticallyLocation = location;
        }
    }

    private sealed record class RawQuantityProperty : IRawQuantityProperty
    {
        private ITypeSymbol Result { get; }
        private string? Name { get; }
        private string? Expression { get; }
        private bool? ImplementStatically { get; }

        private IQuantityPropertySyntax? Syntax { get; }

        public RawQuantityProperty(ITypeSymbol result, string? name, string? expression, bool? implementStatically, IQuantityPropertySyntax? syntax)
        {
            Result = result;
            Name = name;
            Expression = expression;
            ImplementStatically = implementStatically;

            Syntax = syntax;
        }

        ITypeSymbol IRawQuantityProperty.Result => Result;
        string? IRawQuantityProperty.Name => Name;
        string? IRawQuantityProperty.Expression => Expression;
        bool? IRawQuantityProperty.ImplementStatically => ImplementStatically;

        IQuantityPropertySyntax? IRawQuantityProperty.Syntax => Syntax;
    }

    private sealed record class QuantityPropertySyntax : IQuantityPropertySyntax
    {
        private Location Result { get; }
        private Location Name { get; }
        private Location Expression { get; }
        private Location ImplementStatically { get; }

        /// <summary>Instantiates a <see cref="QuantityPropertySyntax"/>, representing syntactical information about a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
        /// <param name="result"><inheritdoc cref="IQuantityPropertySyntax.Result" path="/summary"/></param>
        /// <param name="name"><inheritdoc cref="IQuantityPropertySyntax.Name" path="/summary"/></param>
        /// <param name="expression"><inheritdoc cref="IQuantityPropertySyntax.Expression" path="/summary"/></param>
        /// <param name="implementStatically"><inheritdoc cref="IQuantityPropertySyntax.ImplementStatically" path="/summary"/></param>
        public QuantityPropertySyntax(Location result, Location name, Location expression, Location implementStatically)
        {
            Result = result;
            Name = name;
            Expression = expression;
            ImplementStatically = implementStatically;
        }

        Location IQuantityPropertySyntax.Result => Result;
        Location IQuantityPropertySyntax.Name => Name;
        Location IQuantityPropertySyntax.Expression => Expression;
        Location IQuantityPropertySyntax.ImplementStatically => ImplementStatically;
    }
}
