namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;
using SharpMeasures.Generators.Units;

internal interface IDocumentationStrategy
{
    public abstract string Header();

    public abstract string Zero();
    public abstract string Constant(RefinedVectorConstantDefinition definition);

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
    public abstract string InConstantMultiples(RefinedVectorConstantDefinition definition);
    public abstract string InSpecifiedUnit(UnitInstance unitInstance);

    public abstract string AsDimensionallyEquivalent(IVectorInterface scalar);
    public abstract string CastToDimensionallyEquivalent(IVectorInterface scalar);

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

    public abstract string MultiplyScalarMethod();
    public abstract string DivideScalarMethod();

    public abstract string UnaryPlusOperator();
    public abstract string NegateOperator();

    public abstract string MultiplyScalarOperatorLHS();
    public abstract string MultiplyScalarOperatorRHS();
    public abstract string DivideScalarOperatorLHS();
}
