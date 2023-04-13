namespace SharpMeasures.Generators.Parsing.Units;

/// <summary>Represents a parsed <see cref="FixedUnitInstanceAttribute"/>.</summary>
public interface IRawFixedUnitInstance : IRawUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="FixedUnitInstanceAttribute"/>.</summary>
    new public abstract IFixedUnitInstanceSyntax? Syntax { get; }
}
