namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="ScalarQuantityAttribute{TUnit}"/> to be parsed.</summary>
public sealed class ScalarQuantityAttributeParser : IConstructiveSyntacticAttributeParser<IRawScalarQuantity>, IConstructiveSemanticAttributeParser<IRawScalarQuantity>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="ScalarQuantityAttributeParser"/>, parsing the arguments of a <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public ScalarQuantityAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawScalarQuantity? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        ScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SyntacticParser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawScalarQuantity? TryParse(AttributeData attributeData)
    {
        ScalarQuantityAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawScalarQuantity? Create(ScalarQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Unit is null)
        {
            return null;
        }

        return new RawScalarQuantity(recorder.Unit, recorder.AllowNegative, recorder.UseUnitBias, recorder.ImplementSum, recorder.ImplementDifference, CreateSyntax(recorder, parsingMode));
    }

    private static IScalarQuantitySyntax? CreateSyntax(ScalarQuantityAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new ScalarQuantitySyntax(recorder.UnitLocation, recorder.AllowNegativeLocation, recorder.UseUnitBiasLocation, recorder.ImplementSumLocation, recorder.ImplementDifferenceLocation);
    }

    private sealed class ScalarQuantityAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Unit { get; private set; }
        public bool? AllowNegative { get; private set; }
        public bool? UseUnitBias { get; private set; }
        public bool? ImplementSum { get; private set; }
        public bool? ImplementDifference { get; private set; }

        public Location UnitLocation { get; private set; } = Location.None;
        public Location AllowNegativeLocation { get; private set; } = Location.None;
        public Location UseUnitBiasLocation { get; private set; } = Location.None;
        public Location ImplementSumLocation { get; private set; } = Location.None;
        public Location ImplementDifferenceLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TUnit", Adapters.For(RecordUnit));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("AllowNegative", Adapters.For<bool>(RecordAllowNegative));
            yield return ("UseUnitBias", Adapters.For<bool>(RecordUseUnitBias));
            yield return ("ImplementSum", Adapters.For<bool>(RecordImplementSum));
            yield return ("ImplementDifference", Adapters.For<bool>(RecordImplementDifference));
        }

        private void RecordUnit(ITypeSymbol unit, Location location)
        {
            Unit = unit;
            UnitLocation = location;
        }

        private void RecordAllowNegative(bool allowNegative, Location location)
        {
            AllowNegative = allowNegative;
            AllowNegativeLocation = location;
        }

        private void RecordUseUnitBias(bool useUnitBias, Location location)
        {
            UseUnitBias = useUnitBias;
            UseUnitBiasLocation = location;
        }

        private void RecordImplementSum(bool implementSum, Location location)
        {
            ImplementSum = implementSum;
            ImplementSumLocation = location;
        }

        private void RecordImplementDifference(bool implementDifference, Location location)
        {
            ImplementDifference = implementDifference;
            ImplementDifferenceLocation = location;
        }
    }

    private sealed record class RawScalarQuantity : IRawScalarQuantity
    {
        private ITypeSymbol Unit { get; }
        private bool? AllowNegative { get; }
        private bool? UseUnitBias { get; }
        private bool? ImplementSum { get; }
        private bool? ImplementDifference { get; }

        private IScalarQuantitySyntax? Syntax { get; }

        public RawScalarQuantity(ITypeSymbol unit, bool? allowNegative, bool? useUnitBias, bool? implementSum, bool? implementDifference, IScalarQuantitySyntax? syntax)
        {
            Unit = unit;
            AllowNegative = allowNegative;
            UseUnitBias = useUnitBias;
            ImplementSum = implementSum;
            ImplementDifference = implementDifference;

            Syntax = syntax;
        }

        ITypeSymbol IRawScalarQuantity.Unit => Unit;
        bool? IRawScalarQuantity.AllowNegative => AllowNegative;
        bool? IRawScalarQuantity.UseUnitBias => UseUnitBias;
        bool? IRawScalarQuantity.ImplementSum => ImplementSum;
        bool? IRawScalarQuantity.ImplementDifference => ImplementDifference;

        IScalarQuantitySyntax? IRawScalarQuantity.Syntax => Syntax;
    }

    private sealed record class ScalarQuantitySyntax : IScalarQuantitySyntax
    {
        private Location Unit { get; }
        private Location AllowNegative { get; }
        private Location UseUnitBias { get; }
        private Location ImplementSum { get; }
        private Location ImplementDifference { get; }

        public ScalarQuantitySyntax(Location unit, Location allowNegative, Location useUnitBias, Location implementSum, Location implementDifference)
        {
            Unit = unit;
            AllowNegative = allowNegative;
            UseUnitBias = useUnitBias;
            ImplementSum = implementSum;
            ImplementDifference = implementDifference;
        }

        Location IScalarQuantitySyntax.Unit => Unit;
        Location IScalarQuantitySyntax.AllowNegative => AllowNegative;
        Location IScalarQuantitySyntax.UseUnitBias => UseUnitBias;
        Location IScalarQuantitySyntax.ImplementSum => ImplementSum;
        Location IScalarQuantitySyntax.ImplementDifference => ImplementDifference;
    }
}
