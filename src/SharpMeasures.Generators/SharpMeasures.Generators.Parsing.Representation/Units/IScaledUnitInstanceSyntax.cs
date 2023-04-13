namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
public interface IScaledUnitInstanceSyntax : IModifiedUnitInstanceSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the value by which the original unit instance is scaled.</summary>
    public abstract Location Scale { get; }
}
