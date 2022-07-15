﻿namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Refinement.SharpMeasuresVector;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Populations;
using SharpMeasures.Generators.Vectors.Populations;

internal record class DataModel : IDataModel<DataModel>
{
    public RefinedSharpMeasuresVectorDefinition VectorDefinition { get; }
    public ParsedBaseVector VectorData { get; }

    public VectorPopulation VectorPopulation { get; }
    public VectorPopulationErrors VectorPopulationData { get; }

    public IDocumentationStrategy Documentation { get; init; } = EmptyDocumentation.Instance;

    public DataModel(RefinedSharpMeasuresVectorDefinition vectorDefinition, ParsedBaseVector vectorData, VectorPopulation vectorPopulation,
        VectorPopulationErrors vectorPopulationData)
    {
        VectorDefinition = vectorDefinition;
        VectorData = vectorData;

        VectorPopulation = vectorPopulation;
        VectorPopulationData = vectorPopulationData;
    }

    DefinedType IDataModel.VectorType => VectorData.VectorType;
    int IDataModel.Dimension => VectorDefinition.Dimension;

    bool IDataModel.ImplementSum => VectorDefinition.ImplementSum;
    bool IDataModel.ImplementDifference => VectorDefinition.ImplementDifference;

    NamedType IDataModel.Difference => VectorDefinition.Difference;

    IUnitType IDataModel.Unit => VectorDefinition.Unit;
    IScalarType? IDataModel.Scalar => VectorDefinition.Scalar;

    IEnumerable<UnresolvedIncludeUnitsDefinition> IDataModel.IncludeUnits => VectorData.IncludeUnits;
    IEnumerable<UnresolvedExcludeUnitsDefinition> IDataModel.ExcludeUnits => VectorData.ExcludeUnits;
    IEnumerable<UnresolvedConvertibleQuantityDefinition> IDataModel.DimensionalEquivalences => VectorData.DimensionalEquivalences;

    string? IDataModel.DefaultUnitName => VectorDefinition.DefaultUnitName;
    string? IDataModel.DefaultUnitSymbol => VectorDefinition.DefaultUnitSymbol;

    string IDataModel.Identifier => VectorData.VectorType.Name;
    Location IDataModel.IdentifierLocation => VectorData.VectorLocation.AsRoslynLocation();

    bool IDataModel.GenerateDocumentation => VectorData.VectorDefinition.GenerateDocumentation;
    bool IDataModel.ExplicitlySetGenerateDocumentation => VectorData.VectorDefinition.Locations.ExplicitlySetGenerateDocumentation;

    DataModel IDataModel<DataModel>.WithDocumentation(IDocumentationStrategy documentation) => this with { Documentation = documentation };
}
