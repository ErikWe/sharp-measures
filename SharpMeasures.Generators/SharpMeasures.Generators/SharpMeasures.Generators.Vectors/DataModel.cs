﻿namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Refinement.GeneratedVector;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal record class DataModel : IDataModel<DataModel>
{
    public RefinedGeneratedVectorDefinition VectorDefinition { get; }
    public ParsedGeneratedVector VectorData { get; }

    public VectorPopulation VectorPopulation { get; }
    public VectorPopulationData VectorPopulationData { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(RefinedGeneratedVectorDefinition vectorDefinition, ParsedGeneratedVector vectorData, VectorPopulation vectorPopulation,
        VectorPopulationData vectorPopulationData)
    {
        VectorDefinition = vectorDefinition;
        VectorData = vectorData;

        VectorPopulation = vectorPopulation;
        VectorPopulationData = vectorPopulationData;
    }

    DefinedType IDataModel.VectorType => VectorData.VectorType;
    int IDataModel.Dimension => VectorDefinition.Dimension;

    UnitInterface IDataModel.Unit => VectorDefinition.Unit;
    ScalarInterface? IDataModel.Scalar => VectorDefinition.Scalar;

    IEnumerable<IncludeUnitsDefinition> IDataModel.IncludeUnits => VectorData.IncludeUnits;
    IEnumerable<ExcludeUnitsDefinition> IDataModel.ExcludeUnits => VectorData.ExcludeUnits;
    IEnumerable<DimensionalEquivalenceDefinition> IDataModel.DimensionalEquivalences => VectorData.DimensionalEquivalences;

    string? IDataModel.DefaultUnitName => VectorDefinition.DefaultUnitName;
    string? IDataModel.DefaultUnitSymbol => VectorDefinition.DefaultUnitSymbol;

    string IDataModel.Identifier => VectorData.VectorType.Name;
    Location IDataModel.IdentifierLocation => VectorData.VectorLocation.AsRoslynLocation();

    bool IDataModel.GenerateDocumentation => VectorData.VectorDefinition.GenerateDocumentation;
    bool IDataModel.ExplicitlySetGenerateDocumentation => VectorData.VectorDefinition.Locations.ExplicitlySetGenerateDocumentation;

    DataModel IDataModel<DataModel>.WithDocumentation(IDocumentationStrategy documentation) => this with { Documentation = documentation };
}