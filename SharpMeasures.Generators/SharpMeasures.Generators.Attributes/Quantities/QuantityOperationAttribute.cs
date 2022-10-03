namespace SharpMeasures.Generators;

using System;

/// <summary>Describes an operation { +, -, *, / } that may be applied to quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class QuantityOperationAttribute : Attribute
{
    /// <summary>The result of the operation.</summary>
    public Type Result { get; }
    /// <summary>The other quantity in the operation.</summary>
    public Type Other { get; }

    /// <summary>The operator type.</summary>
    public OperatorType OperatorType { get; }
    /// <summary>The position of this quantity in the operation. By default, the position will be <see cref="OperatorPosition.Left"/>.</summary>
    public OperatorPosition Position { get; }

    /// <summary>Dictates whether the mirrored operation is also implemented. The default behaviour is <see langword="true"/> if <see cref="OperatorType"/> is <see cref="OperatorType.Addition"/> or <see cref="OperatorType.Multiplication"/>, and <see langword="false"/> otherwise.</summary>
    public bool Mirror { get; init; }

    /// <summary>Describes how the operator is implemented. The default behaviour is <see cref="QuantityOperationImplementation.OperatorAndMethod"/>.</summary>
    public QuantityOperationImplementation Implementation { get; init; }

    /// <summary>The name of the method, if one is implemented. By default, the name will be <i>Add</i>, <i>Subtract</i>, <i>SubtractFrom</i>, <i>Multiply</i>, <i>Divide</i>, or <i>DivideInto</i> - depending on <see cref="OperatorType"/> and <see cref="Position"/>.</summary>
    public string MethodName { get; init; } = string.Empty;
    /// <summary>The name of the mirrored method, if applicable. By default, the name will be <i>Subtract</i>, <i>SubtractFrom</i>, <i>Divide</i>, or <i>DivideInto</i> - depending on <see cref="OperatorType"/> and <see cref="Position"/>.</summary>
    public string MirroredMethodName { get; init; } = string.Empty;

    /// <inheritdoc cref="QuantityOperationAttribute"/>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="other"><inheritdoc cref="Other" path="/summary"/><para><inheritdoc cref="Other" path="/remarks"/></para></param>
    /// <param name="operatorType"><inheritdoc cref="OperatorType" path="/summary"/><para><inheritdoc cref="OperatorType" path="/remarks"/></para></param>
    /// <param name="position"><inheritdoc cref="Position" path="/summary"/><para><inheritdoc cref="Position" path="/remarks"/></para></param>
    public QuantityOperationAttribute(Type result, Type other, OperatorType operatorType, OperatorPosition position)
    {
        Result = result;
        Other = other;

        Position = position;
        OperatorType = operatorType;
    }

    /// <inheritdoc cref="QuantityOperationAttribute"/>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="other"><inheritdoc cref="Other" path="/summary"/><para><inheritdoc cref="Other" path="/remarks"/></para></param>
    /// <param name="operatorType"><inheritdoc cref="OperatorType" path="/summary"/><para><inheritdoc cref="OperatorType" path="/remarks"/></para></param>
    public QuantityOperationAttribute(Type result, Type other, OperatorType operatorType) : this(result, other, operatorType, OperatorPosition.Left) { }
}
