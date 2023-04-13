namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
public interface IPrefixedUnitInstanceSyntax : IModifiedUnitInstanceSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the prefix that is applied to the original unit instance.</summary>
    public abstract Location Prefix { get; }
}
