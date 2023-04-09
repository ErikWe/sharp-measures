namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, describing a custom process implemented by the quantity.</summary>
/// <typeparam name="TResult">The type that is the result of the process.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityProcessAttribute<TResult> : Attribute
{
    /// <summary>The name of the process.</summary>
    public string Name { get; }

    /// <summary>The expression describing the process.</summary>
    public string Expression { get; }

    /// <summary>The signature of the process.</summary>
    public Type[] Signature { get; }

    /// <summary>The names of the parameters of the process. By default, the name of each parameter will be derived from the type of the parameter.</summary>
    public string[] ParameterNames { get; }

    /// <summary>Indicates that the process should be implemented statically. The default behaviour is <see langword="false"/>.</summary>
    public bool ImplementStatically { get; init; }

    /// <inheritdoc cref="QuantityProcessAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    public QuantityProcessAttribute(string name, string expression)
    {
        Name = name;
        Expression = expression;

        Signature = Array.Empty<Type>();
        ParameterNames = Array.Empty<string>();
    }

    /// <inheritdoc cref="QuantityProcessAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    public QuantityProcessAttribute(string name, string expression, Type[] signature)
    {
        Name = name;
        Expression = expression;

        Signature = signature;
        ParameterNames = Array.Empty<string>();
    }

    /// <inheritdoc cref="QuantityProcessAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    /// <param name="parameterNames"><inheritdoc cref="ParameterNames" path="/summary"/></param>
    public QuantityProcessAttribute(string name, string expression, Type[] signature, string[] parameterNames)
    {
        Name = name;
        Expression = expression;

        Signature = signature;
        ParameterNames = parameterNames;
    }
}
