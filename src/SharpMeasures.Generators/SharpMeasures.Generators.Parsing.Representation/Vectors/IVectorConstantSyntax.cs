namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="VectorConstantAttribute"/>.</summary>
public interface IVectorConstantSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the constant.</summary>
    public abstract Location Name { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the unit instance in which the provided value is expressed.</summary>
    public abstract Location UnitInstanceName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the value of the constant.</summary>
    public abstract Location ValueCollection { get; }

    /// <summary>The <see cref="Location"/> of the individual elements in the argument for the value of the constant.</summary>
    public abstract IReadOnlyList<Location> ValueElements { get; }
}
