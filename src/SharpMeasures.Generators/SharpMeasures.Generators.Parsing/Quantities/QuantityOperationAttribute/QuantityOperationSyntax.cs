namespace SharpMeasures.Generators.Parsing.Quantities.QuantityOperationAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IQuantityOperationSyntax"/>
internal sealed record class QuantityOperationSyntax : IQuantityOperationSyntax
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

    /// <summary>Instantiates a <see cref="QuantityOperationSyntax"/>, representing syntactical information about a parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="result"><inheritdoc cref="IQuantityOperationSyntax.Result" path="/summary"/></param>
    /// <param name="other"><inheritdoc cref="IQuantityOperationSyntax.Other" path="/summary"/></param>
    /// <param name="operatorType"><inheritdoc cref="IQuantityOperationSyntax.OperatorType" path="/summary"/></param>
    /// <param name="position"><inheritdoc cref="IQuantityOperationSyntax.Position" path="/summary"/></param>
    /// <param name="mirrorMode"><inheritdoc cref="IQuantityOperationSyntax.MirrorMode" path="/summary"/></param>
    /// <param name="implementation"><inheritdoc cref="IQuantityOperationSyntax.Implementation" path="/summary"/></param>
    /// <param name="mirroredImplementation"><inheritdoc cref="IQuantityOperationSyntax.MirroredImplementation" path="/summary"/></param>
    /// <param name="methodName"><inheritdoc cref="IQuantityOperationSyntax.MethodName" path="/summary"/></param>
    /// <param name="staticMethodName"><inheritdoc cref="IQuantityOperationSyntax.StaticMethodName" path="/summary"/></param>
    /// <param name="mirroredMethodName"><inheritdoc cref="IQuantityOperationSyntax.MirroredMethodName" path="/summary"/></param>
    /// <param name="mirroredStaticMethodName"><inheritdoc cref="IQuantityOperationSyntax.MirroredStaticMethodName" path="/summary"/></param>
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
