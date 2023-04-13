namespace SharpMeasures.Generators.Parsing.Units;

/// <summary>Represents a parsed <see cref="UnitInstanceAliasAttribute"/>.</summary>
public interface IRawAliasedUnitInstance : IRawModifiedUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="UnitInstanceAliasAttribute"/>.</summary>
    new public abstract IAliasedUnitInstanceSyntax? Syntax { get; }
}
