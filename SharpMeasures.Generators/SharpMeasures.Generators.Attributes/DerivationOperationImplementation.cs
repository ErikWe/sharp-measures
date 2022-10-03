namespace SharpMeasures.Generators;

/// <summary>Describes how an operation is implemented.</summary>
public enum QuantityOperationImplementation
{
    /// <summary>The operation is not implemented.</summary>
    None,
    /// <summary>The operation is implemented both as an operator and a method.</summary>
    OperatorAndMethod,
    /// <summary>The operation is implemented as an operator.</summary>
    Operator,
    /// <summary>The operation is implemented as a method.</summary>
    Method
}
