namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal interface IDataModel
{
    public abstract DefinedType VectorType { get; }
    public abstract int Dimension { get; }

    public abstract UnitInterface Unit { get; }
    public abstract ScalarInterface? Scalar { get; }

    public abstract IEnumerable<IncludeUnitsDefinition> IncludeUnits { get; }
    public abstract IEnumerable<ExcludeUnitsDefinition> ExcludeUnits { get; }
    public abstract IEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract VectorPopulation VectorPopulation { get; }
    public abstract VectorPopulationData VectorPopulationData { get; }

    public abstract string Identifier { get; }
    public abstract Location? IdentifierLocation { get; }

    public abstract bool ExplicitlySetGenerateDocumentation { get; }
    public abstract bool GenerateDocumentation { get; }
    public abstract IDocumentationStrategy Documentation { get; }
}

internal interface IDataModel<TSelf> : IDataModel where TSelf : IDataModel<TSelf>
{
    public abstract TSelf WithDocumentation(IDocumentationStrategy documentation);
}
