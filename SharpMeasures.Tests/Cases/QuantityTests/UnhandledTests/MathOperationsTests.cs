namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MathOperationsTests
{
    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Plus_Method_ShouldBeUnchanged(Unhandled quantity)
    {
        Unhandled result = quantity.Plus();

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Plus_Operator_ShouldBeUnchanged(Unhandled quantity)
    {
        Unhandled result = +quantity;

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Negate_Method_ShouldBeScaledByMinusOne(Unhandled quantity)
    {
        Unhandled result = quantity.Negate();

        Assert.Equal(quantity.Magnitude * -1, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Negated_Operator_ShouldBeScaledByMinusOne(Unhandled quantity)
    {
        Unhandled result = -quantity;

        Assert.Equal(quantity.Magnitude * -1, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void AddUnhandled_Method_MagnitudeShouldEqualAddition(Unhandled lhs, Unhandled rhs)
    {
        Unhandled result = lhs.Add(rhs);

        Assert.Equal(lhs.Magnitude + rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void AddUnhandled_Operator_MagnitudeShouldEqualAddition(Unhandled lhs, Unhandled rhs)
    {
        Unhandled result = lhs + rhs;

        Assert.Equal(lhs.Magnitude + rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void SubtractUnhandled_Method_MagnitudeShouldEqualSubtraction(Unhandled lhs, Unhandled rhs)
    {
        Unhandled result = lhs.Subtract(rhs);

        Assert.Equal(lhs.Magnitude - rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void SubtractUnhandled_Operator_MagnitudeShouldEqualSubtraction(Unhandled lhs, Unhandled rhs)
    {
        Unhandled result = lhs - rhs;

        Assert.Equal(lhs.Magnitude - rhs.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Method_MagnitudeShouldEqualMultiplication(Unhandled quantity, Unhandled factor)
    {
        Unhandled result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Operator_MagnitudeShouldEqualMultiplication(Unhandled quantity, Unhandled factor)
    {
        Unhandled result = quantity * factor;

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void DivideUnhandled_Method_MagnitudeShouldEqualDivision(Unhandled quantity, Unhandled divisor)
    {
        Unhandled result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void DivideUnhandled_Operator_MagnitudeShouldEqualDivision(Unhandled quantity, Unhandled divisor)
    {
        Unhandled result = quantity / divisor;

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void RemainderDouble_Method_MagnitudeShouldEqualRemainder(Scalar scalar, double divisor)
    {
        Scalar result = scalar.Remainder(divisor);

        Assert.Equal(scalar.Magnitude % divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset>))]
    public void RemainderDouble_Operator_MagnitudeShouldEqualRemainder(Unhandled quantity, double divisor)
    {
        Unhandled result = quantity % divisor;

        Assert.Equal(quantity.Magnitude % divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset>))]
    public void MultiplyDouble_Method_MagnitudeShouldEqualMultiplication(Unhandled quantity, double factor)
    {
        Unhandled result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset>))]
    public void MultiplyDouble_Operator_MagnitudeShouldEqualMultiplication(Unhandled quantity, double factor)
    {
        Unhandled resultLHS = quantity * factor;
        Unhandled resultRHS = factor * quantity;

        Assert.Equal(quantity.Magnitude * factor, resultLHS.Magnitude, 2);
        Assert.Equal(factor * quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset>))]
    public void DivideDouble_Method_MagnitudeShouldEqualDivision(Unhandled quantity, double divisor)
    {
        Unhandled result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset>))]
    public void DivideDouble_Operator_MagnitudeShouldEqualDivision(Unhandled quantity, double divisor)
    {
        Unhandled resultLHS = quantity / divisor;
        Unhandled resultRHS = divisor / quantity;

        Assert.Equal(quantity.Magnitude / divisor, resultLHS.Magnitude, 2);
        Assert.Equal(divisor / quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void RemainderScalar_Method_MagnitudeShouldEqualRemainder(Unhandled quantity, Scalar divisor)
    {
        Unhandled result = quantity.Remainder(divisor);

        Assert.Equal(quantity.Magnitude % divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void RemainderScalar_Operator_MagnitudeShouldEqualRemainder(Unhandled quantity, Scalar divisor)
    {
        Unhandled result = quantity % divisor;

        Assert.Equal(quantity.Magnitude % divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void MultiplyScalar_Method_MagnitudeShouldEqualMultiplication(Unhandled quantity, Scalar factor)
    {
        Unhandled result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void MultiplyScalar_Operator_MagnitudeShouldEqualMultiplication(Unhandled quantity, Scalar factor)
    {
        Unhandled result = quantity * factor;

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void DivideScalar_Method_MagnitudeShouldEqualDivision(Unhandled quantity, Scalar divisor)
    {
        Unhandled result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void DivideScalar_Operator_MagnitudeShouldEqualDivision(Unhandled quantity, Scalar divisor)
    {
        Unhandled result = quantity / divisor;

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void MultiplyGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled.Zero.Multiply(factor!));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void MultiplyGeneric_MagnitudeShouldEqualMultiplication(Unhandled quantity, IScalarQuantity factor)
    {
        Unhandled result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void DivideGeneric_NullDivisor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? divisor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled.Zero.Divide(divisor!));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void DivideGeneric_MagnitudeShouldEqualDivision(Unhandled quantity, IScalarQuantity divisor)
    {
        Unhandled result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void MultiplyIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled.Zero * factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void MultiplyIScalar_MagnitudeShouldEqualMultiplication(Unhandled quantity, IScalarQuantity factor)
    {
        Unhandled result = quantity * factor;

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void DivideIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Unhandled.Zero / factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset>))]
    public void DivideIScalar_MagnitudeShouldEqualDivision(Unhandled quantity, IScalarQuantity factor)
    {
        Unhandled result = quantity / factor;

        Assert.Equal(quantity.Magnitude / factor.Magnitude, result.Magnitude, 2);
    }
}
