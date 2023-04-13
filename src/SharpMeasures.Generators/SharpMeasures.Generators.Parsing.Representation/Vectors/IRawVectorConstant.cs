namespace SharpMeasures.Generators.Parsing.Vectors;

using OneOf;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="VectorConstantAttribute"/>.</summary>
public interface IRawVectorConstant
{
    /// <summary>The name of the constant.</summary>
    public abstract string? Name { get; }

    /// <summary>The name of the unit instance in which the provided value is expressed.</summary>
    public abstract string? UnitInstanceName { get; }

    /// <summary>The value of the constant, when expressed in the provided unit instance.</summary>
    public abstract OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="VectorConstantAttribute"/>.</summary>
    public abstract IVectorConstantSyntax? Syntax { get; }
}
