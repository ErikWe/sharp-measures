namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

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

    public abstract string Conversion(NamedType vector);
    public abstract string AntidirectionalConversion(NamedType vector);
    public abstract string CastConversion(NamedType vector);
    public abstract string AntidirectionalCastConversion(NamedType vector);

    public abstract string OperationMethod(IQuantityOperation operation, NamedType other);
    public abstract string MirroredOperationMethod(IQuantityOperation operation, NamedType other);
    public abstract string VectorOperationMethod(IVectorOperation operation, NamedType other);
    public abstract string MirroredVectorOperationMethod(IVectorOperation operation, NamedType other);
    public abstract string OperationOperator(IQuantityOperation operation, NamedType other);
    public abstract string MirroredOperationOperator(IQuantityOperation operation, NamedType other);

    public abstract string Process(IQuantityProcess process);

    public abstract string IsNaN();
    public abstract string IsZero();
    public abstract string IsFinite();
    public abstract string IsInfinite();
    
    public abstract string Magnitude();

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

    public abstract string AddTVectorMethod();
    public abstract string SubtractTVectorMethod();
    public abstract string SubtractFromTVectorMethod();
    public abstract string MultiplyTScalarMethod();
    public abstract string DivideTScalarMethod();

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

    public abstract string AddIVectorOperator();
    public abstract string SubtractIVectorOperator();
    public abstract string MultiplyIScalarOperatorLHS();
    public abstract string MultiplyIScalarOperatorRHS();
    public abstract string DivideIScalarOperatorLHS();

    public abstract string AddUnhandledOperatorLHS();
    public abstract string AddUnhandledOperatorRHS();
    public abstract string SubtractUnhandledOperatorLHS();
    public abstract string SubtractUnhandledOperatorRHS();
    public abstract string MultiplyUnhandledOperatorLHS();
    public abstract string MultiplyUnhandledOperatorRHS();
    public abstract string DivideUnhandledOperatorLHS();
}
