namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Divide_Unhandled_IScalarQuantity
{
    private static Unhandled Target(Unhandled x, IScalarQuantity y) => x / y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetValidUnhandled();
        var y = Datasets.GetNullScalarQuantity();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMethod(Unhandled x, Scalar y)
    {
        var expected = Unhandled.Divide(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(Unhandled x, IScalarQuantity y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
