namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents the syntactical information about a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
public interface IQuantityPropertySyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the resulting type of the property.</summary>
    public abstract Location Result { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the property.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the expression describing the property.</summary>
    public abstract Location Expression { get; }

    /// <summary>The <see cref="Location"/> of the argument for the whether the property should be implemented statically. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementStatically { get; }
}
