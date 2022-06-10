namespace SharpMeasures.Generators.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal record class ResizedDataModel : IDataModel<ResizedDataModel>
{
    public ParsedResizedVector Vector { get; }
    public NamedType AssociatedRootVector { get; }

    public UnitInterface Unit { get; }
    public ScalarInterface? Scalar { get; }

    public DocumentationFile Documentation { get; init; } = DocumentationFile.Empty;

    public ResizedDataModel(ParsedResizedVector vector, NamedType associatedRootVector, UnitInterface unit, ScalarInterface? scalar)
    {
        Vector = vector;
        AssociatedRootVector = associatedRootVector;
        Unit = unit;
        Scalar = scalar;
    }

    string IDataModel<ResizedDataModel>.Identifier => Vector.VectorType.Name;
    Location IDataModel<ResizedDataModel>.IdentifierLocation => Vector.VectorLocation.AsRoslynLocation();

    bool IDataModel<ResizedDataModel>.GenerateDocumentation => Vector.VectorDefinition.GenerateDocumentation;
    bool IDataModel<ResizedDataModel>.ExplicitlySetGenerateDocumentation => Vector.VectorDefinition.Locations.ExplicitlySetGenerateDocumentation;

    ResizedDataModel IDataModel<ResizedDataModel>.WithDocumentation(DocumentationFile documentation) => this with { Documentation = documentation };
}
