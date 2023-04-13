namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
public interface IBiasedUnitInstanceSyntax : IModifiedUnitInstanceSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the bias relative to the original unit instance.</summary>
    public abstract Location Bias { get; }
}
