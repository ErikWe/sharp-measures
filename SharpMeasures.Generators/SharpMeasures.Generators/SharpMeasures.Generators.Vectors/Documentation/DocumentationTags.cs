namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using System;

internal class DocumentationTags : IDocumentationStrategy, IEquatable<DocumentationTags>
{
    private int Dimension { get; }

    public DocumentationTags(int dimension)
    {
        Dimension = dimension;
    }

    public string Header() => "Header";

    public string Zero() => "Zero";
    public string Constant(RefinedVectorConstantDefinition definition) => $"Constant_{definition.Name}";

    public string WithScalarComponents() => "WithComponents_Scalars";
    public string WithVectorComponents() => "WithComponents_Vector";

    public string ComponentsConstructor() => "Constructor_Components";
    public string ScalarsConstructor() => "Constructor_Scalars";
    public string VectorConstructor() => "Constructor_Vector";

    public string ScalarsAndUnitConstructor() => "Constructor_Scalars_Unit";
    public string VectorAndUnitConstructor() => "Constructor_Vector_Unit";

    public string CastFromComponents() => "Operator_Cast_Components";

    public string Component(int componentIndex) => $"Component_{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, Dimension)}";
    public string ComponentMagnitude(int componentIndex) => $"Component_Magnitude_{VectorTextBuilder.GetUpperCasedComponentName(componentIndex, Dimension)}";
    public string Components() => "Components";

    public string InUnit() => "InUnit";
    public string InConstantMultiples(RefinedVectorConstantDefinition definition) => $"InMultiplesOf_{definition.Name}";
    public string InSpecifiedUnit(UnitInstance unitInstance) => $"InUnit_{unitInstance.Name}";

    public string AsDimensionallyEquivalent(IVectorInterface vector) => $"As_{vector.VectorType.Name}";
    public string CastToDimensionallyEquivalent(IVectorInterface vector) => $"Operator_Cast_{vector.VectorType.Name}";

    public string IsNaN() => "IsNaN";
    public string IsZero() => "IsZero";
    public string IsFinite() => "IsFinite";
    public string IsInfinite() => "IsInfinite";

    public string Magnitude() => "Magnitude";
    public string SquaredMagnitude() => "SquaredMagnitude";

    public string ScalarMagnitude() => "Magnitude_Scalar";
    public string ScalarSquaredMagnitude() => "SquaredMagnitude_Scalar";

    public string Normalize() => "Normalize";
    public string Transform() => "Transform";

    public string ToStringDocumentation() => "ToString";

    public string EqualsSameTypeMethod() => "Method_Equals_SameType";
    public string EqualsObjectMethod() => "Method_Equals_Object";

    public string EqualitySameTypeOperator() => "Operator_Equality_SameType";
    public string InequalitySameTypeOperator() => "Operator_Inequality_SameType";

    public string GetHashCodeDocumentation() => "GetHashCode";

    public string Deconstruct() => "Deconstruct";

    public string UnaryPlusMethod() => "Method_UnaryPlus";
    public string NegateMethod() => "Method_Negate";

    public string MultiplyScalarMethod() => "Method_Multiply_Scalar";
    public string DivideScalarMethod() => "Method_Divide_Scalar";

    public string UnaryPlusOperator() => "Operator_UnaryPlus";
    public string NegateOperator() => "Operator_Negate";

    public string MultiplyScalarOperatorLHS() => "Operator_Multiply_Scalar_LHS";
    public string MultiplyScalarOperatorRHS() => "Operator_Multiply_Scalar_RHS";
    public string DivideScalarOperatorLHS() => "Operator_Divide_Scalar_LHS";

    public virtual bool Equals(DocumentationTags? other) => other is not null && Dimension == other.Dimension;
    public override bool Equals(object? obj) => obj is DocumentationTags other && Equals(other);

    public static bool operator ==(DocumentationTags? lhs, DocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DocumentationTags? lhs, DocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Dimension.GetHashCode();
}
