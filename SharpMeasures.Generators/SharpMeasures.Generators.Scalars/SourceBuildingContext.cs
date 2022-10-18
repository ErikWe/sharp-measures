namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Scalars.Documentation;

internal sealed record class SourceBuildingContext
{
    public GeneratedFileHeaderContent HeaderContent { get; }

    public IDocumentationStrategy Documentation { get; }

    public SourceBuildingContext(GeneratedFileHeaderContent headerContent, IDocumentationStrategy documentation)
    {
        HeaderContent = headerContent;

        Documentation = documentation;
    }
}
