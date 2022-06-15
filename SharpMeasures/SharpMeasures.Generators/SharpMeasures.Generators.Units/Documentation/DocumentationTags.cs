namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Linq;
using System.Text;

internal class DocumentationTags : IDocumentationStrategy, IEquatable<DocumentationTags>
{
    public static DocumentationTags Instance { get; } = new();

    private DocumentationTags() { }

    public string Header() => "Header";

    public string Derivation(DerivableSignature signature) => $"From_{ParseDerivableSignature(signature)}";
    public string Definition(IUnitDefinition definition) => $"Definition_{definition.Name}";

    public string RepresentedQuantity() => "Quantity";
    public string Offset() => "Offset";

    public string Constructor() => "Constructor";

    public string ScaledBy() => "ScaledBy";
    public string OffsetBy() => "OffsetBy";
    public string WithPrefix() => "WithPrefix";

    public string ToStringDocumentation() => "ToString";

    public string CompareToSameType() => "CompareTo_SameType";

    public string LessThanSameType() => "Operator_LessThan_SameType";
    public string GreaterThanSameType() => "Operator_GreaterThan_SameType";
    public string LessThanOrEqualSameType() => "Operator_LessThanOrEqual_SameType";
    public string GreaterThanOrEqualSameType() => "Operator_GreaterThanOrEqual_SameType";

    private static string ParseDerivableSignature(DerivableSignature signature)
    {
        StringBuilder tag = new();

        IterativeBuilding.AppendEnumerable(tag, signature.Select(static (x) => x.Name), "_");

        return tag.ToString();
    }

    public virtual bool Equals(DocumentationTags? other)
    {
        return other is not null;
    }

    public override bool Equals(object obj)
    {
        if (obj is DocumentationTags other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(DocumentationTags? lhs, DocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DocumentationTags? lhs, DocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
