namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal record class DataModel : IDataModel<DataModel>
{
    public ParsedVector Vector { get; }
    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public DocumentationFile Documentation { get; init; } = DocumentationFile.Empty;

    public DataModel(ParsedVector vector, UnitInterface unit, ScalarInterface? scalar)
    {
        Vector = vector;
        Unit = unit;
        Scalar = scalar;
    }

    string IDataModel<DataModel>.Identifier => Vector.VectorType.Name;
    Location IDataModel<DataModel>.IdentifierLocation => Vector.VectorLocation.AsRoslynLocation();

    bool IDataModel<DataModel>.GenerateDocumentation => Vector.VectorDefinition.GenerateDocumentation;
    bool IDataModel<DataModel>.ExplicitlySetGenerateDocumentation => Vector.VectorDefinition.Locations.ExplicitlySetGenerateDocumentation;

    DataModel IDataModel<DataModel>.WithDocumentation(DocumentationFile documentation) => this with { Documentation = documentation };
}
