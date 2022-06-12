namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Vectors.Parsing;

using System.Collections.Generic;

internal interface IDataModel
{
    public abstract DefinedType VectorType { get; }
    public abstract int Dimension { get; }

    public IEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    public VectorPopulation VectorPopulation { get; }
    public VectorPopulationData VectorPopulationData { get; }

    public abstract string Identifier { get; }
    public abstract Location? IdentifierLocation { get; }

    public abstract bool ExplicitlySetGenerateDocumentation { get; }
    public abstract bool GenerateDocumentation { get; }
    public abstract DocumentationFile Documentation { get; }
}

internal interface IDataModel<TSelf> : IDataModel where TSelf : IDataModel<TSelf>
{
    public abstract TSelf WithDocumentation(DocumentationFile documentation);
}
