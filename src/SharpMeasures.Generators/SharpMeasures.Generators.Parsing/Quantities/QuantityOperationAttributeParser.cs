namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;
using System.Collections.Generic;

/// <summary>Allows the arguments of a <see cref="QuantityOperationAttribute{TResult, TOther}"/> to be parsed.</summary>
public sealed class QuantityOperationAttributeParser : IConstructiveSyntacticAttributeParser<IRawQuantityOperation>, IConstructiveSemanticAttributeParser<IRawQuantityOperation>
{
    private ISyntacticAttributeParser SyntacticParser { get; }
    private ISemanticAttributeParser SemanticParser { get; }

    /// <summary>Instantiates a <see cref="QuantityOperationAttributeParser"/>, parsing the arguments of a <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="syntacticParser"><inheritdoc cref="ISyntacticAttributeParser" path="/summary"/></param>
    /// <param name="semanticParser"><inheritdoc cref="ISemanticAttributeParser" path="/summary"/></param>
    /// <exception cref="ArgumentNullException"/>
    public QuantityOperationAttributeParser(ISyntacticAttributeParser syntacticParser, ISemanticAttributeParser semanticParser)
    {
        SyntacticParser = syntacticParser ?? throw new ArgumentNullException(nameof(syntacticParser));
        SemanticParser = semanticParser ?? throw new ArgumentNullException(nameof(semanticParser));
    }

    /// <inheritdoc/>
    public IRawQuantityOperation? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        QuantityOperationAttributeArgumentRecorder recorder = new();

        SyntacticParser.TryParse(recorder, attributeData, attributeSyntax);

        return Create(recorder, AttributeParsingMode.Syntactically);
    }

    /// <inheritdoc/>
    public IRawQuantityOperation? TryParse(AttributeData attributeData)
    {
        QuantityOperationAttributeArgumentRecorder recorder = new();

        if (SemanticParser.TryParse(recorder, attributeData) is false)
        {
            return null;
        }

        return Create(recorder, AttributeParsingMode.Semantically);
    }

    private static IRawQuantityOperation? Create(QuantityOperationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (recorder.Result is null || recorder.Other is null || recorder.OperatorType is null)
        {
            return null;
        }

        return new RawQuantityOperation(recorder.Result, recorder.Other, recorder.OperatorType.Value, recorder.Position, recorder.MirrorMode, recorder.Implementation, recorder.MirroredImplementation, recorder.MethodName,
            recorder.StaticMethodName, recorder.MirroredMethodName, recorder.MirroredStaticMethodName, CreateSyntax(recorder, parsingMode));
    }

    private static IQuantityOperationSyntax? CreateSyntax(QuantityOperationAttributeArgumentRecorder recorder, AttributeParsingMode parsingMode)
    {
        if (parsingMode is AttributeParsingMode.Semantically)
        {
            return null;
        }

        return new QuantityOperationSyntax(recorder.ResultLocation, recorder.OtherLocation, recorder.OperatorTypeLocation, recorder.PositionLocation, recorder.MirrorModeLocation, recorder.ImplementationLocation,
            recorder.MirroredImplementationLocation, recorder.MethodNameLocation, recorder.StaticMethodNameLocation, recorder.MirroredMethodNameLocation, recorder.MirroredStaticMethodNameLocation);

    }

    private sealed class QuantityOperationAttributeArgumentRecorder : AArgumentRecorder
    {
        public ITypeSymbol? Result { get; private set; }
        public ITypeSymbol? Other { get; private set; }

        public OperatorType? OperatorType { get; private set; }
        public OperationPosition? Position { get; private set; }
        public OperationMirrorMode? MirrorMode { get; private set; }
        public OperationImplementation? Implementation { get; private set; }
        public OperationImplementation? MirroredImplementation { get; private set; }

        public string? MethodName { get; private set; }
        public string? StaticMethodName { get; private set; }
        public string? MirroredMethodName { get; private set; }
        public string? MirroredStaticMethodName { get; private set; }

        public Location ResultLocation { get; private set; } = Location.None;
        public Location OtherLocation { get; private set; } = Location.None;

        public Location OperatorTypeLocation { get; private set; } = Location.None;
        public Location PositionLocation { get; private set; } = Location.None;
        public Location MirrorModeLocation { get; private set; } = Location.None;
        public Location ImplementationLocation { get; private set; } = Location.None;
        public Location MirroredImplementationLocation { get; private set; } = Location.None;

        public Location MethodNameLocation { get; private set; } = Location.None;
        public Location StaticMethodNameLocation { get; private set; } = Location.None;
        public Location MirroredMethodNameLocation { get; private set; } = Location.None;
        public Location MirroredStaticMethodNameLocation { get; private set; } = Location.None;

        protected override IEnumerable<(string, DSyntacticGenericRecorder)> AddGenericRecorders()
        {
            yield return ("TResult", Adapters.For(RecordResult));
            yield return ("TOther", Adapters.For(RecordOther));
        }

        protected override IEnumerable<(string, DSyntacticSingleRecorder)> AddSingleRecorders()
        {
            yield return ("OperatorType", Adapters.For<OperatorType>(RecordOperatorType));
            yield return ("Position", Adapters.For<OperationPosition>(RecordPosition));
            yield return ("MirrorMode", Adapters.For<OperationMirrorMode>(RecordMirrorMode));
            yield return ("Implementation", Adapters.For<OperationImplementation>(RecordImplementation));
            yield return ("MirroredImplementation", Adapters.For<OperationImplementation>(RecordMirroredImplementation));
            yield return ("MethodName", Adapters.ForNullable<string>(RecordMethodName));
            yield return ("StaticMethodName", Adapters.ForNullable<string>(RecordStaticMethodName));
            yield return ("MirroredMethodName", Adapters.ForNullable<string>(RecordMirroredMethodName));
            yield return ("MirroredStaticMethodName", Adapters.ForNullable<string>(RecordMirroredStaticMethodName));
        }

        private void RecordResult(ITypeSymbol result, Location location)
        {
            Result = result;
            ResultLocation = location;
        }

        private void RecordOther(ITypeSymbol other, Location location)
        {
            Other = other;
            OtherLocation = location;
        }

        private void RecordOperatorType(OperatorType operatorType, Location location)
        {
            OperatorType = operatorType;
            OperatorTypeLocation = location;
        }

        private void RecordPosition(OperationPosition position, Location location)
        {
            Position = position;
            PositionLocation = location;
        }

        private void RecordMirrorMode(OperationMirrorMode mirrorMode, Location location)
        {
            MirrorMode = mirrorMode;
            MirrorModeLocation = location;
        }

        private void RecordImplementation(OperationImplementation implementation, Location location)
        {
            Implementation = implementation;
            ImplementationLocation = location;
        }

        private void RecordMirroredImplementation(OperationImplementation mirroredImplementation, Location location)
        {
            MirroredImplementation = mirroredImplementation;
            MirroredImplementationLocation = location;
        }

        private void RecordMethodName(string? methodName, Location location)
        {
            if (methodName is not null)
            {
                MethodName = methodName;
            }

            MethodNameLocation = location;
        }

        private void RecordStaticMethodName(string? staticMethodName, Location location)
        {
            if (staticMethodName is not null)
            {
                StaticMethodName = staticMethodName;
            }

            StaticMethodNameLocation = location;
        }

        private void RecordMirroredMethodName(string? mirroredMethodName, Location location)
        {
            if (mirroredMethodName is not null)
            {
                MirroredMethodName = mirroredMethodName;
            }

            MirroredMethodNameLocation = location;
        }

        private void RecordMirroredStaticMethodName(string? mirroredStaticMethodName, Location location)
        {
            if (mirroredStaticMethodName is not null)
            {
                MirroredStaticMethodName = mirroredStaticMethodName;
            }

            MirroredStaticMethodNameLocation = location;
        }
    }

    private sealed record class RawQuantityOperation : IRawQuantityOperation
    {
        private ITypeSymbol Result { get; }
        private ITypeSymbol Other { get; }

        private OperatorType OperatorType { get; }
        private OperationPosition? Position { get; }
        private OperationMirrorMode? MirrorMode { get; }
        private OperationImplementation? Implementation { get; }
        private OperationImplementation? MirroredImplementation { get; }

        private string? MethodName { get; }
        private string? StaticMethodName { get; }
        private string? MirroredMethodName { get; }
        private string? MirroredStaticMethodName { get; }

        private IQuantityOperationSyntax? Syntax { get; }

        public RawQuantityOperation(ITypeSymbol result, ITypeSymbol other, OperatorType operatorType, OperationPosition? position, OperationMirrorMode? mirrorMode, OperationImplementation? implementation,
            OperationImplementation? mirroredImplementation, string? methodName, string? staticMethodName, string? mirroredMethodName, string? mirroredStaticMethodName, IQuantityOperationSyntax? syntax)
        {
            Result = result;
            Other = other;

            OperatorType = operatorType;
            Position = position;
            MirrorMode = mirrorMode;
            Implementation = implementation;
            MirroredImplementation = mirroredImplementation;

            MethodName = methodName;
            StaticMethodName = staticMethodName;
            MirroredMethodName = mirroredMethodName;
            MirroredStaticMethodName = mirroredStaticMethodName;

            Syntax = syntax;
        }

        ITypeSymbol IRawQuantityOperation.Result => Result;
        ITypeSymbol IRawQuantityOperation.Other => Other;

        OperatorType IRawQuantityOperation.OperatorType => OperatorType;
        OperationPosition? IRawQuantityOperation.Position => Position;
        OperationMirrorMode? IRawQuantityOperation.MirrorMode => MirrorMode;
        OperationImplementation? IRawQuantityOperation.Implementation => Implementation;
        OperationImplementation? IRawQuantityOperation.MirroredImplementation => MirroredImplementation;

        string? IRawQuantityOperation.MethodName => MethodName;
        string? IRawQuantityOperation.StaticMethodName => StaticMethodName;
        string? IRawQuantityOperation.MirroredMethodName => MirroredMethodName;
        string? IRawQuantityOperation.MirroredStaticMethodName => MirroredStaticMethodName;

        IQuantityOperationSyntax? IRawQuantityOperation.Syntax => Syntax;
    }

    private sealed record class QuantityOperationSyntax : IQuantityOperationSyntax
    {
        private Location Result { get; }
        private Location Other { get; }

        private Location OperatorType { get; }
        private Location Position { get; }
        private Location MirrorMode { get; }
        private Location Implementation { get; }
        private Location MirroredImplementation { get; }

        private Location MethodName { get; }
        private Location StaticMethodName { get; }
        private Location MirroredMethodName { get; }
        private Location MirroredStaticMethodName { get; }

        public QuantityOperationSyntax(Location result, Location other, Location operatorType, Location position, Location mirrorMode, Location implementation, Location mirroredImplementation, Location methodName,
            Location staticMethodName, Location mirroredMethodName, Location mirroredStaticMethodName)
        {
            Result = result;
            Other = other;

            OperatorType = operatorType;
            Position = position;
            MirrorMode = mirrorMode;
            Implementation = implementation;
            MirroredImplementation = mirroredImplementation;

            MethodName = methodName;
            StaticMethodName = staticMethodName;
            MirroredMethodName = mirroredMethodName;
            MirroredStaticMethodName = mirroredStaticMethodName;
        }

        Location IQuantityOperationSyntax.Result => Result;
        Location IQuantityOperationSyntax.Other => Other;

        Location IQuantityOperationSyntax.OperatorType => OperatorType;
        Location IQuantityOperationSyntax.Position => Position;
        Location IQuantityOperationSyntax.MirrorMode => MirrorMode;
        Location IQuantityOperationSyntax.Implementation => Implementation;
        Location IQuantityOperationSyntax.MirroredImplementation => MirroredImplementation;

        Location IQuantityOperationSyntax.MethodName => MethodName;
        Location IQuantityOperationSyntax.StaticMethodName => StaticMethodName;
        Location IQuantityOperationSyntax.MirroredMethodName => MirroredMethodName;
        Location IQuantityOperationSyntax.MirroredStaticMethodName => MirroredStaticMethodName;
    }
}
