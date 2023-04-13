namespace SharpMeasures.Generators.Parsing.Units;

using OneOf;

/// <summary>Represents a parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
public interface IRawBiasedUnitInstance : IRawModifiedUnitInstance
{
    /// <summary>The bias relative to the original unit instance.</summary>
    public abstract OneOf<double, string?> Bias { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
    new public abstract IBiasedUnitInstanceSyntax? Syntax { get; }
}
