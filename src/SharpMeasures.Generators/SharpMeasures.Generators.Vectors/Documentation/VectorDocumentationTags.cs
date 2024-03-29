﻿namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;

internal sealed class VectorDocumentationTags : IVectorDocumentationStrategy, IEquatable<VectorDocumentationTags>
{
    private int Dimension { get; }

    public VectorDocumentationTags(int dimension)
    {
        Dimension = dimension;
    }

    public string Header() => "Header";

    public string Zero() => "Zero";
    public string Constant(IVectorConstant constant) => $"Constant_{constant.Name}";

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
    public string InConstantMultiples(IVectorConstant constant) => $"InMultiplesOf_{constant.Name}";
    public string InSpecifiedUnit(IUnitInstance unitInstance) => $"InUnit_{unitInstance.Name}";

    public string Conversion(NamedType vector) => $"As_{vector.Name}";
    public string AntidirectionalConversion(NamedType vector) => $"From_{vector.Name}";
    public string CastConversion(NamedType vector) => $"Operator_CastTo_{vector.Name}";
    public string AntidirectionalCastConversion(NamedType vector) => $"Operator_CastFrom_{vector.Name}";

    public string OperationMethod(IQuantityOperation operation, NamedType other) => $"OperationMethod_{operation.MethodName}_{other.Name}";
    public string MirroredOperationMethod(IQuantityOperation operation, NamedType other) => $"OperationMethod_{operation.MirroredMethodName}_{other.Name}";
    public string VectorOperationMethod(IVectorOperation operation, NamedType other) => $"VectorOperationMethod_{operation.Name}_{other.Name}";
    public string MirroredVectorOperationMethod(IVectorOperation operation, NamedType other) => $"VectorOperationMethod_{operation.MirroredName}_{other.Name}";
    public string OperationOperator(IQuantityOperation operation, NamedType other) => $"Operation_{(operation.Position is OperatorPosition.Left ? "LHS" : "RHS")}_{operation.OperatorType}_{other.Name}";
    public string MirroredOperationOperator(IQuantityOperation operation, NamedType other) => $"Operation_{(operation.Position is OperatorPosition.Right ? "LHS" : "RHS")}_{operation.OperatorType}_{other.Name}";

    public string Process(IQuantityProcess process) => $"Process_{process.Name}";

    public string IsNaN() => "IsNaN";
    public string IsZero() => "IsZero";
    public string IsFinite() => "IsFinite";
    public string IsInfinite() => "IsInfinite";

    public string Magnitude() => "Magnitude";

    public string ScalarMagnitude() => "Magnitude_Scalar";
    public string ScalarSquaredMagnitude() => "SquaredMagnitude_Scalar";

    public string Normalize() => "Normalize";
    public string Transform() => "Transform";

    public string ToStringMethod() => "ToString";
    public string ToStringFormat() => "ToString_Format";
    public string ToStringProvider() => "ToString_Provider";
    public string ToStringFormatAndProvider() => "ToString_Format_Provider";

    public string EqualsSameTypeMethod() => "Method_Equals_SameType";
    public string EqualsObjectMethod() => "Method_Equals_Object";

    public string EqualitySameTypeOperator() => "Operator_Equality_SameType";
    public string InequalitySameTypeOperator() => "Operator_Inequality_SameType";

    public string GetHashCodeDocumentation() => "GetHashCode";

    public string Deconstruct() => "Deconstruct";

    public string UnaryPlusMethod() => "Method_UnaryPlus";
    public string NegateMethod() => "Method_Negate";

    public string AddSameTypeMethod() => "Method_Add_SameType";
    public string SubtractSameTypeMethod() => "Method_Subtract_SameType";

    public string AddDifferenceMethod() => "Method_Add_Difference";
    public string SubtractDifferenceMethod() => "Method_Subtract_Difference";

    public string MultiplyScalarMethod() => "Method_Multiply_Scalar";
    public string DivideScalarMethod() => "Method_Divide_Scalar";

    public string AddTVectorMethod() => "Method_Add_TVector";
    public string SubtractTVectorMethod() => "Method_Subtract_TVector";
    public string SubtractFromTVectorMethod() => "Method_SubtractFrom_TVector";
    public string MultiplyTScalarMethod() => "Method_Multiply_TScalar";
    public string DivideTScalarMethod() => "Method_Divide_TScalar";

    public string UnaryPlusOperator() => "Operator_UnaryPlus";
    public string NegateOperator() => "Operator_Negate";

    public string AddSameTypeOperator() => "Operator_Add_SameType";
    public string SubtractSameTypeOperator() => "Operator_Subtract_SameType";

    public string AddDifferenceOperatorLHS() => "Operator_Add_Difference_LHS";
    public string AddDifferenceOperatorRHS() => "Operator_Add_Difference_RHS";
    public string SubtractDifferenceOperatorLHS() => "Operator_Subtract_Difference_LHS";

    public string MultiplyScalarOperatorLHS() => "Operator_Multiply_Scalar_LHS";
    public string MultiplyScalarOperatorRHS() => "Operator_Multiply_Scalar_RHS";
    public string DivideScalarOperatorLHS() => "Operator_Divide_Scalar_LHS";

    public string AddIVectorOperator() => "Operator_Add_IVector";
    public string SubtractIVectorOperator() => "Operator_Subtract_IVector";
    public string MultiplyIScalarOperatorLHS() => "Operator_Multiply_IScalar_LHS";
    public string MultiplyIScalarOperatorRHS() => "Operator_Multiply_IScalar_RHS";
    public string DivideIScalarOperatorLHS() => "Operator_Divide_IScalar_LHS";

    public string AddUnhandledOperatorLHS() => "Operator_Add_Unhandled_LHS";
    public string AddUnhandledOperatorRHS() => "Operator_Add_Unhandled_RHS";
    public string SubtractUnhandledOperatorLHS() => "Operator_Subtract_Unhandled_LHS";
    public string SubtractUnhandledOperatorRHS() => "Operator_Subtract_Unhandled_RHS";
    public string MultiplyUnhandledOperatorLHS() => "Operator_Multiply_Unhandled_LHS";
    public string MultiplyUnhandledOperatorRHS() => "Operator_Multiply_Unhandled_RHS";
    public string DivideUnhandledOperatorLHS() => "Operator_Divide_Unhandled_LHS";

    public bool Equals(VectorDocumentationTags? other) => other is not null && Dimension == other.Dimension;
    public override bool Equals(object? obj) => obj is VectorDocumentationTags other && Equals(other);

    public static bool operator ==(VectorDocumentationTags? lhs, VectorDocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(VectorDocumentationTags? lhs, VectorDocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => Dimension.GetHashCode();
}
