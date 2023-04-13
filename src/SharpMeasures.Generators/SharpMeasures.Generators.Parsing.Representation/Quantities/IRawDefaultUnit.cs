namespace SharpMeasures.Generators.Parsing.Quantities;

/// <summary>Represents a parsed <see cref="DefaultUnitAttribute"/>.</summary>
public interface IRawDefaultUnit
{
    /// <summary>The name of the default unit instance.</summary>
    public abstract string? Unit { get; }

    /// <summary>The symbol representing the default unit instance.</summary>
    public abstract string? Symbol { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="DefaultUnitAttribute"/>.</summary>
    public abstract IDefaultUnitSyntax? Syntax { get; }
}
