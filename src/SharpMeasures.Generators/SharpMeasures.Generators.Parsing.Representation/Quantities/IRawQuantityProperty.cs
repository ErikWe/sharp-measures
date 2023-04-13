namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
public interface IRawQuantityProperty
{
    /// <summary>The resulting type of the property.</summary>
    public abstract ITypeSymbol Result { get; }

    /// <summary>The name of the property.</summary>
    public abstract string? Name { get; }

    /// <summary>The expression describing the property.</summary>
    public abstract string? Expression { get; }

    /// <summary>Indicates that the property should be implemented statically.</summary>
    public abstract bool? ImplementStatically { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
    public abstract IQuantityPropertySyntax? Syntax { get; }
}
