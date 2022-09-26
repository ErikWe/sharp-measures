namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Documentation;

using System;

internal sealed class GroupFileDocumentation : IGroupDocumentationStrategy, IEquatable<GroupFileDocumentation>
{
    private GroupDocumentationTags DocumentationTags { get; }

    private DocumentationFile DocumentationFile { get; }
    private IGroupDocumentationStrategy DefaultDocumentationStrategy { get; }

    public GroupFileDocumentation(DocumentationFile documentationFile, IGroupDocumentationStrategy defaultDocumentationStrategy)
    {
        DocumentationTags = new();

        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public string Header() => FromFileOrDefault(static (strategy) => strategy.Header());

    public string ScalarFactoryMethod(int dimension) => FromFileOrDefault((strategy) => strategy.ScalarFactoryMethod(dimension));
    public string VectorFactoryMethod(int dimension) => FromFileOrDefault((strategy) => strategy.VectorFactoryMethod(dimension));
    public string ComponentsFactoryMethod(int dimension) => FromFileOrDefault((strategy) => strategy.ComponentsFactoryMethod(dimension));

    private string FromFileOrDefault(Func<IGroupDocumentationStrategy, string> defaultDelegate)
    {
        string tag = defaultDelegate(DocumentationTags);

        if (DocumentationFile.OptionallyResolveTag(tag) is not string { Length: > 0 } tagContent)
        {
            tagContent = defaultDelegate(DefaultDocumentationStrategy);
        }

        return tagContent;
    }

    public bool Equals(GroupFileDocumentation? other) => other is not null && DocumentationTags == other.DocumentationTags && DocumentationFile == other.DocumentationFile && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);
    public override bool Equals(object? obj) => obj is GroupFileDocumentation other && Equals(other);

    public static bool operator ==(GroupFileDocumentation? lhs, GroupFileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(GroupFileDocumentation? lhs, GroupFileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationTags, DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
