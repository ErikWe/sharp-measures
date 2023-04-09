namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, describing a custom read-only property implemented by the quantity.</summary>
/// <typeparam name="TResult">The resulting type of the property.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityPropertyAttribute<TResult> : Attribute
{
    /// <summary>The name of the property.</summary>
    public string Name { get; }

    /// <summary>The expression describing the property.</summary>
    public string Expression { get; }

    /// <summary>Indicates that the property should be implemented statically. The default behaviour is <see langword="false"/>.</summary>
    public bool ImplementStatically { get; init; }

    /// <inheritdoc cref="QuantityPropertyAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    public QuantityPropertyAttribute(string name, string expression)
    {
        Name = name;
        Expression = expression;
    }
}
