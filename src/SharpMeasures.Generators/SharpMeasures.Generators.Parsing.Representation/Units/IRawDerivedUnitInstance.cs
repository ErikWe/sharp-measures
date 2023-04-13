namespace SharpMeasures.Generators.Parsing.Units;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
public interface IRawDerivedUnitInstance : IRawUnitInstance
{
    /// <summary>The ID of the intended derivation signature, if provided.</summary>
    public abstract string? DerivationID { get; }

    /// <summary>The names of the unit instances of other units from which the unit instance is derived.</summary>
    public abstract IReadOnlyList<string?>? Units { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
    new public abstract IDerivedUnitInstanceSyntax? Syntax { get; }
}
