namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, describing the default unit of the quantity - used when formatting the quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class DefaultUnitAttribute : Attribute
{
    /// <summary>The name of the default unit instance.</summary>
    public string Unit { get; }

    /// <summary>The symbol representing the default unit instance.</summary>
    public string Symbol { get; }

    /// <inheritdoc cref="DefaultUnitAttribute"/>
    /// <param name="unit"><inheritdoc cref="Unit" path="/summary"/></param>
    /// <param name="symbol"><inheritdoc cref="Symbol" path="/summary"/></param>
    public DefaultUnitAttribute(string unit, string symbol)
    {
        Unit = unit;
        Symbol = symbol;
    }
}
