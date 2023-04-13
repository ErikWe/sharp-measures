namespace SharpMeasures.Generators.Parsing.Vectors;

using System.Collections.Generic;

/// <summary>Represents a parased <see cref="VectorComponentNamesAttribute"/>.</summary>
public interface IRawVectorComponentNames
{
    /// <summary>The names of the Cartesian components.</summary>
    public abstract IReadOnlyList<string?>? Names { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="VectorComponentNamesAttribute"/>.</summary>
    public abstract IVectorComponentNamesSyntax? Syntax { get; }
}
