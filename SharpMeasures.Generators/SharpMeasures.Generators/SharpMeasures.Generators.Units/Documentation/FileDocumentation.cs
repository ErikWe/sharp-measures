namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.UnitInstances;

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

    public string Derivation(UnitDerivationSignature signature) => FromFileOrDefault((strategy) => strategy.Derivation(signature));
    public string Definition(IUnitInstance definition) => FromFileOrDefault((strategy) => strategy.Definition(definition));

    public string RepresentedQuantity() => FromFileOrDefault(static (strategy) => strategy.RepresentedQuantity());
    public string Bias() => FromFileOrDefault(static (strategy) => strategy.Bias());

    public string Constructor() => FromFileOrDefault(static (strategy) => strategy.Constructor());

    public string ScaledBy() => FromFileOrDefault(static (strategy) => strategy.ScaledBy());
    public string WithBias() => FromFileOrDefault(static (strategy) => strategy.WithBias());
    public string WithPrefix() => FromFileOrDefault(static (strategy) => strategy.WithPrefix());

    public string ToStringDocumentation() => FromFileOrDefault(static (strategy) => strategy.ToStringDocumentation());

    public string EqualsSameTypeMethod() => FromFileOrDefault(static (strategy) => strategy.EqualsSameTypeMethod());
    public string EqualsObjectMethod() => FromFileOrDefault(static (strategy) => strategy.EqualsObjectMethod());

    public string EqualitySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.EqualitySameTypeOperator());
    public string InequalitySameTypeOperator() => FromFileOrDefault(static (strategy) => strategy.InequalitySameTypeOperator());

    public string GetHashCodeDocumentation() => FromFileOrDefault(static (strategy) => strategy.GetHashCodeDocumentation());

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

    public virtual bool Equals(FileDocumentation? other) => other is not null && DocumentationFile == other.DocumentationFile
        && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);

    public override bool Equals(object? obj) => obj is FileDocumentation other && Equals(other);

    public static bool operator ==(FileDocumentation? lhs, FileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(FileDocumentation? lhs, FileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
