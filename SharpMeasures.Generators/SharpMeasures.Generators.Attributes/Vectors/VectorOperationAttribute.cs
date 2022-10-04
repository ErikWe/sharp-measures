namespace SharpMeasures.Generators;

using System;

/// <summary>Describes an operation { ⋅, ⨯ } that may be applied to vector quantities.</summary>
/// <remarks>For the operators { +, -, *, / }, use <see cref="QuantityOperationAttribute"/>.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class VectorOperationAttribute : Attribute
{
    /// <summary>The result of the operation.</summary>
    public Type Result { get; }
    /// <summary>The other quantity in the operation.</summary>
    public Type Other { get; }

    /// <summary>The operator type.</summary>
    public VectorOperatorType OperatorType { get; }
    /// <summary>The position of this quantity in the operation. By default, the position will be <see cref="OperatorPosition.Left"/>.</summary>
    /// <remarks>This is only relevant when using <see cref="VectorOperatorType.Cross"/>, where the result will be scaled by { -1 } if <see cref="OperatorPosition.Right"/> is used.</remarks>
    public OperatorPosition Position { get; }

    /// <summary>Dictates whether the mirrored operation is also implemented. The default behaviour is <see langword="false"/>.</summary>
    /// <remarks>This is only relevant when using <see cref="VectorOperatorType.Cross"/>, where the result will be scaled by { -1 } when mirrored.</remarks>
    public bool Mirror { get; init; }

    /// <summary>The name of the operation. By default, the name will be <i>Dot</i>, <i>Cross</i>, or <i>CrossInto</i> - depending on <see cref="OperatorType"/> and <see cref="Position"/>.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>The name of the mirrored operation, if one is implemented. By default, the name will be <i>Cross</i> or <i>CrossInto</i> - depending on <see cref="OperatorType"/> and <see cref="Position"/>.</summary>
    public string MirroredName { get; init; } = string.Empty;

    /// <inheritdoc cref="VectorOperationAttribute"/>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="other"><inheritdoc cref="Other" path="/summary"/><para><inheritdoc cref="Other" path="/remarks"/></para></param>
    /// <param name="operatorType"><inheritdoc cref="OperatorType" path="/summary"/><para><inheritdoc cref="OperatorType" path="/remarks"/></para></param>
    /// <param name="position"><inheritdoc cref="Position" path="/summary"/><para><inheritdoc cref="Position" path="/remarks"/></para></param>
    public VectorOperationAttribute(Type result, Type other, VectorOperatorType operatorType, OperatorPosition position)
    {
        Result = result;
        Other = other;

        Position = position;
        OperatorType = operatorType;
    }

    /// <inheritdoc cref="VectorOperationAttribute"/>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="other"><inheritdoc cref="Other" path="/summary"/><para><inheritdoc cref="Other" path="/remarks"/></para></param>
    /// <param name="operatorType"><inheritdoc cref="OperatorType" path="/summary"/><para><inheritdoc cref="OperatorType" path="/remarks"/></para></param>
    public VectorOperationAttribute(Type result, Type other, VectorOperatorType operatorType) : this(result, other, operatorType, OperatorPosition.Left) { }
}
