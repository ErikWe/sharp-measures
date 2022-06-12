namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Refinement;

using System.Collections.Generic;

internal record class DataModel : IDataModel<DataModel>
{
    public RefinedGeneratedVectorDefinition VectorDefinition { get; }
    public ParsedGeneratedVector VectorData { get; }

    public VectorPopulation VectorPopulation { get; }
    public VectorPopulationData VectorPopulationData { get; }

    public DocumentationFile Documentation { get; init; } = DocumentationFile.Empty;

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

    IEnumerable<DimensionalEquivalenceDefinition> IDataModel.DimensionalEquivalences => VectorData.DimensionalEquivalences;

    string IDataModel.Identifier => VectorData.VectorType.Name;
    Location IDataModel.IdentifierLocation => VectorData.VectorLocation.AsRoslynLocation();

    bool IDataModel.GenerateDocumentation => VectorData.VectorDefinition.GenerateDocumentation;
    bool IDataModel.ExplicitlySetGenerateDocumentation => VectorData.VectorDefinition.Locations.ExplicitlySetGenerateDocumentation;

    DataModel IDataModel<DataModel>.WithDocumentation(DocumentationFile documentation) => this with { Documentation = documentation };
}
