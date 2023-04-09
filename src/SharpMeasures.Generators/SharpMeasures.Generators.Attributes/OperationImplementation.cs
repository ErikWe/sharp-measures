namespace SharpMeasures;

using System;

/// <summary>Describes how a quantity implements an operation.</summary>
[Flags]
public enum OperationImplementation
{
    /// <summary>Indicates that the operation is not implemented.</summary>
    None = 0,
    /// <summary>The operation is implemented as an instance method.</summary>
    InstanceMethod = 1,
    /// <summary>The operation is implemented as a static method.</summary>
    StaticMethod = 2,
    /// <summary>The operation is implemented as an operator.</summary>
    Operator = 4,
    /// <summary>The operation is implemented both as an operator and as a method.</summary>
    All = InstanceMethod | StaticMethod | Operator
}
