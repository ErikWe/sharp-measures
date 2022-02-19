namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Double;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.Unhandled;

using System;

using Xunit;

public class MathOperationsTests
{
    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Plus_Method_ShouldBeUnchanged(Scalar scalar)
    {
        Scalar result = scalar.Plus();

        Assert.Equal(scalar, result);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Plus_Operator_ShouldBeUnchanged(Scalar scalar)
    {
        Scalar result = +scalar;

        Assert.Equal(scalar, result);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Negate_Method_ShouldBeScaledByMinusOne(Scalar scalar)
    {
        Scalar result = scalar.Negate();

        Assert.Equal(scalar.Magnitude * -1, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Negated_Operator_ShouldBeScaledByMinusOne(Scalar scalar)
    {
        Scalar result = -scalar;

        Assert.Equal(scalar.Magnitude * -1, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void AddDouble_Method_MagnitudeShouldEqualAddition(Scalar scalar, double term)
    {
        Scalar result = scalar.Add(term);

        Assert.Equal(scalar.Magnitude + term, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void AddDouble_Operator_MagnitudeShouldEqualAddition(Scalar scalar, double term)
    {
        Scalar resultLHS = scalar + term;
        Scalar resultRHS = term + scalar;

        Assert.Equal(scalar.Magnitude + term, resultLHS.Magnitude, 2);
        Assert.Equal(term + scalar.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void SubtractDouble_Method_MagnitudeShouldEqualSubtraction(Scalar scalar, double term)
    {
        Scalar result = scalar.Subtract(term);

        Assert.Equal(scalar.Magnitude - term, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void SubtractDouble_Operator_MagnitudeShouldEqualSubtraction(Scalar scalar, double term)
    {
        Scalar resultLHS = scalar - term;
        Scalar resultRHS = term - scalar;

        Assert.Equal(scalar.Magnitude - term, resultLHS.Magnitude, 2);
        Assert.Equal(term - scalar.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void AddScalar_Method_MagnitudeShouldEqualAddition(Scalar lhs, Scalar rhs)
    {
        Scalar result = lhs.Add(rhs);

        Assert.Equal(lhs.Magnitude + rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void AddScalar_Operator_MagnitudeShouldEqualAddition(Scalar lhs, Scalar rhs)
    {
        Scalar result = lhs + rhs;

        Assert.Equal(lhs.Magnitude + rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void SubtractScalar_Method_MagnitudeShouldEqualSubtraction(Scalar lhs, Scalar rhs)
    {
        Scalar result = lhs.Subtract(rhs);

        Assert.Equal(lhs.Magnitude - rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void SubtractScalar_Operator_MagnitudeShouldEqualSubtraction(Scalar lhs, Scalar rhs)
    {
        Scalar result = lhs - rhs;

        Assert.Equal(lhs.Magnitude - rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Method_MagnitudeShouldEqualMultiplication(Scalar scalar, Unhandled factor)
    {
        Unhandled result = scalar.Multiply(factor);

        Assert.Equal(scalar.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Operator_MagnitudeShouldEqualMultiplication(Scalar scalar, Unhandled factor)
    {
        Unhandled result = scalar * factor;

        Assert.Equal(scalar.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnhandledDataset>))]
    public void DivideUnhandled_Method_MagnitudeShouldEqualDivision(Scalar scalar, Unhandled divisor)
    {
        Unhandled result = scalar.Divide(divisor);

        Assert.Equal(scalar.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnhandledDataset>))]
    public void DivideUnhandled_Operator_MagnitudeShouldEqualDivision(Scalar scalar, Unhandled divisor)
    {
        Unhandled result = scalar / divisor;

        Assert.Equal(scalar.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void RemainderDouble_Method_MagnitudeShouldEqualRemainder(Scalar scalar, double divisor)
    {
        Scalar result = scalar.Remainder(divisor);

        Assert.Equal(scalar.Magnitude % divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void RemainderDouble_Operator_MagnitudeShouldEqualRemainder(Scalar scalar, double divisor)
    {
        Scalar result = scalar % divisor;

        Assert.Equal(scalar.Magnitude % divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void MultiplyDouble_Method_MagnitudeShouldEqualMultiplication(Scalar scalar, double factor)
    {
        Scalar result = scalar.Multiply(factor);

        Assert.Equal(scalar.Magnitude * factor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void MultiplyDouble_Operator_MagnitudeShouldEqualMultiplication(Scalar scalar, double factor)
    {
        Scalar resultLHS = scalar * factor;
        Scalar resultRHS = factor * scalar;

        Assert.Equal(scalar.Magnitude * factor, resultLHS.Magnitude, 2);
        Assert.Equal(factor * scalar.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void DivideDouble_Method_MagnitudeShouldEqualDivision(Scalar scalar, double divisor)
    {
        Scalar result = scalar.Divide(divisor);

        Assert.Equal(scalar.Magnitude / divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void DivideDouble_Operator_MagnitudeShouldEqualDivision(Scalar scalar, double divisor)
    {
        Scalar resultLHS = scalar / divisor;
        Scalar resultRHS = divisor / scalar;

        Assert.Equal(scalar.Magnitude / divisor, resultLHS.Magnitude, 2);
        Assert.Equal(divisor / scalar.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void RemainderScalar_Method_MagnitudeShouldEqualRemainder(Scalar scalar, Scalar divisor)
    {
        Scalar result = scalar.Remainder(divisor);

        Assert.Equal(scalar.Magnitude % divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void RemainderScalar_Operator_MagnitudeShouldEqualRemainder(Scalar scalar, Scalar divisor)
    {
        Scalar result = scalar % divisor;

        Assert.Equal(scalar.Magnitude % divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void MultiplyScalar_Method_MagnitudeShouldEqualMultiplication(Scalar scalar, Scalar factor)
    {
        Scalar result = scalar.Multiply(factor);

        Assert.Equal(scalar.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void MultiplyScalar_Operator_MagnitudeShouldEqualMultiplication(Scalar scalar, Scalar factor)
    {
        Scalar resultLHS = scalar * factor;
        Scalar resultRHS = factor * scalar;

        Assert.Equal(scalar.Magnitude * factor.Magnitude, resultLHS.Magnitude, 2);
        Assert.Equal(factor.Magnitude * scalar.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void DivideScalar_Method_MagnitudeShouldEqualDivision(Scalar scalar, Scalar divisor)
    {
        Scalar result = scalar.Divide(divisor);

        Assert.Equal(scalar.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void DivideScalar_Operator_MagnitudeShouldEqualDivision(Scalar scalar, Scalar divisor)
    {
        Scalar resultLHS = scalar / divisor;
        Scalar resultRHS = divisor / scalar;

        Assert.Equal(scalar.Magnitude / divisor.Magnitude, resultLHS.Magnitude, 2);
        Assert.Equal(divisor.Magnitude / scalar.Magnitude, resultRHS.Magnitude, 2);
    }

    [Fact]
    public void MultiplyGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Scalar>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Scalar.Zero.Multiply(Scalar.Zero, factory!));
    }

    [Fact]
    public void MultiplyGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Scalar.Zero.Multiply(factor!, (x) => new Scalar(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void MultiplyGeneric_MagnitudeShouldEqualMultiplication(Scalar scalar, IScalarQuantity factor)
    {
        Scalar result = scalar.Multiply(factor, (x) => new Scalar(x));

        Assert.Equal(scalar.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void DivideGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Scalar>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Scalar.Zero.Divide(Scalar.Zero, factory!));
    }

    [Fact]
    public void DivideGeneric_NullDivisor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? divisor = null;

        Assert.Throws<ArgumentNullException>(() => Scalar.Zero.Divide(divisor!, (x) => new Scalar(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void DivideGeneric_MagnitudeShouldEqualDivision(Scalar scalar, IScalarQuantity divisor)
    {
        Scalar result = scalar.Divide(divisor, (x) => new Scalar(x));

        Assert.Equal(scalar.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void MultiplyIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Scalar.Zero * factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnhandledDataset>))]
    public void MultiplyIScalar_MagnitudeShouldEqualMultiplication(Scalar scalar, IScalarQuantity factor)
    {
        Unhandled result = scalar * factor;

        Assert.Equal(scalar.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void DivideIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Scalar.Zero / factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnhandledDataset>))]
    public void DivideIScalar_MagnitudeShouldEqualDivision(Scalar scalar, IScalarQuantity factor)
    {
        Unhandled result = scalar / factor;

        Assert.Equal(scalar.Magnitude / factor.Magnitude, result.Magnitude, 2);
    }
}
