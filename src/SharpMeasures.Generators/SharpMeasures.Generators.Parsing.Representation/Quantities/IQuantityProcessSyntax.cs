namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents the syntactical information about a parsed <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
public interface IQuantityProcessSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the type that is the result of the process.</summary>
    public abstract Location Result { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the process.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the expression describing the process.</summary>
    public abstract Location Expression { get; }

    /// <summary>The <see cref="Location"/> of the argument for the signature of the process. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location SignatureCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the signature of the process.</summary>
    public abstract IReadOnlyList<Location> SignatureElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for the names of the parameters of the process. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ParameterNamesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the parameters of the process.</summary>
    public abstract IReadOnlyList<Location> ParameterNamesElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for the whether the process should be implemented statically. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementStatically { get; }
}
