namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ToVector3Tests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, Vector3Dataset>))]
    public void Vector3_Method_ShouldMatchMultiplication(Unhandled quantity, Vector3 vector)
    {
        Unhandled3 result = quantity.Multiply(vector);

        Assert.Equal(quantity.Magnitude * vector.X, result.X, 2);
        Assert.Equal(quantity.Magnitude * vector.Y, result.Y, 2);
        Assert.Equal(quantity.Magnitude * vector.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void DoubleTuple_Method_ShouldMatchMultiplication(Unhandled quantity, double x, double y, double z)
    {
        Unhandled3 result = quantity.Multiply((x, y, z));

        Assert.Equal(quantity.Magnitude * x, result.X, 2);
        Assert.Equal(quantity.Magnitude * y, result.Y, 2);
        Assert.Equal(quantity.Magnitude * z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void ScalarTuple_Method_ShouldMatchMultiplication(Unhandled quantity, Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 result = quantity.Multiply((x, y, z));

        Assert.Equal(quantity.Magnitude * x, result.X, 2);
        Assert.Equal(quantity.Magnitude * y, result.Y, 2);
        Assert.Equal(quantity.Magnitude * z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, Vector3Dataset>))]
    public void Vector3_Operator_ShouldMatchMultiplication(Unhandled quantity, Vector3 vector)
    {
        Unhandled3 resultLHS = quantity * vector;
        Unhandled3 resultRHS = vector * quantity;

        Assert.Equal(quantity.Magnitude * vector.X, resultLHS.X, 2);
        Assert.Equal(quantity.Magnitude * vector.Y, resultLHS.Y, 2);
        Assert.Equal(quantity.Magnitude * vector.Z, resultLHS.Z, 2);

        Assert.Equal(vector.X * quantity.Magnitude, resultRHS.X, 2);
        Assert.Equal(vector.Y * quantity.Magnitude, resultRHS.Y, 2);
        Assert.Equal(vector.Z * quantity.Magnitude, resultRHS.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void DoubleTuple_Operator_ShouldMatchMultiplication(Unhandled quantity, double x, double y, double z)
    {
        Unhandled3 resultLHS = quantity * (x, y, z);
        Unhandled3 resultRHS = (x, y, z) * quantity;

        Assert.Equal(quantity.Magnitude * x, resultLHS.X, 2);
        Assert.Equal(quantity.Magnitude * y, resultLHS.Y, 2);
        Assert.Equal(quantity.Magnitude * z, resultLHS.Z, 2);

        Assert.Equal(x * quantity.Magnitude, resultRHS.X, 2);
        Assert.Equal(y * quantity.Magnitude, resultRHS.Y, 2);
        Assert.Equal(z * quantity.Magnitude, resultRHS.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void ScalarTuple_Operator_ShouldMatchMultiplication(Unhandled quantity, Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 resultLHS = quantity * (x, y, z);
        Unhandled3 resultRHS = (x, y, z) * quantity;

        Assert.Equal(quantity.Magnitude * x, resultLHS.X, 2);
        Assert.Equal(quantity.Magnitude * y, resultLHS.Y, 2);
        Assert.Equal(quantity.Magnitude * z, resultLHS.Z, 2);

        Assert.Equal(x * quantity.Magnitude, resultRHS.X, 2);
        Assert.Equal(y * quantity.Magnitude, resultRHS.Y, 2);
        Assert.Equal(z * quantity.Magnitude, resultRHS.Z, 2);
    }
}
