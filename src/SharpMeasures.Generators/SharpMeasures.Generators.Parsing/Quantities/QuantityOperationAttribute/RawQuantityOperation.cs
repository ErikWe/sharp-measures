namespace SharpMeasures.Generators.Parsing.Quantities.QuantityOperationAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawQuantityOperation"/>
internal sealed record class RawQuantityOperation : IRawQuantityOperation
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

    /// <summary>Instantiates a <see cref="RawQuantityOperation"/>, representing a parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="result"><inheritdoc cref="IRawQuantityOperation.Result" path="/summary"/></param>
    /// <param name="other"><inheritdoc cref="IRawQuantityOperation.Other" path="/summary"/></param>
    /// <param name="operatorType"><inheritdoc cref="IRawQuantityOperation.OperatorType" path="/summary"/></param>
    /// <param name="position"><inheritdoc cref="IRawQuantityOperation.Position" path="/summary"/></param>
    /// <param name="mirrorMode"><inheritdoc cref="IRawQuantityOperation.MirrorMode" path="/summary"/></param>
    /// <param name="implementation"><inheritdoc cref="IRawQuantityOperation.Implementation" path="/summary"/></param>
    /// <param name="mirroredImplementation"><inheritdoc cref="IRawQuantityOperation.MirroredImplementation" path="/summary"/></param>
    /// <param name="methodName"><inheritdoc cref="IRawQuantityOperation.MethodName" path="/summary"/></param>
    /// <param name="staticMethodName"><inheritdoc cref="IRawQuantityOperation.StaticMethodName" path="/summary"/></param>
    /// <param name="mirroredMethodName"><inheritdoc cref="IRawQuantityOperation.MirroredMethodName" path="/summary"/></param>
    /// <param name="mirroredStaticMethodName"><inheritdoc cref="IRawQuantityOperation.MirroredStaticMethodName" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawQuantityOperation.Syntax" path="/summary"/></param>
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
