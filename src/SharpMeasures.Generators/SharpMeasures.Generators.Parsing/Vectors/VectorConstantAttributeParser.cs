namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="VectorConstantAttribute"/> to be parsed.</summary>
public sealed class VectorConstantAttributeParser : IConstructiveSemanticAttributeParser<IRawVectorConstant>, IConstructiveSyntacticAttributeParser<IRawVectorConstant>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="VectorConstantAttributeParser"/>, parsing the arguments of a <see cref="VectorConstantAttribute"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public VectorConstantAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawVectorConstant? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        VectorConstantAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawVectorConstant? TryParse(AttributeData attributeData)
    {
        VectorConstantAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawVectorConstant? Create(VectorConstantAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        return new RawVectorConstant(recorder.Name, recorder.UnitInstanceName, recorder.Value, CreateSyntax(recorder, parsingMode));
    }

    private static IVectorConstantSyntax? CreateSyntax(VectorConstantAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new VectorConstantSyntax(recorder.NameLocation, recorder.UnitInstanceNameLocation, recorder.ValueCollectionLocation, recorder.ValueElementLocations);
    }

    private sealed class VectorConstantAttributeArgumentRecorder : AArgumentRecorder
    {
        public string? Name { get; private set; }
        public string? UnitInstanceName { get; private set; }
        public OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; private set; }

        public Location NameLocation { get; private set; } = Location.None;
        public Location UnitInstanceNameLocation { get; private set; } = Location.None;
        public Location ValueCollectionLocation { get; private set; } = Location.None;
        public IReadOnlyList<Location> ValueElementLocations { get; private set; } = Array.Empty<Location>();

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("Name", Adapters.ForNullable<string>(RecordName));
            yield return ("UnitInstanceName", Adapters.ForNullable<string>(RecordUnitInstanceName));
        }

        protected override IEnumerable<(string, DSyntacticArrayRecorder)> AddArrayRecorders()
        {
            yield return ("Value", Adapters.ForNullableCollection<double>(RecordValue));
            yield return ("Expressions", Adapters.ForNullable<string>(RecordExpressions));
        }

        private void RecordName(string? name, Location location)
        {
            Name = name;
            NameLocation = location;
        }

        private void RecordUnitInstanceName(string? unitInstanceName, Location location)
        {
            UnitInstanceName = unitInstanceName;
            UnitInstanceNameLocation = location;
        }

        private void RecordValue(IReadOnlyList<double>? value, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Value = OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>.FromT0(value);

            ValueCollectionLocation = collectionLocation;
            ValueElementLocations = elementLocations;
        }

        private void RecordExpressions(IReadOnlyList<string?>? expressions, Location collectionLocation, IReadOnlyList<Location> elementLocations)
        {
            Value = OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>.FromT1(expressions);

            ValueCollectionLocation = collectionLocation;
            ValueElementLocations = elementLocations;
        }
    }

    private sealed record class RawVectorConstant : IRawVectorConstant
    {
        private string? Name { get; }
        private string? UnitInstanceName { get; }
        private OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; }

        private IVectorConstantSyntax? Syntax { get; }

        public RawVectorConstant(string? name, string? unitInstanceName, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> value, IVectorConstantSyntax? syntax)
        {
            Name = name;
            UnitInstanceName = unitInstanceName;
            Value = value;

            Syntax = syntax;
        }

        string? IRawVectorConstant.Name => Name;
        string? IRawVectorConstant.UnitInstanceName => UnitInstanceName;
        OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> IRawVectorConstant.Value => Value;

        IVectorConstantSyntax? IRawVectorConstant.Syntax => Syntax;
    }

    private sealed record class VectorConstantSyntax : IVectorConstantSyntax
    {
        private Location Name { get; }
        private Location UnitInstanceName { get; }
        private Location ValueCollection { get; }
        private IReadOnlyList<Location> ValueElements { get; }

        /// <summary>Instantiates a <see cref="VectorConstantSyntax"/>, representing syntactical information about a parsed <see cref="VectorConstantAttribute"/>.</summary>
        /// <param name="name"><inheritdoc cref="IVectorConstantSyntax.Name" path="/summary"/></param>
        /// <param name="unitInstanceName"><inheritdoc cref="IVectorConstantSyntax.UnitInstanceName" path="/summary"/></param>
        /// <param name="valueCollection"><inheritdoc cref="IVectorConstantSyntax.ValueCollection" path="/summary"/></param>
        /// <param name="valueElements"><inheritdoc cref="IVectorConstantSyntax.ValueElements" path="/summary"/></param>
        public VectorConstantSyntax(Location name, Location unitInstanceName, Location valueCollection, IReadOnlyList<Location> valueElements)
        {
            Name = name;
            UnitInstanceName = unitInstanceName;
            ValueCollection = valueCollection;
            ValueElements = valueElements;
        }

        Location IVectorConstantSyntax.Name => Name;
        Location IVectorConstantSyntax.UnitInstanceName => UnitInstanceName;
        Location IVectorConstantSyntax.ValueCollection => ValueCollection;
        IReadOnlyList<Location> IVectorConstantSyntax.ValueElements => ValueElements;
    }
}
