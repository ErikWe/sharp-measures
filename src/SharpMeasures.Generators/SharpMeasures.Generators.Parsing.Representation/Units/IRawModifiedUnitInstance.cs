namespace SharpMeasures.Generators.Parsing.Units;

/// <summary>Represents a parsed attribute that describes an instance of a unit as a modified form of another instance.</summary>
public interface IRawModifiedUnitInstance : IRawUnitInstance
{
    /// <summary>The <see cref="string"/> name of the original unit instance, of which this unit instance is a modified form.</summary>
    public abstract string? OriginalUnitInstance { get; }

    /// <summary>Provides syntactical information about the parsed attribute.</summary>
    new public abstract IModifiedUnitInstanceSyntax? Syntax { get; }
}
