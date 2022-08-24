﻿namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Units.UnitInstances;

internal interface IVectorDocumentationStrategy
{
    public abstract string Header();

    public abstract string Zero();
    public abstract string Constant(IVectorConstant constant);

    public abstract string WithScalarComponents();
    public abstract string WithVectorComponents();

    public abstract string ComponentsConstructor();
    public abstract string ScalarsConstructor();
    public abstract string VectorConstructor();
    public abstract string ScalarsAndUnitConstructor();
    public abstract string VectorAndUnitConstructor();

    public abstract string CastFromComponents();

    public abstract string Component(int dimensionIndex);
    public abstract string ComponentMagnitude(int dimensionIndex);
    public abstract string Components();

    public abstract string InUnit();
    public abstract string InConstantMultiples(IVectorConstant constant);
    public abstract string InSpecifiedUnit(IUnitInstance unitInstance);

    public abstract string Conversion(NamedType vectorGroupMember);
    public abstract string CastConversion(NamedType vectorGroupMember);

    public abstract string IsNaN();
    public abstract string IsZero();
    public abstract string IsFinite();
    public abstract string IsInfinite();
    
    public abstract string Magnitude();
    public abstract string SquaredMagnitude();

    public abstract string ScalarMagnitude();
    public abstract string ScalarSquaredMagnitude();

    public abstract string Normalize();
    public abstract string Transform();

    public abstract string ToStringDocumentation();

    public abstract string EqualsSameTypeMethod();
    public abstract string EqualsObjectMethod();

    public abstract string EqualitySameTypeOperator();
    public abstract string InequalitySameTypeOperator();

    public abstract string GetHashCodeDocumentation();

    public abstract string Deconstruct();

    public abstract string UnaryPlusMethod();
    public abstract string NegateMethod();

    public abstract string AddSameTypeMethod();
    public abstract string SubtractSameTypeMethod();

    public abstract string AddDifferenceMethod();
    public abstract string SubtractDifferenceMethod();

    public abstract string MultiplyScalarMethod();
    public abstract string DivideScalarMethod();

    public abstract string UnaryPlusOperator();
    public abstract string NegateOperator();

    public abstract string AddSameTypeOperator();
    public abstract string SubtractSameTypeOperator();

    public abstract string AddDifferenceOperatorLHS();
    public abstract string AddDifferenceOperatorRHS();
    public abstract string SubtractDifferenceOperatorLHS();

    public abstract string MultiplyScalarOperatorLHS();
    public abstract string MultiplyScalarOperatorRHS();
    public abstract string DivideScalarOperatorLHS();
}