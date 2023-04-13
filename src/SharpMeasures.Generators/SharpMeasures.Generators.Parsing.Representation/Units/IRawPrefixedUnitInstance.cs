namespace SharpMeasures.Generators.Parsing.Units;

using OneOf;

/// <summary>Represents a parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
public interface IRawPrefixedUnitInstance : IRawModifiedUnitInstance
{
    /// <summary>The prefix that is applied to the original unit instance.</summary>
    public abstract OneOf<MetricPrefixName, BinaryPrefixName> Prefix { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
    new public abstract IPrefixedUnitInstanceSyntax? Syntax { get; }
}
