namespace SharpMeasures.Generators;

using System;

/// <summary>Describes how a quantity implements an operation.</summary>
[Flags]
public enum QuantityOperationImplementation
{
    /// <summary>Indicates that the operation is not implemented.</summary>
    None = 0,
    /// <summary>The operation is implemented as an operator.</summary>
    Operator = 1,
    /// <summary>The operation is implemented as a method.</summary>
    Method = 2,
    /// <summary>The operation is implemented both as an operator and as a method.</summary>
    OperatorAndMethod = Operator | Method
}
