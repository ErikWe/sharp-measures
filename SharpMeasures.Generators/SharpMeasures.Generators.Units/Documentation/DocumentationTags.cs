namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Text;

internal sealed class DocumentationTags : IDocumentationStrategy, IEquatable<DocumentationTags>
{
    public static DocumentationTags Instance { get; } = new();

    private DocumentationTags() { }

    public string Header() => "Header";

    public string Derivation(IReadOnlyList<NamedType> signature) => $"Derivation_{ParseDerivableSignature(signature)}";
    public string FixedUnitInstance(IFixedUnitInstance unitInstance) => $"UnitInstance_{unitInstance.Name}";
    public string DerivedUnitInstance(IDerivedUnitInstance unitInstance) => $"UnitInstance_{unitInstance.Name}";
    public string UnitAliasInstance(IUnitInstanceAlias unitInstance) => $"UnitInstance_{unitInstance.Name}";
    public string BiasedUnitInstance(IBiasedUnitInstance unitInstance) => $"UnitInstance_{unitInstance.Name}";
    public string PrefixedUnitInstance(IPrefixedUnitInstance unitInstance) => $"UnitInstance_{unitInstance.Name}";
    public string ScaledUnitInstance(IScaledUnitInstance unitInstance) => $"UnitInstance_{unitInstance.Name}";

    public string RepresentedQuantity() => "Quantity";
    public string Bias() => "Bias";

    public string Constructor() => "Constructor";

    public string ScaledBy() => "ScaledBy";
    public string WithBias() => "WithBias";
    public string WithPrefix() => "WithPrefix";

    public string ToStringDocumentation() => "ToString";

    public string EqualsSameTypeMethod() => "Method_Equals_SameType";
    public string EqualsObjectMethod() => "Method_Equals_Object";

    public string EqualitySameTypeOperator() => "Operator_Equality_SameType";
    public string InequalitySameTypeOperator() => "Operator_Inequality_SameType";

    public string GetHashCodeDocumentation() => "GetHashCode";

    public string CompareToSameType() => "CompareTo_SameType";

    public string LessThanSameType() => "Operator_LessThan_SameType";
    public string GreaterThanSameType() => "Operator_GreaterThan_SameType";
    public string LessThanOrEqualSameType() => "Operator_LessThanOrEqual_SameType";
    public string GreaterThanOrEqualSameType() => "Operator_GreaterThanOrEqual_SameType";

    private static string ParseDerivableSignature(IReadOnlyList<NamedType> signature)
    {
        StringBuilder tag = new();

        IterativeBuilding.AppendEnumerable(tag, transformSignature(), "_");

        return tag.ToString();

        IReadOnlyList<string> transformSignature()
        {
            List<string> transformedSignature = new(signature.Count);

            foreach (var signatureComponent in signature)
            {
                transformedSignature.Add(signatureComponent.Name);
            }

            return transformedSignature;
        }
    }

    public bool Equals(DocumentationTags? other) => other is not null;
    public override bool Equals(object? obj) => obj is DocumentationTags other && Equals(other);

    public static bool operator ==(DocumentationTags? lhs, DocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DocumentationTags? lhs, DocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
