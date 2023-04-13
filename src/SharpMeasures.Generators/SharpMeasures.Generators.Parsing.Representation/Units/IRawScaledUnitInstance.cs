namespace SharpMeasures.Generators.Parsing.Units;

using OneOf;

/// <summary>Represents a parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
public interface IRawScaledUnitInstance : IRawModifiedUnitInstance
{
    /// <summary>The value by which the original unit instance is scaled.</summary>
    public abstract OneOf<double, string?> Scale { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
    new public abstract IScaledUnitInstanceSyntax? Syntax { get; }
}
