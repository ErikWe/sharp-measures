namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Documentation;

using System;
using System.Collections.Generic;

internal sealed class FileDocumentation : IDocumentationStrategy, IEquatable<FileDocumentation>
{
    private bool PrintDocumentationTags { get; }
    private DocumentationFile? DocumentationFile { get; }
    private IDocumentationStrategy DefaultDocumentationStrategy { get; }

    public FileDocumentation(bool printDocumentationTags, DocumentationFile? documentationFile, IDocumentationStrategy defaultDocumentationStrategy)
    {
        PrintDocumentationTags = printDocumentationTags;
        DocumentationFile = documentationFile;
        DefaultDocumentationStrategy = defaultDocumentationStrategy;
    }

    public string Header() => FromFileOrDefault(static (strategy) => strategy.Header());

    public string Derivation(IReadOnlyList<NamedType> signature) => FromFileOrDefault((strategy) => strategy.Derivation(signature));
    public string FixedUnitInstance(IFixedUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.FixedUnitInstance(unitInstance));
    public string DerivedUnitInstance(IDerivedUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.DerivedUnitInstance(unitInstance));
    public string UnitAliasInstance(IUnitInstanceAlias unitInstance) => FromFileOrDefault((strategy) => strategy.UnitAliasInstance(unitInstance));
    public string BiasedUnitInstance(IBiasedUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.BiasedUnitInstance(unitInstance));
    public string PrefixedUnitInstance(IPrefixedUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.PrefixedUnitInstance(unitInstance));
    public string ScaledUnitInstance(IScaledUnitInstance unitInstance) => FromFileOrDefault((strategy) => strategy.ScaledUnitInstance(unitInstance));

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

    private string FromFileOrDefault(Func<IDocumentationStrategy, string> target)
    {
        string tag = target(DocumentationTags.Instance);

        if (DocumentationFile?.OptionallyResolveTag(tag) is not string { Length: > 0 } tagContent)
        {
            tagContent = target(DefaultDocumentationStrategy);
        }

        if (PrintDocumentationTags)
        {
            tagContent = $"""
                {tagContent}
                /// <sharpmeasures-tag>{tag}</sharpmeasures-tag>
                """;
        }

        return tagContent;
    }

    public bool Equals(FileDocumentation? other) => other is not null && PrintDocumentationTags == other.PrintDocumentationTags && DocumentationFile == other.DocumentationFile && DefaultDocumentationStrategy.Equals(other.DefaultDocumentationStrategy);
    public override bool Equals(object? obj) => obj is FileDocumentation other && Equals(other);

    public static bool operator ==(FileDocumentation? lhs, FileDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(FileDocumentation? lhs, FileDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (PrintDocumentationTags, DocumentationFile, DefaultDocumentationStrategy).GetHashCode();
}
