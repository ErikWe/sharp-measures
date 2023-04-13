namespace SharpMeasures.Generators.Parsing.Vectors.VectorComponentNamesAttribute;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawVectorComponentNames"/>
internal sealed record class RawVectorComponentNames : IRawVectorComponentNames
{
    private IReadOnlyList<string?>? Names { get; }

    private IVectorComponentNamesSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawVectorComponentNames"/>, representing a parsed <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="names"><inheritdoc cref="IRawVectorComponentNames.Names" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawVectorComponentNames.Syntax" path="/summary"/></param>
    public RawVectorComponentNames(IReadOnlyList<string?>? names, IVectorComponentNamesSyntax? syntax)
    {
        Names = names;

        Syntax = syntax;
    }

    IReadOnlyList<string?>? IRawVectorComponentNames.Names => Names;

    IVectorComponentNamesSyntax? IRawVectorComponentNames.Syntax => Syntax;
}
