namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed attribute that describes an instance of a unit as a modified form of another instance.</summary>
public interface IModifiedUnitInstanceSyntax : IUnitInstanceSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the original unit instance.</summary>
    public abstract Location OriginalUnitInstance { get; }
}
