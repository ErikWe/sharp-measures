namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Documentation;

using System;

internal class VectorGroupFileDocumentation : IVectorGroupDocumentationStrategy, IEquatable<VectorGroupFileDocumentation>
{
    private VectorGroupDocumentationTags DocumentationTags { get; }

    private DocumentationFile DocumentationFile { get; }
    private IVectorGroupDocumentationStrategy DefaultDocumentationStrategy { get; }

    public VectorGroupFileDocumentation(DocumentationFile documentationFile, IVectorGroupDocumentationStrategy defaultDocumentationStrategy)
    {
        DocumentationTags = new();

        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public virtual bool Equals(VectorGroupFileDocumentation? other) => other is not null && DocumentationTags == other.DocumentationTags
        && DocumentationFile == other.DocumentationFile && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);

    public override bool Equals(object? obj) => obj is VectorGroupFileDocumentation other && Equals(other);

    public static bool operator ==(VectorGroupFileDocumentation? lhs, VectorGroupFileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(VectorGroupFileDocumentation? lhs, VectorGroupFileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationTags, DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
