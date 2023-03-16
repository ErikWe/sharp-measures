namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Vectors.Documentation;

internal sealed record class VectorSourceBuildingContext
{
    public GeneratedFileHeaderContent HeaderContent { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public VectorSourceBuildingContext(GeneratedFileHeaderContent headerContent, IVectorDocumentationStrategy documentation)
    {
        HeaderContent = headerContent;

        Documentation = documentation;
    }
}
