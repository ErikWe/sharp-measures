namespace SharpMeasures.Generators;

using System;

/// <summary>Describes a process associated with a quantity.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class QuantityProcessAttribute : Attribute
{
    /// <summary>The name of the process.</summary>
    public string Name { get; }

    /// <summary>The type of the result. By default, the current type will be used as the result.</summary>
    public Type? Result { get; }

    /// <summary>The expression describing the process.</summary>
    public string Expression { get; }

    /// <summary>The types of the parameters passed to the process.</summary>
    public Type[] ParameterTypes { get; }

    /// <summary>The names of the parameters passed to the process.</summary>
    public string[] ParameterNames { get; }

    /// <summary>Indicates that the process should be implemented as a property, rather than as a method. The default behaviour is <see langword="false"/>.</summary>
    public bool ImplementAsProperty { get; init; }

    /// <summary>Indicates that the process should be implemented statically. The default behaviour is <see langword="false"/>.</summary>
    public bool ImplementStatically { get; init; }

    /// <inheritdoc cref="QuantityProcessAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    public QuantityProcessAttribute(string name, string expression) : this(name, null!, expression) { }

    /// <inheritdoc cref="QuantityProcessAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    public QuantityProcessAttribute(string name, Type result, string expression) : this(name, result, expression, Array.Empty<Type>(), Array.Empty<string>()) { }

    /// <inheritdoc cref="QuantityProcessAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="parameterTypes"><inheritdoc cref="ParameterTypes" path="/summary"/><para><inheritdoc cref="ParameterTypes" path="/remarks"/></para></param>
    public QuantityProcessAttribute(string name, string expression, Type[] parameterTypes) : this(name, expression, parameterTypes, Array.Empty<string>()) { }

    /// <inheritdoc cref="QuantityProcessAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="parameterTypes"><inheritdoc cref="ParameterTypes" path="/summary"/><para><inheritdoc cref="ParameterTypes" path="/remarks"/></para></param>
    public QuantityProcessAttribute(string name, Type result, string expression, Type[] parameterTypes) : this(name, result, expression, parameterTypes, Array.Empty<string>()) { }

    /// <inheritdoc cref="QuantityProcessAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="parameterTypes"><inheritdoc cref="ParameterTypes" path="/summary"/><para><inheritdoc cref="ParameterTypes" path="/remarks"/></para></param>
    /// <param name="parameterNames"><inheritdoc cref="ParameterNames" path="/summary"/><para><inheritdoc cref="ParameterNames" path="/remarks"/></para></param>
    public QuantityProcessAttribute(string name, string expression, Type[] parameterTypes, string[] parameterNames) : this(name, null!, expression, parameterTypes, parameterNames) { }

    /// <inheritdoc cref="QuantityProcessAttribute"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/><para><inheritdoc cref="Name" path="/remarks"/></para></param>
    /// <param name="result"><inheritdoc cref="Result" path="/summary"/><para><inheritdoc cref="Result" path="/remarks"/></para></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="parameterTypes"><inheritdoc cref="ParameterTypes" path="/summary"/><para><inheritdoc cref="ParameterTypes" path="/remarks"/></para></param>
    /// <param name="parameterNames"><inheritdoc cref="ParameterNames" path="/summary"/><para><inheritdoc cref="ParameterNames" path="/remarks"/></para></param>
    public QuantityProcessAttribute(string name, Type result, string expression, Type[] parameterTypes, string[] parameterNames)
    {
        Name = name;
        Result = result;
        Expression = expression;

        ParameterTypes = parameterTypes;
        ParameterNames = parameterNames;
    }
}
