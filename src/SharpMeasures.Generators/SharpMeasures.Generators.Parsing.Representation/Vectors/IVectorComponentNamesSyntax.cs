namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="VectorComponentNamesAttribute"/>.</summary>
public interface IVectorComponentNamesSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the names of the Cartesian components.</summary>
    public abstract Location NamesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the Cartesian components.</summary>
    public abstract IReadOnlyList<Location> NamesElements { get; }
}
