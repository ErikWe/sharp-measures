namespace SharpMeasures.Tests.UnhandledCases;

using System;

using Xunit;

public class Operator_Multiply_IVector3Quantity_Unhandled
{
    private static Unhandled3 Target(IVector3Quantity x, Unhandled y) => x * y;

    [Fact]
    public void Null_ArgumentNullException()
    {
        var x = Datasets.GetNullVector3Quantity();
        var y = Datasets.GetValidUnhandled();

        AnyError_TException<ArgumentNullException>(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidVector3))]
    public void MatchMethod(Unhandled y, Vector3 x)
    {
        var expected = Unhandled.Multiply(y, x);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException>(IVector3Quantity x, Unhandled y) where TException : Exception
    {
        var exception = Record.Exception(() => Target(x, y));

        Assert.IsType<TException>(exception);
    }
}
