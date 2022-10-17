namespace SharpMeasures.Generators;

using System;

/// <summary>Describes an operation { + , - , ⋅ , ÷ } supported by the quantity.</summary>
/// <remarks>See <see cref="VectorOperationAttribute"/> for operations limited to vector quantities.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class QuantityOperationAttribute : Attribute
{
    /// <summary>The quantity that is the result of the operation.</summary>
    public Type Result { get; }
    /// <summary>The other quantity in the operation.</summary>
    public Type Other { get; }

    /// <summary>The operator that is applied to the quantities.</summary>
    public OperatorType OperatorType { get; }
    /// <summary>The position of this quantity in the operation. By default, the position will be <see cref="OperatorPosition.Left"/>.</summary>
    public OperatorPosition Position { get; }

    /// <summary>Dictates whether the mirrored operation is also implemented. The default behaviour is <see langword="false"/>, unless <see cref="Other"/> is defined in another assembly and <see cref="OperatorType"/>
    /// is <see cref="OperatorType.Addition"/> or <see cref="OperatorType.Multiplication"/>.</summary>
    /// <remarks>Using the default behaviour reduces the likelihood that two separate quantities defines the same operator, which would result in ambiguous invokations.</remarks>
    public bool Mirror { get; init; }

    /// <summary>Dictates how the operator is implemented. The default behaviour is <see cref="QuantityOperationImplementation.OperatorAndMethod"/>.</summary>
    public QuantityOperationImplementation Implementation { get; init; }

    /// <summary>The name of the method, if one is implemented. By default, the name will be one of { <i>Add</i> , <i>Subtract</i> , <i>SubtractFrom</i> , <i>Multiply</i> , <i>Divide</i> , <i>DivideInto</i> } - depending on <see cref="OperatorType"/> and <see cref="Position"/>.</summary>
    public string MethodName { get; init; } = string.Empty;
    /// <summary>The name of the mirrored method, if applicable. By default, the name will be one of { <i>Subtract</i>, <i>SubtractFrom</i>, <i>Divide</i>, <i>DivideInto</i> } - depending on <see cref="OperatorType"/> and <see cref="Position"/>.</summary>
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
