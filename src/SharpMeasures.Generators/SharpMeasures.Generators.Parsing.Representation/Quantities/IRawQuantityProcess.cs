namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
public interface IRawQuantityProcess
{
    /// <summary>The type that is the result of the process.</summary>
    public abstract ITypeSymbol Result { get; }

    /// <summary>The name of the process.</summary>
    public abstract string? Name { get; }

    /// <summary>The expression describing the process.</summary>
    public abstract string? Expression { get; }

    /// <summary>The signature of the process.</summary>
    public abstract IReadOnlyList<ITypeSymbol?>? Signature { get; }

    /// <summary>The names of the parameters of the process.</summary>
    public abstract IReadOnlyList<string?>? ParameterNames { get; }

    /// <summary>Indicates that the process should be implemented statically.</summary>
    public abstract bool? ImplementStatically { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
    public abstract IQuantityProcessSyntax? Syntax { get; }
}
