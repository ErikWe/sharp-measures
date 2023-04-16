namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityProcessAttribute{TResult}"/> to be parsed.</summary>
public sealed class QuantityProcessAttributeParser : IConstructiveSyntacticAttributeParser<IRawQuantityProcess>, IConstructiveSemanticAttributeParser<IRawQuantityProcess>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityProcessAttributeParser"/>, parsing the arguments of a <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityProcessAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawQuantityProcess? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        QuantityProcessAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawQuantityProcess? TryParse(AttributeData attributeData)
    {
        QuantityProcessAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawQuantityProcess? Create(QuantityProcessAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Result is null)
        {
            return null;
        }

        return new RawQuantityProcess(recorder.Result, recorder.Name, recorder.Expression, recorder.Signature, recorder.ParameterNames, recorder.ImplementStatically, CreateSyntax(recorder, parsingMode));
    }

    private static IQuantityProcessSyntax? CreateSyntax(QuantityProcessAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new QuantityProcessSyntax(recorder.ResultLocation, recorder.NameLocation, recorder.ExpressionLocation, recorder.SignatureCollectionLocation, recorder.SignatureElementLocations, recorder.ParameterNamesCollectionLocation, recorder.ParameterNamesElementLocations, recorder.ImplementStaticallyLocation);
    }

    private sealed class QuantityProcessAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Result { get; private set; }

        public string? Name { get; private set; }
        public string? Expression { get; private set; }
        public IReadOnlyList<ITypeSymbol?>? Signature { get; private set; }
        public IReadOnlyList<string?>? ParameterNames { get; private set; }
        public bool? ImplementStatically { get; private set; }

        public Location ResultLocation { get; private set; } = Location.None;

        public Location NameLocation { get; private set; } = Location.None;
        public Location ExpressionLocation { get; private set; } = Location.None;
        public Location SignatureCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> SignatureElementLocations { get; private set; } = Array.Empty<Location>();
        public Location ParameterNamesCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> ParameterNamesElementLocations { get; private set; } = Array.Empty<Location>();
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

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Signature", Adapters.ForNullable<ITypeSymbol>(RecordSignature));
            yield return ("ParameterNames", Adapters.ForNullable<string>(RecordParameterNames));
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

        private void RecordSignature(IReadOnlyList<ITypeSymbol?>? signature, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Signature = signature;
            SignatureCollectionLocation = collectionLocation;
            SignatureElementLocations = elementLocations;
        }

        private void RecordParameterNames(IReadOnlyList<string?>? parameterNames, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            ParameterNames = parameterNames;
            ParameterNamesCollectionLocation = collectionLocation;
            ParameterNamesElementLocations = elementLocations;
        }

        private void RecordImplementStatically(bool implementStatically, Location location)
        {
            ImplementStatically = implementStatically;
            ImplementStaticallyLocation = location;
        }
    }

    private sealed record class RawQuantityProcess : IRawQuantityProcess
    {
        private ITypeSymbol Result { get; }
        private string? Name { get; }
        private string? Expression { get; }
        private IReadOnlyList<ITypeSymbol?>? Signature { get; }
        private IReadOnlyList<string?>? ParameterNames { get; }
        private bool? ImplementStatically { get; }

        private IQuantityProcessSyntax? Syntax { get; }

        public RawQuantityProcess(ITypeSymbol result, string? name, string? expression, IReadOnlyList<ITypeSymbol?>? signature, IReadOnlyList<string?>? parameterNames, bool? implementStatically, IQuantityProcessSyntax? syntax)
        {
            Result = result;
            Name = name;
            Expression = expression;
            Signature = signature;
            ParameterNames = parameterNames;
            ImplementStatically = implementStatically;

            Syntax = syntax;
        }

        ITypeSymbol IRawQuantityProcess.Result => Result;
        string? IRawQuantityProcess.Name => Name;
        string? IRawQuantityProcess.Expression => Expression;
        IReadOnlyList<ITypeSymbol?>? IRawQuantityProcess.Signature => Signature;
        IReadOnlyList<string?>? IRawQuantityProcess.ParameterNames => ParameterNames;
        bool? IRawQuantityProcess.ImplementStatically => ImplementStatically;

        IQuantityProcessSyntax? IRawQuantityProcess.Syntax => Syntax;
    }

    private sealed record class QuantityProcessSyntax : IQuantityProcessSyntax
    {
        private Location Result { get; }
        private Location Name { get; }
        private Location Expression { get; }
        private Location SignatureCollection { get; }
        private IReadOnlyList<Location> SignatureElements { get; }
        private Location ParameterNamesCollection { get; }
        private IReadOnlyList<Location> ParameterNamesElements { get; }
        private Location ImplementStatically { get; }

        public QuantityProcessSyntax(Location result, Location name, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements, Location parameterNamesCollection, IReadOnlyList<Location> parameterNamesElements, Location implementStatically)
        {
            Result = result;
            Name = name;
            Expression = expression;
            SignatureCollection = signatureCollection;
            SignatureElements = signatureElements;
            ParameterNamesCollection = parameterNamesCollection;
            ParameterNamesElements = parameterNamesElements;
            ImplementStatically = implementStatically;
        }

        Location IQuantityProcessSyntax.Result => Result;
        Location IQuantityProcessSyntax.Name => Name;
        Location IQuantityProcessSyntax.Expression => Expression;
        Location IQuantityProcessSyntax.SignatureCollection => SignatureCollection;
        IReadOnlyList<Location> IQuantityProcessSyntax.SignatureElements => SignatureElements;
        Location IQuantityProcessSyntax.ParameterNamesCollection => ParameterNamesCollection;
        IReadOnlyList<Location> IQuantityProcessSyntax.ParameterNamesElements => ParameterNamesElements;
        Location IQuantityProcessSyntax.ImplementStatically => ImplementStatically;
    }
}
