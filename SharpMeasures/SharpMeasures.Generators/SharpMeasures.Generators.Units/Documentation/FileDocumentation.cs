namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System;

internal class FileDocumentation : IDocumentationStrategy, IEquatable<FileDocumentation>
{
    private DocumentationFile DocumentationFile { get; }
    private IDocumentationStrategy DefaultDocumentationStrategy { get; }

    public FileDocumentation(DocumentationFile documentationFile, IDocumentationStrategy defaultDocumentationStrategy)
    {
        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public string Header() => FromFileOrDefault(static (strategy) => strategy.Header());

    public string Derivation(DerivableSignature signature) => FromFileOrDefault((strategy) => strategy.Derivation(signature));
    public string Definition(IUnitDefinition definition) => FromFileOrDefault((strategy) => strategy.Definition(definition));

    public string RepresentedQuantity() => FromFileOrDefault(static (strategy) => strategy.RepresentedQuantity());
    public string Offset() => FromFileOrDefault(static (strategy) => strategy.Offset());

    public string Constructor() => FromFileOrDefault(static (strategy) => strategy.Constructor());

    public string ScaledBy() => FromFileOrDefault(static (strategy) => strategy.ScaledBy());
    public string OffsetBy() => FromFileOrDefault(static (strategy) => strategy.OffsetBy());
    public string WithPrefix() => FromFileOrDefault(static (strategy) => strategy.WithPrefix());

    public string ToStringDocumentation() => FromFileOrDefault(static (strategy) => strategy.ToStringDocumentation());

    public string CompareToSameType() => FromFileOrDefault(static (strategy) => strategy.CompareToSameType());

    public string LessThanSameType() => FromFileOrDefault(static (strategy) => strategy.LessThanSameType());
    public string GreaterThanSameType() => FromFileOrDefault(static (strategy) => strategy.GreaterThanSameType());
    public string LessThanOrEqualSameType() => FromFileOrDefault(static (strategy) => strategy.LessThanOrEqualSameType());
    public string GreaterThanOrEqualSameType() => FromFileOrDefault(static (strategy) => strategy.GreaterThanOrEqualSameType());

    private string FromFileOrDefault(Func<IDocumentationStrategy, string> defaultDelegate)
    {
        string tag = defaultDelegate(DocumentationTags.Instance);

        if (DocumentationFile.OptionallyResolveTag(tag) is not string { Length: > 0 } tagContent)
        {
            tagContent = defaultDelegate(DefaultDocumentationStrategy);
        }

        return tagContent;
    }

    public virtual bool Equals(FileDocumentation? other)
    {
        if (other is null)
        {
            return false;
        }

        return DocumentationFile == other.DocumentationFile && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);
    }

    public override bool Equals(object obj)
    {
        if (obj is FileDocumentation other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(FileDocumentation? lhs, FileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(FileDocumentation? lhs, FileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
