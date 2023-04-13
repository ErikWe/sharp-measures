namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface IRawQuantityDifference
{
    /// <summary>The quantity that represents the difference between two instances of the implementing quantity.</summary>
    public abstract ITypeSymbol Difference { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    public abstract IQuantityDifferenceSyntax? Syntax { get; }
}
