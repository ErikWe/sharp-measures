namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;
using SharpMeasures.Generators.Units;

internal interface IDocumentationStrategy
{
    public abstract string Header();

    public abstract string Zero();
    public abstract string Constant(RefinedScalarConstantDefinition definition);
    public abstract string UnitBase(UnitInstance unitInstance);

    public abstract string WithMagnitude();

    public abstract string FromReciprocal();
    public abstract string FromSquare();
    public abstract string FromCube();
    public abstract string FromSquareRoot();
    public abstract string FromCubeRoot();

    public abstract string ScalarConstructor();
    public abstract string ScalarAndUnitConstructor();

    public abstract string Magnitude();
    public abstract string InUnit();
    public abstract string InConstantMultiples(RefinedScalarConstantDefinition definition);
    public abstract string InSpecifiedUnit(UnitInstance unitInstance);

    public abstract string AsDimensionallyEquivalent(ScalarInterface scalar);
    public abstract string CastToDimensionallyEquivalent(ScalarInterface scalar);

    public abstract string IsNaN();
    public abstract string IsZero();
    public abstract string IsPositive();
    public abstract string IsNegative();
    public abstract string IsFinite();
    public abstract string IsInfinite();
    public abstract string IsPositiveInfinity();
    public abstract string IsNegativeInfinity();

    public abstract string Absolute();
    public abstract string Sign();

    public abstract string Reciprocal();
    public abstract string Square();
    public abstract string Cube();
    public abstract string SquareRoot();
    public abstract string CubeRoot();

    public abstract string ToStringDocumentation();

    public abstract string EqualsSameTypeMethod();
    public abstract string EqualsObjectMethod();

    public abstract string EqualitySameTypeOperator();
    public abstract string InequalitySameTypeOperator();

    public abstract string GetHashCodeDocumentation();

    public abstract string UnaryPlusMethod();
    public abstract string NegateMethod();

    public abstract string AddSameTypeMethod();
    public abstract string SubtractSameTypeMethod();
    public abstract string SubtractFromSameTypeMethod();

    public abstract string AddDifferenceMethod();
    public abstract string SubtractDifferenceMethod();

    public abstract string MultiplyScalarMethod();
    public abstract string DivideScalarMethod();

    public abstract string MultiplySameTypeMethod();
    public abstract string DivideSameTypeMethod();

    public abstract string MultiplyVectorMethod(int dimension);

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
    public abstract string DivideScalarOperatorRHS();

    public abstract string MultiplySameTypeOperator();
    public abstract string DivideSameTypeOperator();

    public abstract string MultiplyVectorOperatorLHS(int dimension);
    public abstract string MultiplyVectorOperatorRHS(int dimension);

    public abstract string CompareToSameType();

    public abstract string LessThanSameType();
    public abstract string GreaterThanSameType();
    public abstract string LessThanOrEqualSameType();
    public abstract string GreaterThanOrEqualSameType();
}
